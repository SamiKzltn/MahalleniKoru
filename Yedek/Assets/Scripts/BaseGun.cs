using UnityEngine;
using UnityEngine.UI;

public abstract class BaseGun : MonoBehaviour
{
    [Header("AYARLAR")]
    public bool atesEdebilir;
    public float IcerdenAtesSikligi;
    public float View;
    public float zoomView = 20;

    [Header("SESLER")]
    public AudioClip atesSesi;
    public AudioClip reloadSesi;
    public AudioClip emptySesi;
    public AudioClip AmmoTakeSound;

    [Header("Images")]
    public GameObject Aim;
    public GameObject Scope;

    [Header("Silah Ayarlari")]
    [SerializeField] protected int MaxMermiSayisi;
    [SerializeField] protected int kalanMermi = 1;

    public GameObject Kovan_Objesi;
    public GameObject Kovan_Cikis_Noktasi;

    public AmmoBoxSpawn AmmoBoxSpawner;

    [Header("DÝÐER")]
    public Camera benimCam;
    [SerializeField]protected Animator animatorum;
    public Transform muzzleTransform;

    protected bool bosSesCaldi = false;
    protected float sonSesZamani = 0f;
    protected float sesBeklemeSuresi = 1f;
    [SerializeField] protected KeyCode m_fireKey = KeyCode.Mouse0;
    [SerializeField] protected KeyCode m_reloadKey = KeyCode.R;
    [SerializeField] protected KeyCode m_lootKey = KeyCode.E;
    [SerializeField] protected KeyCode m_zoomKey = KeyCode.Mouse1;
    [SerializeField] protected Gun_Scriptable Gun_Scriptable;

    protected virtual void Start()
    {
        Initialize();
    }

    protected abstract void Initialize();
    protected virtual void Update()
    {
        InputControl();
    }
    protected virtual void InputControl()
    {
        if (Input.GetKey(m_fireKey))
        {
            FireControl();
        }
        else if (Input.GetKey(m_reloadKey))
        {
            Reload();
        }

        if (Input.GetKey(m_lootKey))
        {
            Loot();
        }
        if (Input.GetKeyDown(m_zoomKey))
        {
            ZoomIn();
        }
        else if (Input.GetKeyUp(m_zoomKey))
        {
            ZoomOut();
        }
    }
    protected virtual void FireControl()
    {
        bool status = (atesEdebilir && Time.time > IcerdenAtesSikligi);
        bool ammoStatus = FireAmmoControl();
        if (status && ammoStatus)
        {
            IcerdenAtesSikligi = Time.time + Gun_Scriptable.shotFrequency;
            Fire();
        }
    }
    protected virtual bool FireAmmoControl()
    {
        bool status = (kalanMermi > 0);
        if (!status)
        {
            OnEmptyFireAttempt();
        }
        return status;
    }
    protected abstract void OnEmptyFireAttempt();
    protected abstract void Fire();
    protected abstract void Reload();
    protected abstract void Loot();
    protected abstract void ZoomIn();
    protected abstract void ZoomOut();



}
