using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidgerController : MonoBehaviour
{
    //animation components 
    Animator animator;
    

    //fear bool
    public bool isScared = false;

    int index = 0;


    //navigation
    Transform tr;
    Rigidbody2D rb;
    Vector2 desiredPosition;
    Vector2 currentPosition;

    public Transform playerPositon;
	
	//public Light2D squidgerLight;

    public GlobalFishIndexHandler gb;


    public GameObject pp;


    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        playerPositon = GameObject.Find("TamerPlayer").GetComponent<Transform>();
        gb = GameObject.Find("Global Handler").GetComponent<GlobalFishIndexHandler>();
        animator = GetComponent<Animator>();


        desiredPosition = new Vector2(currentPosition.x + Random.Range(-20, 20), currentPosition.y + Random.Range(-5, 5));



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
     

        //cases: hide, swim around randomly avoid walls.
        if (Vector2.Distance(tr.position,playerPositon.position) <15)
        {
            animator.SetBool("isAnimating", false);
            animator.SetBool("IsHiding", true);
        }
        else
        {
            animator.SetBool("IsHiding", false);
            animator.SetBool("isAnimating", true);

            

            currentPosition = transform.position;
            if(desiredPosition.y > 1)
            {
                desiredPosition.y -= 1;
            }
            tr.up = Vector2.Lerp(tr.up,desiredPosition - currentPosition,Time.deltaTime/4);
            if (Vector2.Distance(transform.position, desiredPosition) > 3)
            {
                
                if (rb.velocity.magnitude < 5)
                {
                    rb.AddForce(tr.up * Time.deltaTime * 500);
                }
                

            }
            else
            {
                desiredPosition = new Vector2(currentPosition.x + Random.Range(-20, 20), currentPosition.y + Random.Range(-5, 0));
            }


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
	    int r = Random.Range(-1,1);
	    if(r==0){
		rb.AddTorque(-100);
	    }
	    else
		rb.AddTorque(100);
            
	    desiredPosition = new Vector2(currentPosition.x + Random.Range(-2, 2), currentPosition.y + Random.Range(-2, 2));
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "attackHitbox")
        {
            Instantiate(pp,tr.position, Quaternion.identity);
            gb.fishCaught(index);
            Destroy(gameObject);
        }
    }
    
}
