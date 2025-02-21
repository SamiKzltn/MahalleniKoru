using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour, IDamageable
{
    private Rigidbody rb;
    public Camera benimCam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        benimCam = Camera.main;
    }

    public bool Hit(int damage)
    {
        if (rb != null)
        {
            rb.AddForce(benimCam.transform.forward * damage);
        }
        return true;
    }
}
