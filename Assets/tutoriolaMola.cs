using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutoriolaMola : MonoBehaviour
{
    Transform tr;
    public GameObject pp;
    public GlobalFishIndexHandler gb;
    public PlayerDataController pdc;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        gb = GameObject.Find("Global Handler").GetComponent<GlobalFishIndexHandler>();
        pdc = GameObject.Find("TamerPlayer").GetComponent<PlayerDataController>();

        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attackHitbox")
        {
            Instantiate(pp, tr.position, Quaternion.identity);
            pdc.addPoints(500);
            Destroy(gameObject);
        }
    }
}
