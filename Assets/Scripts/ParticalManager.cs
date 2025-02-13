using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalManager : MonoBehaviour
{
    public static ParticalManager Instance;

    public ParticleSystem AtesEfekti;
    public ParticleSystem Mermi›zi;
    public ParticleSystem kanEfekti;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    public void FireEffect(Vector3 position, Quaternion rotation,Transform parent = null)
    {
        ParticleSystem efekt = Instantiate(AtesEfekti, position, rotation);
        efekt.transform.SetParent(parent);
        efekt.Play();
        Destroy(efekt.gameObject, efekt.main.duration);
    }
    public void BloodHitParticalls(RaycastHit hit) 
    {
        ParticleSystem particle = Instantiate(kanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particle.gameObject, particle.main.duration / 10);
    }
    public void ObjectHitParticalls(RaycastHit hit)
    {
        ParticleSystem particle = Instantiate(Mermi›zi, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particle.gameObject, particle.main.duration / 10);
    }
}
