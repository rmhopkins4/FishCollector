using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScrip : MonoBehaviour
{
    [SerializeField]//stores zone starts and ends 
    Vector2[] yMinMax = new Vector2[5];
    [SerializeField]
    Vector2 xMinMax = new Vector2(-10, 10);
    [SerializeField]//stores all fish that can spawn in an area
    public List<GameObject> sunFish, twiFish, midFish, abyFish, treFish, trash;
    [SerializeField]
    GameObject cloud, squig, squiglet, lump;
    /*
     *floats of min and max y values
     * 
     *2d array of fish, one per area, serilzed to show in unity
     * 
     *check number of fish currently spawned, all objects tagged with fish
     *chck how far away all the fish are, if too far despawn
     *
     *no need to keep track of health
     *
     *check if thng is gonna spawn inside wall
     */
    GameObject pl;
    List<GameObject> guys, pogs;
    PlayerInputActions playerInputActions;
    bool isSpawning;
    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
        playerInputActions = new PlayerInputActions();
        guys = new List<GameObject>(GameObject.FindGameObjectsWithTag("fish"));

    }
    [SerializeField]
    float killRadius = 300;
    void Update()
    {
        guys = new List<GameObject>(GameObject.FindGameObjectsWithTag("fish"));
        if (guys.Count < 30)
            spawnGuy();
        foreach (GameObject guy in GameObject.FindGameObjectsWithTag("fish"))
        {
            if (Vector2.Distance(guy.transform.position, pl.transform.position) > killRadius)
            {
                guys.Remove(guy);
                GameObject.Destroy(guy);
            }
        }


        pogs = new List<GameObject>(GameObject.FindGameObjectsWithTag("trash"));
        if (pogs.Count < 5)
            spawnTrash();
        foreach (GameObject trash in pogs)
        {
            if (Vector2.Distance(trash.transform.position, pl.transform.position) > killRadius)
            {
                pogs.Remove(trash);
                GameObject.Destroy(trash);
            }
        }

        int squidCount = 0;
        int squidletCount = 0;
        int cloudCount = 0;
        int lumpCount = 0;
        foreach (GameObject fish in GameObject.FindObjectsOfType<GameObject>())
        {
            if (fish.activeInHierarchy)
            {
                if (fish.name.Contains("Lumper"))
                {
                    lumpCount++;
                }
                    
                else if (fish.name.Contains("Cloudler"))
                {
                    cloudCount++;
                }
                    
                else if (fish.name.Contains("Squidglet"))
                {
                    squidletCount++;
                }
                    
                else if (fish.name.Contains("squidger"))
                {
                    squidCount++;
                }
                    
            }
            
        }
       // print((squidCount < 5) + " " + (squidletCount < 5) + " " + (lumpCount < 5) + " " + (cloudCount < 5));
        if (squidCount < 10)
        {
            squidgSpawn();
        }
            
        if (squidletCount < 3)
        {
            squidgletSpawn();
        }
            
        if (lumpCount < 5)
        {
            lumperSpawn();
        }
           
        if (cloudCount < 5)
        {
            cloudlerSpawn();
        }
            
    }
    void spawnGuy(float x1 = 30, float x2 = 250)
    {
        Vector2 pos = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.x, ((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.y));//grab a random point in a cirlce raidus spawn distance away from the player
        Collider2D curCol = Physics2D.OverlapCircle(pos, 2);//gets first collider in a cirlce about pos, radius 2(soft value
        if (pos.y > -5)
            pos.y = -5;
        if (pos.x < -300)
            pos.x = 290;
        else if (pos.x > 370)
            pos.x = 350;
        for (int i = 0; i < 20; i++)
        {//in a for to prevent too much wsted time spawning in new fish 
         //pos = (Random.insideUnitCircle * spawnDistance) * transform.position;
            pos = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.x, ((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.y));//grab a random point in a cirlce raidus spawn distance away from the player
            if (pos.y > -5)
                pos.y = -5;
            if (pos.x < -300)
                pos.x = 290;
            else if (pos.x > 370)
                pos.x = 350;
            curCol = Physics2D.OverlapCircle(pos, 2);
            if (curCol == null || curCol.gameObject.tag != "ground")
                break;
        }
        if (curCol == null || curCol.gameObject.tag != "ground")
        {

            //int curDepth = Mathf.Abs(Mathf.FloorToInt(pos.y / 250));
            int curDepth = Mathf.Abs((int)pos.y) / 250;
            switch (curDepth)
            {
                case 0:
                    guys.Add((GameObject)Instantiate(sunFish[Random.Range(0, sunFish.Count)], pos, Quaternion.Euler(Vector3.zero)));
                    break;
                case 1:
                    guys.Add((GameObject)Instantiate(twiFish[Random.Range(0, twiFish.Count)], pos, Quaternion.Euler(Vector3.zero)));
                    break;
                case 2:
                    guys.Add((GameObject)Instantiate(midFish[Random.Range(0, midFish.Count)], pos, Quaternion.Euler(Vector3.zero)));
                    break;
                case 3:
                    guys.Add((GameObject)Instantiate(abyFish[Random.Range(0, abyFish.Count)], pos, Quaternion.Euler(Vector3.zero)));
                    break;
                case 4:
                    pos.x = Random.Range(-30, 100);
                    guys.Add((GameObject)Instantiate(treFish[Random.Range(0, treFish.Count)], pos, Quaternion.Euler(Vector3.zero)));
                    break;
                case 5:
                    pos.x = Random.Range(-30, 100);
                    guys.Add((GameObject)Instantiate(treFish[Random.Range(0, treFish.Count)], pos, Quaternion.Euler(Vector3.zero)));
                    break;
                default:
                    guys.Add((GameObject)Instantiate(sunFish[Random.Range(0, treFish.Count)], pos, Quaternion.Euler(Vector3.zero)));
                    break;
            }
            guys[guys.Count - 1].transform.parent = GameObject.Find("...FISHHOLDER").GetComponent<Transform>();
        }
    }
    void spawnTrash(float x1 = 60, float x2 = 180){
        Vector2 pos = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.x, ((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.y));//grab a random point in a cirlce raidus spawn distance away from the player
        Collider2D curCol = Physics2D.OverlapCircle(pos, 2);//gets first collider in a cirlce about pos, radius 2(soft value
        if (pos.y > -5)
            pos.y = -5;
        if (pos.x < -300)
            pos.x = 290;
        else if (pos.y > 370)
            pos.x = 350;
        for (int i = 0; i < 20; i++)
        {//in a for to prevent too much wsted time spawning in new fish 
         //pos = (Random.insideUnitCircle * spawnDistance) * transform.position;
            pos = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.x, ((Random.Range(0, 2) * 2 - 1) * Random.Range(x1, x2) + pl.transform.position.y));//grab a random point in a cirlce raidus spawn distance away from the player
            if (pos.y > -5)
                continue;
            if (pos.x < -300)
                continue;
            else if (pos.y < 370)
                continue;
            curCol = Physics2D.OverlapCircle(pos, 2);
            if (curCol == null || curCol.gameObject.tag != "ground")
                break;
        }
        if (curCol == null || curCol.gameObject.tag != "ground")
        {
            Instantiate(trash[Random.Range(0, trash.Count - 1)], pos,Quaternion.Euler(Vector3.zero));
        }
    }
    void cloudlerSpawn(){
        Vector2 pos = new Vector2(Random.Range(-300, 300), Random.Range(30, 100));
        Instantiate(cloud, pos, Quaternion.Euler(Vector3.zero));
    }
    [SerializeField]
    List<Vector2> squidgerPoints, lumperPoints, squidgeletPoints;
    void squidgSpawn(){
        Vector2 pos = squidgerPoints[Random.Range(0, squidgerPoints.Count - 1)];
        Collider2D curCol = Physics2D.OverlapCircle(pos, 2);
        if (curCol == null)
            Instantiate(squig, pos, Quaternion.Euler(Vector3.zero));
    }
    void squidgletSpawn() {
        Vector2 pos = squidgeletPoints[Random.Range(0, squidgeletPoints.Count - 1)];
        Collider2D curCol = Physics2D.OverlapCircle(pos, 2);
        if (curCol == null)
            Instantiate(squiglet, pos, Quaternion.Euler(Vector3.zero));
    }
    void lumperSpawn(){
        Vector2 pos = lumperPoints[Random.Range(0, lumperPoints.Count - 1)];
        Collider2D curCol = Physics2D.OverlapCircle(pos, 2);
        if (curCol == null)
            Instantiate(lump, pos, Quaternion.Euler(Vector3.zero));
    }
}