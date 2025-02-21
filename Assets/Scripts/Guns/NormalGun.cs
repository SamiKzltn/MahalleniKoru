using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalGun : BaseGun
{
    private UIManager uiManager => UIManager.Instance;
    private AudioManager audioManager => AudioManager.Instance;
    private ParticalManager particalManager => ParticalManager.Instance;
    private PlayerScript playerScript => PlayerScript.Instance;
    private SaveLoadSystem saveLoadSystem => SaveLoadSystem.Instance;
    //private BotScript botScript => BotScript.Instance;
    public PlayerData _playerData { get; set; }
    public List<Weapon> weaponList
    {
        get
        {
            return _playerData.allweapons;
        }
        set
        {
            _playerData.allweapons = value;
        }
    }

    Weapon _weapon;

    protected override void Initialize()
    {
        SetAnimEvent();
        benimCam = transform.GetComponentInParent<Camera>();
        View = benimCam.fieldOfView;
        Scope.SetActive(false);

        // Silah ve mermi verilerini JSON sisteminden al
        _weapon = saveLoadSystem.weaponList.Find(w => w.ID == Gun_Scriptable.ID);
        gunid = _weapon.ID;
        gunAllBullet = _weapon.totalAmmo;
        Debug.Log(Gun_Scriptable.ID);
        foreach (var weapon in saveLoadSystem.weaponList)
        {
            Debug.Log(weapon.ID);
        }
        MaxMermiSayisi = _weapon != null ? _weapon.totalAmmo : 50; // Varsayýlan 50
        kalanMermi = _weapon != null ? _weapon.magazineAmmo : 0; // Varsayýlan 0

        // Þarjörü doldur ve kalan mermiyi ayarla
        if (kalanMermi < Gun_Scriptable.magazineCapacity)
        {
            int doldurulacakMermi = Mathf.Min(Gun_Scriptable.magazineCapacity - kalanMermi, MaxMermiSayisi);
            kalanMermi += doldurulacakMermi;
            MaxMermiSayisi -= doldurulacakMermi;
        }
        // UI Güncelle
        uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
    }
    protected virtual void SetAnimEvent()
    {
        AnimationClip[] clips = animatorum.runtimeAnimatorController.animationClips;
        AnimationClip clip;
        for (int i = 0; i < clips.Length; i++)
        {
            clip = clips[i];
            if (clip.name == "Reload")
            {
                clip.AddEvent(new AnimationEvent { functionName = nameof(PlayReloadClip), time = .11f });
                return;
            }

        }
    }
    protected override void Fire()
    {
        GameObject obje = Instantiate(Kovan_Objesi, Kovan_Cikis_Noktasi.transform.position, Kovan_Cikis_Noktasi.transform.rotation);
        Rigidbody rigit = obje.GetComponent<Rigidbody>();
        rigit.AddRelativeForce(new Vector3(-10f, 1, 0) * 30);

        audioManager.PlayEffect(atesSesi);
        ParticalManager.Instance.FireEffect(muzzleTransform.position, muzzleTransform.rotation, muzzleTransform);
        animatorum.Play("AtesAnim");

        kalanMermi--;
        uiManager.JustMagazine(kalanMermi);

        RaycastHit hit;
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, Gun_Scriptable.Range))
        {
            if (hit.transform.TryGetComponent(out IDamageable damageable))
            {
                particalManager.BloodHitParticalls(hit);
                bool death = damageable.Hit(50);
                if (death) playerScript.playerMoney += 1000;

            }
            else if (hit.transform.TryGetComponent(out IDamageable component)) 
            {
                component.Hit(50);
            }
            else
            {
                particalManager.ObjectHitParticalls(hit);
            }
        }
    }
    protected override void Loot()
    {
        RaycastHit hit;

        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, 3))
        {
            if (hit.transform.gameObject.CompareTag("Mermi"))
            {
                AmmoKayit(hit.transform.gameObject.GetComponent<AmmoScripts>().gunId, hit.transform.gameObject.GetComponent<AmmoScripts>().Ammo_Type);
                Debug.Log(hit.transform.gameObject.GetComponent<AmmoScripts>().Ammo_Type);
                AmmoBoxSpawner.DestroyPoints(hit.transform.gameObject.GetComponent<AmmoScripts>().Pointi);
                Destroy(hit.transform.gameObject);
                AmmoBoxSpawner.HowMuchExits--;
            }
        }
    }
    protected override void OnEmptyFireAttempt()
    {
        if (!bosSesCaldi || Time.time > sonSesZamani + sesBeklemeSuresi)
        {
            audioManager.PlayEffect(emptySesi);
            bosSesCaldi = true;
            sonSesZamani = Time.time;
        }
    }
    protected override void Reload()
    {
        if (kalanMermi >= Gun_Scriptable.magazineCapacity || MaxMermiSayisi <= 0) return;

        if (kalanMermi + MaxMermiSayisi <= Gun_Scriptable.magazineCapacity)
        {
            kalanMermi += MaxMermiSayisi;
            MaxMermiSayisi = 0;
            uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);

        }
        else
        {
            int aradakiFark = Gun_Scriptable.magazineCapacity - kalanMermi;
            MaxMermiSayisi -= aradakiFark;
            kalanMermi = Gun_Scriptable.magazineCapacity;
            uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);

        }
        CommonData.AmmoSave(Gun_Scriptable.gunName + "_Mermi", MaxMermiSayisi);

        animatorum.Play("Reload");
        Save();
    }
    protected override void ZoomIn()
    {
        animatorum.SetBool("ZoomIn", true);
        benimCam.cullingMask = ~(1 << 6);
        benimCam.fieldOfView = zoomView;
        Aim.SetActive(false);
        Scope.SetActive(true);
    }
    protected override void ZoomOut()
    {
        animatorum.SetBool("ZoomIn", false);
        benimCam.cullingMask = -1;
        benimCam.fieldOfView = View;
        Aim.SetActive(true);
        Scope.SetActive(false);
    }
    public virtual void PlayReloadClip()
    {
        audioManager.PlayEffect(reloadSesi);
    }
    protected void AmmoKayit(int gunid2, int ammo_number)
    {
        //_weapon = saveLoadSystem.weaponList.Find(w => w.ID == Gun_Scriptable.ID);

        foreach (Weapon weapon in saveLoadSystem.weaponList)
        {
            if (weapon.ID == gunid2)
            {
                if (gunid == gunid2)
                {
                    MaxMermiSayisi += ammo_number;
                    Save();
                    uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
                }
                else
                {
                    weapon.totalAmmo += ammo_number;
                    Save();
                }
            }
        }
    }
    protected override void WeaponChange()
    {
        Save();
        Weapon _weapon = saveLoadSystem.weaponList.Find(w => w.ID == Gun_Scriptable.ID);
        MaxMermiSayisi = _weapon != null ? _weapon.totalAmmo : 50; // Varsayýlan 50
        kalanMermi = _weapon != null ? _weapon.magazineAmmo : 0; // Varsayýlan 0

        if (kalanMermi < Gun_Scriptable.magazineCapacity)
        {
            int doldurulacakMermi = Mathf.Min(Gun_Scriptable.magazineCapacity - kalanMermi, MaxMermiSayisi);
            kalanMermi += doldurulacakMermi;
            MaxMermiSayisi -= doldurulacakMermi;
        }

        uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
    }

    protected void Save()
    {
        List<Weapon> allWeaponsList = saveLoadSystem.weaponList; // Örneðin tüm silahlarý çeken fonksiyon

        foreach (Weapon weapon1 in allWeaponsList)
        {
            if (weapon1.ID == gunid)
            {
                weapon1.totalAmmo = MaxMermiSayisi;
                weapon1.magazineAmmo = kalanMermi;
            }
        }
    }
}