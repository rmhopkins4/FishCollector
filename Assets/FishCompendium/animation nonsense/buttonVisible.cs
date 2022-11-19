using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonVisible : MonoBehaviour
{

    public bool buttonLeft;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //currently you see [(2x group) - 1 and (2x group)]
        sr.color = new Color(1, 1, 1, 0);

        if(buttonLeft)
        {
            for(int i = 0; i < (flipControl.pageGroup * 2) - 2; i++)
            {
                if(GlobalFishIndexHandler.fishes[i] == 1)
                {
                    sr.color = new Color(1, 1, 1, 1);
                }
            }
        }
        else
        {
            for (int i = 2 * flipControl.pageGroup; i <= 38; i++)
            {
                if (GlobalFishIndexHandler.fishes[i] == 1)
                {
                    sr.color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
}
