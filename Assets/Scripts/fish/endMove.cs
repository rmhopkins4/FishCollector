using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endMove : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        if (dontove)
            return;
        rb = gameObject.GetComponent<Rigidbody2D>();
        point = pointPicker();
        animator = GetComponent<Animator>();

        animator.SetBool("isAnimating", true);
    }
    Rigidbody2D rb;
    Vector3 point;
    [SerializeField]
    float speed, tamerFactor,maxRange;
    [SerializeField]
    public int index;
    [SerializeField]
    bool dontove;
    void Update()
    {
        if(dontove)
            return;
        if (Vector2.Distance(transform.position, point) > 15)
        {

            //transform.up = Vector2.SmoothDamp(transform.up, point-transform.position,ref velDamp,0.7f);
            transform.up = Vector2.Lerp(transform.up, point - transform.position, 1 / (speed * tamerFactor));
            rb.velocity = transform.up * speed;
            if (rb.velocity == Vector2.zero)
            {
                point = pointPicker();
            }
            Debug.DrawRay(transform.position, point - transform.position);
        }
        else
        {
            point = pointPicker();
            //print(pointPicker());
        }
    }
    [SerializeField]
    float xMin=-170, xMax=190, yMin=-190, yMax=100;
    Vector2 pointPicker()
    {

        //do a pos
        Vector3 pos = Vector3.zero;


        for (int i = 0; i < 1000; i++)
        {

            pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax)); //pick random position within range
            RaycastHit2D ray = Physics2D.Raycast(transform.position, pos - transform.position, Vector2.Distance(transform.position, pos));//throw a ray there

            if ((ray.collider == null || ray.collider.tag != "ground") && Vector2.Distance(pos, transform.position) < maxRange) //if it doesnt hit anything
            {
                return pos; //ye found yerself a point
            }
            else
            {
                Debug.DrawRay(transform.position, pos - transform.position); //draw a ray for funzies 
            }
        }
        Destroy(gameObject); //oh it took you more than 100? stinky fish.
        return Vector2.zero; //filler line. needs to be here so there isnt a compiler error

    }
}
