using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishSpawner : MonoBehaviour
{
    public List<GameObject> bossFish;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int s1=1000, s2=1000, s3=1000, s4=1000, s5 = 10000, s6=1000;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!checkIfAlive("Turtle(Clone)"))
        {
            s4++;
            if (s4 >= 1000)
            {
                spawn(bossFish[3], -100);
                s4 = 0;
            }
        }
        if (!checkIfAlive("Rapier Fish(Clone)"))
        {
            s1++;
            if (s1 >= 1000)
            {
                spawn(bossFish[0], -300);
                s1 = 0;
            }
        }
        if (!checkIfAlive("Midnight Squid(Clone)"))
        {
            s2++;
            if (s2 >= 1000)
            {
                spawn(bossFish[1], -600);
                s2 = 0;
            }
                
        }
        if (!checkIfAlive("Mouth Fish(Clone)"))
        {
            s3++;
            if (s3 >= 1000)
            {
                spawn(bossFish[2], -850);
                s3 = 0;
            }
        }
        if (!checkIfAlive("The Thing(Clone)"))
        {
            s5++;
            if (s5 >= 1000)
            {
                Instantiate(bossFish[4], new Vector3(40, -1300, 0), Quaternion.identity);
                s5 = 0;
            }
        }
        if (!checkIfAlive("glowhale(Clone)"))
        {
            s6++;
            if (s6 >= 1000)
            {
                Instantiate(bossFish[5], new Vector3(48, -880, 0), Quaternion.identity);
                s6 = 0;
            }
        }
        
    }
    bool checkIfAlive(string nameOfBoss)
    {
        if (GameObject.Find(nameOfBoss)!=null)
        {
            return true;
        }
        return false;
    }
    void spawn(GameObject b,int depth)
    {
        Instantiate(b, new Vector3(120, depth, 0), Quaternion.identity);
        
    }
}
