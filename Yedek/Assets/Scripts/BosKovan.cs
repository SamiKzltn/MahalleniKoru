using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosKovan : MonoBehaviour
{
    AudioSource yereDusmeSesi;
    // Start is called before the first frame update
    void Start()
    {
        yereDusmeSesi = GetComponent<AudioSource>();
        Destroy(gameObject,2f);
    }

    private void OnCollisionEnter(Collision collison){
        if(collison.gameObject.CompareTag("Yollar")){
            yereDusmeSesi.Play();

            if(!yereDusmeSesi.isPlaying){
                Destroy(gameObject,1f);
            }
        }
    }
}
