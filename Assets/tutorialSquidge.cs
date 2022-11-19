using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSquidge : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Color c = new Color();
        c.r = Random.Range(0.0f, 1.0f);
        c.g = Random.Range(0.0f, 1.0f);
        c.b = Random.Range(0.0f, 1.0f);
        c.a = 1;
        gameObject.GetComponent<SpriteRenderer>().color = c;
    }

    Vector2 currentPosition;
    Vector2 desiredPosition;
    Vector2 velDamp = new Vector2(10, 10);
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("IsHiding", false);
        animator.SetBool("isAnimating", true);
        currentPosition = transform.position;
        transform.up = Vector2.SmoothDamp(transform.up, desiredPosition - currentPosition, ref velDamp, 0.5f);
        while (desiredPosition.y > 0)
        {
            desiredPosition = new Vector2(currentPosition.x + Random.Range(-20, 20), currentPosition.y + Random.Range(-5, 0));
        }
        if (Vector2.Distance(transform.position, desiredPosition) > 3)
        {


            rb.AddForce(transform.up * 10);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 1);

        }
        else
        {
            desiredPosition = new Vector2(currentPosition.x + Random.Range(-20, 20), currentPosition.y + Random.Range(-5, 5));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "magicHitbox" || collision.gameObject.tag == "attackHitbox") ) //caught
        {
           
            Destroy(gameObject); //destroy the fish
        }
        
    }
}
