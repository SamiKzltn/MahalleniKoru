using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class MagnumSettings : MonoBehaviour
{
    [Header("AYARLAR")]
    public bool atesEdebilir;
    float IceridenAtesSikligi;
    public float DisaridanAtesSikligi;
    public float Menzil;
    public string Silah_Adi;

    [Header("SESLER")]
    public AudioSource atesSesi;
    public AudioSource reloadSesi;
    public AudioSource emptySesi;
    public AudioSource AmmoTakeSound;

    [Header("EFEKTLER")]
    public ParticleSystem atesEfekti;
    public ParticleSystem mermi›zi;
    public ParticleSystem kanEfekti;

    [Header("Silah Ayarlari")]
    int MaxMermiSayisi;
    public int kapasite;
    int kalanMermi;
    public TextMeshProUGUI ToplamMermiSayisiText;
    public TextMeshProUGUI KalanMermiSayisiText;

    public GameObject Kovan_Objesi;
    public GameObject Kovan_Cikis_Noktasi;

    public AmmoBoxSpawn AmmoBoxSpawner;

    [Header("D›–ER")]
    public Camera benimCam;
    Animator animatorum;

    void Start()
    {
        MaxMermiSayisi = PlayerPrefs.GetInt(Silah_Adi + "_Mermi");
        StartLoad();
        animatorum = GetComponent<Animator>();
        ToplamMermiSayisiText.text = MaxMermiSayisi.ToString();
        KalanMermiSayisiText.text = kalanMermi.ToString();
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
            }
            if (kalanMermi == 0)
            {
                EmptySesCal();
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
                    SesCalReload();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            MermiAl();
            AmmoTakeSound.Play();
            ToplamMermiSayisiText.text = MaxMermiSayisi.ToString();
            KalanMermiSayisiText.text = kalanMermi.ToString();
        }
    }
    void SesCalReload()
    {
        reloadSesi.Play();
    }
    void EmptySesCal()
    {
        emptySesi.Play();
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
            KalanMermiSayisiText.text = kalanMermi.ToString();
            ToplamMermiSayisiText.text = MaxMermiSayisi.ToString();
        }
        else
        {
            int aradakiFark = kapasite - kalanMermi;
            MaxMermiSayisi -= aradakiFark;
            kalanMermi = kapasite;
            ToplamMermiSayisiText.text = MaxMermiSayisi.ToString();
            KalanMermiSayisiText.text = kalanMermi.ToString();
        }
    }
    void StartLoad()
    {
        MaxMermiSayisi = PlayerPrefs.GetInt(Silah_Adi + "_Mermi", 50); // Varsay˝lan 50 olsun
        kalanMermi = PlayerPrefs.GetInt(Silah_Adi + "_KalanMermi", 0); // Varsay˝lan 0 olsun

        // ﬁarjˆr¸ doldur ve kalan mermiyi ayarla
        if (kalanMermi < kapasite)
        {
            int doldurulacakMermi = Math.Min(kapasite - kalanMermi, MaxMermiSayisi);
            kalanMermi += doldurulacakMermi;
            MaxMermiSayisi -= doldurulacakMermi;
        }

        // UI G¸ncelle
        ToplamMermiSayisiText.text = MaxMermiSayisi.ToString();
        KalanMermiSayisiText.text = kalanMermi.ToString();

        // ﬁarjˆr bilgilerini PlayerPrefs'e kaydet
        PlayerPrefs.SetInt(Silah_Adi + "_Mermi", MaxMermiSayisi);
        PlayerPrefs.SetInt(Silah_Adi + "_KalanMermi", kalanMermi);
    }
    void AtesEt()
    {

        GameObject obje = Instantiate(Kovan_Objesi, Kovan_Cikis_Noktasi.transform.position, Kovan_Cikis_Noktasi.transform.rotation);
        Rigidbody rigit = obje.GetComponent<Rigidbody>();
        rigit.AddRelativeForce(new Vector3(-10f, 1, 0) * 30);

        atesSesi.Play();
        atesEfekti.Play();
        animatorum.Play("FireAnim");

        kalanMermi--;
        KalanMermiSayisiText.text = kalanMermi.ToString();

        RaycastHit hit;
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, Menzil))
        {
            if (hit.transform.tag == "Dusman")
            {
                ParticleSystem particle = Instantiate(kanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(particle.gameObject, particle.main.duration * 2f);
            }
            else if (hit.transform.tag == "Devrilebilir")
            {
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.AddForce(benimCam.transform.forward * 10);

            }
            else
            {
                ParticleSystem particle = Instantiate(mermi›zi, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(particle.gameObject, particle.main.duration * 2f);
            }
            Debug.Log("Hit: " + hit.transform.name);
        }
    }
    void AmmoKayit(string guntype, int ammo_number)
    {
        switch (guntype)
        {
            case "Taramali":
                break;
            case "Pompali":
                break;
            case "Magnum":
                MaxMermiSayisi += ammo_number;
                PlayerPrefs.SetInt("Taramali_Mermi", MaxMermiSayisi);
                break;
            case "Sniper":
                break;
        }
    }
}
