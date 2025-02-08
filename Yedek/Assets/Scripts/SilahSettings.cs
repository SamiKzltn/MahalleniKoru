using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;

public class SilahSettings : MonoBehaviour
{
    [Header("AYARLAR")]
    public bool atesEdebilir;
  protected  float IceridenAtesSikligi;
    public float DisaridanAtesSikligi;
    public float Menzil;
    public string Silah_Adi;

    [Header("SESLER")]
    public AudioClip atesSesi;
    public AudioClip reloadSesi;
    public AudioClip emptySesi;
    public AudioClip AmmoTakeSound;

    [Header("Silah Ayarlari")]
  protected  int MaxMermiSayisi;
    public int kapasite;
    protected int kalanMermi;

    public GameObject Kovan_Objesi;
    public GameObject Kovan_Cikis_Noktasi;

    public AmmoBoxSpawn AmmoBoxSpawner;

    [Header("DİĞER")]
    public Camera benimCam;
    Animator animatorum;

    protected bool bosSesCaldi = false;
    protected float sonSesZamani = 0f;
    protected float sesBeklemeSuresi = 1f;


    private UIManager uiManager => UIManager.Instance;
    private AudioManager audioManager => AudioManager.Instance;

    private ParticalManager particalManager => ParticalManager.Instance;

    void Start()
    {
        benimCam = transform.GetComponentInParent<Camera>();
        MaxMermiSayisi = PlayerPrefs.GetInt(Silah_Adi + "_Mermi");
        StartLoad();
        animatorum = GetComponent<Animator>();
        uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilir && Time.time > IceridenAtesSikligi && kalanMermi != 0)
            {
                AtesEt();
                IceridenAtesSikligi = Time.time + DisaridanAtesSikligi;
                bosSesCaldi = false;
            }
            else if (kalanMermi == 0)
            {
                if (!bosSesCaldi || Time.time > sonSesZamani + sesBeklemeSuresi)
                {
                    audioManager.PlayEffect(emptySesi);
                    bosSesCaldi = true;
                    sonSesZamani = Time.time;
                }
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (MaxMermiSayisi != 0)
            {
                if (kalanMermi < kapasite)
                {
                    Reload();
                    PlayerPrefs.SetInt(Silah_Adi + "_Mermi", MaxMermiSayisi);
                    animatorum.Play("Reload");
                    audioManager.PlayEffect(reloadSesi);

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            MermiAl();
            audioManager.PlayEffect(AmmoTakeSound);
            uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
        }
    }
    void MermiAl()
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
    void Reload()
    {
        if (kalanMermi + MaxMermiSayisi <= kapasite)
        {
            kalanMermi += MaxMermiSayisi;
            MaxMermiSayisi = 0;
            uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
        }
        else
        {
            int aradakiFark = kapasite - kalanMermi;
            MaxMermiSayisi -= aradakiFark;
            kalanMermi = kapasite;
            uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);
        }
    }
    void StartLoad()
    {
        MaxMermiSayisi = PlayerPrefs.GetInt(Silah_Adi + "_Mermi", 50); // Varsayılan 50 olsun
        kalanMermi = PlayerPrefs.GetInt(Silah_Adi + "_KalanMermi", 0); // Varsayılan 0 olsun

        // Şarjörü doldur ve kalan mermiyi ayarla
        if (kalanMermi < kapasite)
        {
            int doldurulacakMermi = Math.Min(kapasite - kalanMermi, MaxMermiSayisi);
            kalanMermi += doldurulacakMermi;
            MaxMermiSayisi -= doldurulacakMermi;
        }

        // UI Güncelle
        uiManager.ShowAllAmmo(MaxMermiSayisi, kalanMermi);

        // Şarjör bilgilerini PlayerPrefs'e kaydet
        PlayerPrefs.SetInt(Silah_Adi + "_Mermi", MaxMermiSayisi);
        PlayerPrefs.SetInt(Silah_Adi + "_KalanMermi", kalanMermi);
    }

    public Transform muzzleTransform;

    void AtesEt()
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
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, Menzil))
        {
            if (hit.transform.tag == "Dusman")
            {
                particalManager.BloodHitParticalls(hit);
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
    void AmmoKayit(string guntype, int ammo_number)
    {
        switch (guntype)
        {
            case "Taramali":
                MaxMermiSayisi += ammo_number;
                PlayerPrefs.SetInt("Taramali_Mermi", MaxMermiSayisi);
                break;
            case "Pompali":
                break;
            case "Magnum":
                break;
            case "Sniper":
                break;
        }
    }
}