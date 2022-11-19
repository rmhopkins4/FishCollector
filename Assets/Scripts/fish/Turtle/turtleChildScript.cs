using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtleChildScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag=="Player") {
            if(gameObject.name=="pRad")
                turle_script.patrol=false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag=="Player") {
            if(gameObject.name=="pRad")
                turle_script.patrol=false;
        }
    }
}
