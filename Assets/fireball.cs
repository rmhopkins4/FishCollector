using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    ParticleSystem s1;
    public PlayerDataController pdc1;
    // Start is called before the first frame update
    void Awake()
    {
        s1 = gameObject.GetComponent<ParticleSystem>();
        //s.Play();
        pdc1 = GameObject.Find("TamerPlayer").GetComponent<PlayerDataController>();
        //transform.up = GameObject.Find("TamerPlayer").GetComponent<Transform>().position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * 50f;
        if (s1.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pdc1.takeDamage(5);
            Destroy(gameObject);
        }
    }
}
