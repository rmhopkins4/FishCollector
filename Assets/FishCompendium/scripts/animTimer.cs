using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("middle"))
        {
            //message.Equals("dummy");
            //Do things based on middle
            flipControl.halfDone = true;
        }
        if (message.Equals("ended"))
        {
            //message.Equals("dummy");
            // Do other things based on an attack ending.
            flipControl.fullDone = true;
        }
    }
}
