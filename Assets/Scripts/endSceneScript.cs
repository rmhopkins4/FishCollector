using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endSceneScript : MonoBehaviour
{
    void Start(){
        //GameObject.Find("GlobalHandler").GetComponent<LightChanger>().enabled=false;
        foreach (GameObject fish in GameObject.FindGameObjectsWithTag("endFish"))
        {
            if ((GlobalFishIndexHandler.fishes!=null)&&GlobalFishIndexHandler.fishes[fish.GetComponent<endMove>().index]==0)
            {
                Destroy(fish);
            }
        }
    }
}
