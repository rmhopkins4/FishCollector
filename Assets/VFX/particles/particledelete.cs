using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particledelete : MonoBehaviour
{
    float deleteCounter;

    // Start is called before the first frame update
    void Start()
    {
        deleteCounter = GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        deleteCounter -= Time.deltaTime;
        if(deleteCounter <= 0)
        {
            Destroy(gameObject);
        }
    }
}
