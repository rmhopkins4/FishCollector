using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathparticleScript : MonoBehaviour
{
    ParticleSystem s;
    // Start is called before the first frame update
    void Awake()
    {
        s = gameObject.GetComponent<ParticleSystem>();
        //s.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(s.isPlaying == false){
            Destroy(gameObject);
        }
    }
}
