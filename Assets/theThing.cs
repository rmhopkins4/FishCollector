using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theThing : MonoBehaviour
{
    public DistControl postProcessControl;
    Transform player;
    
    public GameObject thing;
    Rigidbody2D rb;
    public GameObject[] trashPrefab;
    public GameObject bombPrefab;
    public GameObject fireBallPrefab;
    public Transform bpoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("TamerPlayer").GetComponent<Transform>() ;
        rb = thing.GetComponent<Rigidbody2D>();
        rb.angularVelocity = 50f;
        postProcessControl = GameObject.Find("Post-Process Volume").GetComponent<DistControl>();
        //rb.velocity = thing.GetComponent<Transform>().up*Random.Range(-1,1)*20;
    }
    int poggie = 0;
    int poggie2 = 0;
    int poggie3 = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Vector2.Distance(transform.position, player.position)<60)
        {
            postProcessControl.nearThing(Vector2.Distance(transform.position, player.position));
            
            transform.Rotate(0f, 0f, 10f, Space.Self);
            poggie++;
            poggie2++;
            poggie3++;
            int trashy2 = Random.Range(0, 5);
            if (poggie >= trashy2)
            {
                int trashy = Random.Range(0, 4);
                GameObject ball = Instantiate(trashPrefab[trashy], transform.position, Quaternion.identity);
                ball.GetComponent<Transform>().up = gameObject.transform.up;
                ball.GetComponent<Rigidbody2D>().velocity = transform.up * 50*trashy2;
                poggie = 0;
            }
            if (poggie2 >= 10)
            {
                GameObject ball = Instantiate(bombPrefab, bpoint.position, Quaternion.identity);
                ball.GetComponent<Transform>().up = gameObject.transform.up;
                ball.GetComponent<Rigidbody2D>().velocity = transform.up * 10;
                poggie2 = 0;
            }
            if (poggie3 >= 50)
            {
                GameObject ball = Instantiate(fireBallPrefab, bpoint.position, Quaternion.identity);
                ball.GetComponent<Transform>().up = gameObject.transform.up;
                ball.GetComponent<Rigidbody2D>().velocity = transform.up * 30;
                rb.AddTorque(Random.Range(-30f, 30f));
                rb.AddForce(thing.GetComponent<Transform>().up * Random.Range(-1, 1));
                poggie3 = 0;
            }

        }
    }
}
