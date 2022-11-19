using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerDataController pdc;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pdc = GameObject.Find("TamerPlayer").GetComponent<PlayerDataController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > 0)
        {
            rb.gravityScale = 1;
        }
        else
        {
            rb.gravityScale = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "attackHitbox" || collision.gameObject.tag == "magicHitbox")
        {
            pdc.ChangeMagic(100);
            Destroy(gameObject);
        }
    }
}
