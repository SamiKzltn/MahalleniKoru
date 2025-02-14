using UnityEngine;
using UnityEngine.UI;

public class NormalGun : BaseGun
{
    private UIManager uiManager => UIManager.Instance;
    private AudioManager audioManager => AudioManager.Instance;
    private ParticalManager particalManager => ParticalManager.Instance;
    private PlayerScript playerScript => PlayerScript.Instance;

    protected override void Initialize()
    {
        SetAnimEvent();
        benimCam = transform.GetComponentInParent<Camera>();

        View = benimCam.fieldOfView;
        Scope.SetActive(false);

        //Sileceksin Sonra
        Debug.Log(CommonData.GetAmmo("Taramali_Mermi"));
        Debug.Log(CommonData.GetAmmo("Magnum_Mermi"));
        Debug.Log(CommonData.GetAmmo("Pompali_Mermi"));
        Debug.Log(CommonData.GetAmmo("Sniper_Mermi"));
        


        MaxMermiSayisi = CommonData.GetAmmo(Gun_Scriptable.gunName + "_Mermi", 50); // Varsayýlan 50 olsun
        kalanMermi = CommonData.GetAmmo(Gun_Scriptable.gunName + "_KalanMermi", 0); // Varsayýlan 0 olsun

        // Þarjörü doldur ve kalan mermiyi ayarla
        if (kalanMermi < Gun_Scriptable.magazineCapacity)
        {
            int doldurulacakMermi = Mathf.Min(Gun_Scriptable.magazineCapacity - kalanMermi, MaxMermiSayisi);
            kalanMermi += doldurulacakMermi;
            MaxMermiSayisi -= doldurulacakMermi;
        }

        // UI Güncelle
        uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);

        // Þarjör bilgilerini PlayerPrefs'e kaydet
        CommonData.AmmoSave(Gun_Scriptable.gunName + "_Mermi", MaxMermiSayisi);
        CommonData.AmmoSave(Gun_Scriptable.gunName + "_KalanMermi", kalanMermi);
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
            if (hit.transform.tag == "Dusman")
            {
                particalManager.BloodHitParticalls(hit);
                playerScript.playerMoney += 1000;
                Debug.Log(playerScript.playerMoney);
            }
            else if (hit.transform.tag == "Devrilebilir")
            {
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.AddForce(benimCam.transform.forward * 10);
            }
            else
            {
                particalManager.ObjectHitParticalls(hit);
            }
            Debug.Log("Hit: " + hit.transform.name);
        }
    }
    protected override void Loot()
    {
        RaycastHit hit;

        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, 3))
        {
            if (hit.transform.gameObject.CompareTag("Mermi"))
            {
                AmmoKayit(hit.transform.gameObject.GetComponent<AmmoScripts>().Gun_Type, hit.transform.gameObject.GetComponent<AmmoScripts>().Ammo_Type);
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
    public virtual void AmmoKayit(string guntype, int ammo_number)
    { 
        int last;
        last = CommonData.GetAmmo(guntype + "_Mermi");
        switch (guntype)
        {
            case "Taramali":
                last += ammo_number;
                CommonData.AmmoSave("Taramali_Mermi", last);
                if (Gun_Scriptable.gunName == guntype)
                {
                    uiManager.ShowAllAmmo(last, kalanMermi);
                    MaxMermiSayisi = last;
                }
                break;
            case "Pompali":
                last += ammo_number;
                CommonData.AmmoSave("Pompali_Mermi", last);
                if (Gun_Scriptable.gunName == guntype)
                {
                    uiManager.ShowAllAmmo(last, kalanMermi);
                    MaxMermiSayisi = last;
                }
                break;
            case "Magnum":
                last += ammo_number;
                CommonData.AmmoSave("Magnum_Mermi", last);
                if (Gun_Scriptable.gunName == guntype)
                {
                    uiManager.ShowAllAmmo(last, kalanMermi);
                    MaxMermiSayisi = last;
                }
                break;
            case "Sniper":
                last += ammo_number;
                CommonData.AmmoSave("Sniper_Mermi", last);
                if (Gun_Scriptable.gunName == guntype)
                {
                    uiManager.ShowAllAmmo(last, kalanMermi);
                    MaxMermiSayisi = last;
                }
                break;
        }

    }
    protected override void WeaponChange()
    {
        MaxMermiSayisi = CommonData.GetAmmo(Gun_Scriptable.gunName + "_Mermi", 50); // Varsayýlan 50 olsun
        kalanMermi = CommonData.GetAmmo(Gun_Scriptable.gunName + "_KalanMermi", 0); // Varsayýlan 0 olsun

        if (kalanMermi < Gun_Scriptable.magazineCapacity)
        {
            int doldurulacakMermi = Mathf.Min(Gun_Scriptable.magazineCapacity - kalanMermi, MaxMermiSayisi);
            kalanMermi += doldurulacakMermi;
            MaxMermiSayisi -= doldurulacakMermi;
        }

        uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
    }
}
