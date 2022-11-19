using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerhandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Debug 0"))
        {
            GameObject.Find("A").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 0"))
        {
            GameObject.Find("A").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 1"))
        {
            GameObject.Find("B").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 1"))
        {
            GameObject.Find("B").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 2"))
        {
            GameObject.Find("X").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 2"))
        {
            GameObject.Find("X").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 3"))
        {
            GameObject.Find("Y").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 3"))
        {
            GameObject.Find("Y").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 4"))
        {
            GameObject.Find("LB 1").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 4"))
        {
            GameObject.Find("LB 1").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 5"))
        {
            GameObject.Find("RB 1").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 5"))
        {
            GameObject.Find("RB 1").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 6"))
        {
            GameObject.Find("Select").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 6"))
        {
            GameObject.Find("Select").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 7"))
        {
            GameObject.Find("Start").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 7"))
        {
            GameObject.Find("Start").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 8"))
        {
            GameObject.Find("L Stick").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 8"))
        {
            GameObject.Find("L Stick").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (Input.GetButton("Debug 9"))
        {
            GameObject.Find("R Stick").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetButtonUp("Debug 9"))
        {
            GameObject.Find("R Stick").GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
