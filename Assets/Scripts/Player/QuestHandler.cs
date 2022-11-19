using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class QuestHandler : MonoBehaviour
{
    public TextMeshProUGUI questBox;

    string[] fishNames = new string[39];
	
	string path = "Assets/FishCompendium/fishInfo.txt";

    public GameObject questDoneParticle;

    public class Quest
    {
        public int type;
        public int direct;
        public int count;
        public int points;
        bool completed;

        public Quest(int typein)
        {
            type = typein;

            if (type <= 3)
            {
                //create a specific fish quest
                direct = Random.Range(1, 34); //pick a random normal fish.
                count = Random.Range(2, 6); //catch between 2-5 of the fish.
                points = count * 100;
            }
            else if (type <= 6)
            {
                //create a zone fish quest
                direct = Random.Range(0, 5); //pick a random zone.
                count = Random.Range(6, 11); //catch between 6 and ten of the zoned fish.
                points = count * 20;
            }
            else if (type <= 9)
            {
                //create a misc quest
                direct = Random.Range(0, 4); //pick one of 4 random quests.
                count = 1; //pick either one or two.
                points = 250;
            }
            else if (type <= 10)
            {
                //create a large fish quest
                direct = Random.Range(34, 39); //pick a random large fish.
                count = 1; //always just catch one.
                points = (direct - 34) * 250 + 500; 
            }
        }
        public Quest(int typein, int directin, int countin)
        {
            type = typein;
            direct = directin;
            count = countin;
        }
    }

    Quest q1;
    Quest q2;
    Quest q3;
    Quest q4;

    string str1, str2, str3, str4;
	
	public GameObject image1, image2, image3, image4;
	
	public GameObject sunlight, twilight, midnight, abyss, trench, hydrothermalVent, goTo, goldDash, Comp;

    // Start is called before the first frame update
    void Start()
    {
        #region fish names
        fishNames[0] = "null fish";
        fishNames[1] = "Squidger";
        fishNames[2] = "Orange Dreamfin";
        fishNames[3] = "Cheeseburger";
        fishNames[4] = "Roundfish";
        fishNames[5] = "Squidgelet";
        fishNames[6] = "Gloober";
        fishNames[7] = "Fish Rosé";
        fishNames[8] = "Deltta Fish";
        fishNames[9] = "Aki-Aki";
        fishNames[10] = "Peter";
        fishNames[11] = "Cloudler";
        fishNames[12] = "Loafus";
        fishNames[13] = "Hat Squid";
        fishNames[14] = "Moonfish";
        fishNames[15] = "Gwomp";
        fishNames[16] = "Gloober 2";
        fishNames[17] = "Sludgler";
        fishNames[18] = "Gloober 3";
        fishNames[19] = "Whaledon";
        fishNames[20] = "Junker Eel";
        fishNames[21] = "Dyson's Vacuum Fish";
        fishNames[22] = "Oomber";
        fishNames[23] = "Junk Grobbler";
        fishNames[24] = "Flounder";
        fishNames[25] = "Radiated Junk Grobbler";
        fishNames[26] = "Lumper";
        fishNames[27] = "Narwhal";
        fishNames[28] = "Gwomp Grande";
        fishNames[29] = "Glowhale";
        fishNames[30] = "Regdiuqs";
        fishNames[31] = "Gamma Glorg";
        fishNames[32] = "Great Red Eel";
        fishNames[33] = "Large Angler";
        fishNames[34] = "Tortuuwa";
        fishNames[35] = "Rapier Fish";
        fishNames[36] = "Midnight Squid";
        fishNames[37] = "Mouth Fish";
        fishNames[38] = "The Thing";

        #endregion

        q1 = new Quest(Random.Range(1, 11));
        q2 = new Quest(Random.Range(1, 11));
        q3 = new Quest(Random.Range(1, 11));
        q4 = new Quest(Random.Range(1, 11));

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            q1.count = 0;
        }

        //MAKING TEXT
        if (q1.type <= 3)
        {
            str1 = "Catch " + q1.count + " x " + fishNames[q1.direct] + "! (" + q1.points + ")" + "\n" + "\n";
			image1.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q1.direct, 3));
        }
        else if(q1.type <= 6)
        {
            if (q1.direct == 0) //if sunlight
            {
                str1 = "Catch " + q1.count + " fish in the Sunlight Zone! (" + q1.points + ")" + "\n" + "\n"; 
				image1.GetComponent<Image>().sprite = sunlight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q1.direct == 1) //if twilight
            {
                str1 = "Catch " + q1.count + " fish in the Twilight Zone! (" + q1.points + ")" + "\n" + "\n";
				image1.GetComponent<Image>().sprite = twilight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q1.direct == 2) //if midnight
            {
                str1 = "Catch " + q1.count + " fish in the Midnight Zone! (" + q1.points + ")" + "\n" + "\n";
				image1.GetComponent<Image>().sprite = midnight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q1.direct == 3) //if abyssal
            {
                str1 = "Catch " + q1.count + " fish in the Abyssal Zone! (" + q1.points + ")" + "\n" + "\n";
				image1.GetComponent<Image>().sprite = abyss.GetComponent<SpriteRenderer>().sprite;
            }
            else //if trench
            {
                str1 = "Quest 1: Catch " + q1.count + " fish in the Hadal Zone! (" + q1.points + ")" + "\n" + "\n";
				image1.GetComponent<Image>().sprite = trench.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else if(q1.type <= 9)
        {
            if(q1.direct == 0)
            {
                str1 = "Go to the bottom of the trench! (" + q1.points + ")" + "\n" + "\n";
                //image is bottom of trench image
                image1.GetComponent<Image>().sprite = goTo.GetComponent<SpriteRenderer>().sprite;
            }
            else if(q1.direct == 1)
            {
                str1 = "Get a perfect Golden Dash! (" + q1.points + ")" + "\n" + "\n";
                //image is gold dash image
                image1.GetComponent<Image>().sprite = goldDash.GetComponent<SpriteRenderer>().sprite;
            }
            else if(q1.direct == 2)
            {
                str1 = "Dash through a hydrothermal vent! Feel the warmth! (" + q1.points + ")" + "\n" + "\n";
				//image is vent image
				image1.GetComponent<Image>().sprite = hydrothermalVent.GetComponent<SpriteRenderer>().sprite;
            }
            else if(q1.direct == 3)
            {
                str1 = "Check your Fish Compendium for new entries! (" + q1.points + ")" + "\n" + "\n";
                //image is compendium image
                image1.GetComponent<Image>().sprite = Comp.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else
        {
            str1 = "Catch the great " + fishNames[q1.direct] + "! (" + q1.points + ")" + "\n" + "\n";
			image1.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q1.direct, 3));
        }

        if (q2.type <= 3)
        {
            str2 = "Catch " + q2.count + " x " + fishNames[q2.direct] + "! (" + q2.points + ")" + "\n" + "\n";
            image2.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q2.direct, 3));
        }
        else if (q2.type <= 6)
        {
            if (q2.direct == 0) //if sunlight
            {
                str2 = "Catch " + q2.count + " fish in the Sunlight Zone! (" + q2.points + ")" + "\n" + "\n";
                image2.GetComponent<Image>().sprite = sunlight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q2.direct == 1) //if twilight
            {
                str2 = "Catch " + q2.count + " fish in the Twilight Zone! (" + q2.points + ")" + "\n" + "\n";
                image2.GetComponent<Image>().sprite = twilight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q2.direct == 2) //if midnight
            {
                str2 = "Catch " + q2.count + " fish in the Midnight Zone! (" + q2.points + ")" + "\n" + "\n";
                image2.GetComponent<Image>().sprite = midnight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q2.direct == 3) //if abyssal
            {
                str2 = "Catch " + q2.count + " fish in the Abyssal Zone! (" + q2.points + ")" + "\n" + "\n";
                image2.GetComponent<Image>().sprite = abyss.GetComponent<SpriteRenderer>().sprite;
            }
            else //if trench
            {
                str2 = "Quest 1: Catch " + q2.count + " fish in the Hadal Zone! (" + q2.points + ")" + "\n" + "\n";
                image2.GetComponent<Image>().sprite = trench.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else if (q2.type <= 9)
        {
            if (q2.direct == 0)
            {
                str2 = "Go to the bottom of the trench! (" + q2.points + ")" + "\n" + "\n";
                //image is bottom of trench image
                image2.GetComponent<Image>().sprite = goTo.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q2.direct == 1)
            {
                str2 = "Get a perfect Golden Dash! (" + q2.points + ")" + "\n" + "\n";
                //image is gold dash image
                image2.GetComponent<Image>().sprite = goldDash.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q2.direct == 2)
            {
                str2 = "Dash through a hydrothermal vent! Feel the warmth! (" + q2.points + ")" + "\n" + "\n";
                //image is vent image
                image2.GetComponent<Image>().sprite = hydrothermalVent.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q2.direct == 3)
            {
                str2 = "Check your Fish Compendium for new entries! (" + q2.points + ")" + "\n" + "\n";
                //image is compendium image
                image2.GetComponent<Image>().sprite = Comp.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else
        {
            str2 = "Catch the great " + fishNames[q2.direct] + "! (" + q2.points + ")" + "\n" + "\n";
            image2.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q2.direct, 3));
        }

        if (q3.type <= 3)
        {
            str3 = "Catch " + q3.count + " x " + fishNames[q3.direct] + "! (" + q3.points + ")" + "\n" + "\n";
            image3.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q3.direct, 3));
        }
        else if (q3.type <= 6)
        {
            if (q3.direct == 0) //if sunlight
            {
                str3 = "Catch " + q3.count + " fish in the Sunlight Zone! (" + q3.points + ")" + "\n" + "\n";
                image3.GetComponent<Image>().sprite = sunlight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q3.direct == 1) //if twilight
            {
                str3 = "Catch " + q3.count + " fish in the Twilight Zone! (" + q3.points + ")" + "\n" + "\n";
                image3.GetComponent<Image>().sprite = twilight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q3.direct == 2) //if midnight
            {
                str3 = "Catch " + q3.count + " fish in the Midnight Zone! (" + q3.points + ")" + "\n" + "\n";
                image3.GetComponent<Image>().sprite = midnight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q3.direct == 3) //if abyssal
            {
                str3 = "Catch " + q3.count + " fish in the Abyssal Zone! (" + q3.points + ")" + "\n" + "\n";
                image3.GetComponent<Image>().sprite = abyss.GetComponent<SpriteRenderer>().sprite;
            }
            else //if trench
            {
                str3 = "Quest 1: Catch " + q3.count + " fish in the Hadal Zone! (" + q3.points + ")" + "\n" + "\n";
                image3.GetComponent<Image>().sprite = trench.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else if (q3.type <= 9)
        {
            if (q3.direct == 0)
            {
                str3 = "Go to the bottom of the trench! (" + q3.points + ")" + "\n" + "\n";
                //image is bottom of trench image
                image3.GetComponent<Image>().sprite = goTo.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q3.direct == 1)
            {
                str3 = "Get a perfect Golden Dash! (" + q3.points + ")" + "\n" + "\n";
                //image is gold dash image
                image3.GetComponent<Image>().sprite = goldDash.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q3.direct == 2)
            {
                str3 = "Dash through a hydrothermal vent! Feel the warmth! (" + q3.points + ")" + "\n" + "\n";
                //image is vent image
                image3.GetComponent<Image>().sprite = hydrothermalVent.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q3.direct == 3)
            {
                str3 = "Check your Fish Compendium for new entries! (" + q3.points + ")" + "\n" + "\n";
                //image is compendium image
                image3.GetComponent<Image>().sprite = Comp.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else
        {
            str3 = "Catch the great " + fishNames[q3.direct] + "! (" + q3.points + ")" + "\n" + "\n";
            image3.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q3.direct, 3));
        }

        if (q4.type <= 3)
        {
            str4 = "Catch " + q4.count + " x " + fishNames[q4.direct] + "! (" + q4.points + ")" + "\n" + "\n";
            image4.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q4.direct, 3));
        }
        else if (q4.type <= 6)
        {
            if (q4.direct == 0) //if sunlight
            {
                str4 = "Catch " + q4.count + " fish in the Sunlight Zone! (" + q4.points + ")" + "\n" + "\n";
                image4.GetComponent<Image>().sprite = sunlight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q4.direct == 1) //if twilight
            {
                str4 = "Catch " + q4.count + " fish in the Twilight Zone! (" + q4.points + ")" + "\n" + "\n";
                image1.GetComponent<Image>().sprite = twilight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q4.direct == 2) //if midnight
            {
                str4 = "Catch " + q4.count + " fish in the Midnight Zone! (" + q4.points + ")" + "\n" + "\n";
                image4.GetComponent<Image>().sprite = midnight.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q4.direct == 3) //if abyssal
            {
                str4 = "Catch " + q4.count + " fish in the Abyssal Zone! (" + q4.points + ")" + "\n" + "\n";
                image4.GetComponent<Image>().sprite = abyss.GetComponent<SpriteRenderer>().sprite;
            }
            else //if trench
            {
                str4 = "Quest 1: Catch " + q4.count + " fish in the Hadal Zone! (" + q4.points + ")" + "\n" + "\n";
                image4.GetComponent<Image>().sprite = trench.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else if (q4.type <= 9)
        {
            if (q4.direct == 0)
            {
                str4 = "Go to the bottom of the trench! (" + q4.points + ")" + "\n" + "\n";
                //image is bottom of trench image
                image4.GetComponent<Image>().sprite = goTo.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q4.direct == 1)
            {
                str4 = "Get a perfect Golden Dash! (" + q4.points + ")" + "\n" + "\n";
                //image is gold dash image
                image4.GetComponent<Image>().sprite = goldDash.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q4.direct == 2)
            {
                str4 = "Dash through a hydrothermal vent! Feel the warmth! (" + q4.points + ")" + "\n" + "\n";
                //image is vent image
                image4.GetComponent<Image>().sprite = hydrothermalVent.GetComponent<SpriteRenderer>().sprite;
            }
            else if (q4.direct == 3)
            {
                str4 = "Check your Fish Compendium for new entries! (" + q4.points + ")" + "\n" + "\n";
                //image is compendium image
                image4.GetComponent<Image>().sprite = Comp.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else
        {
            str4 = "Catch the great " + fishNames[q4.direct] + "! (" + q4.points + ")" + "\n" + "\n";
            image4.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(q4.direct, 3));
        }


        questBox.text = str1 + str2 + str3 + str4;

        //COUNT = 0
        if(q1.count <= 0)
        {
            //delete and make new q1.
            this.GetComponent<PlayerDataController>().ChangeMagic(q1.points);
            q1 = new Quest(Random.Range(1, 11));
            GameObject chiPar = Instantiate(questDoneParticle, image1.transform.position, Quaternion.identity);
            chiPar.transform.SetParent(image1.transform, true);
        }

        if (q2.count <= 0)
        {
            //delete and make new q2.
            this.GetComponent<PlayerDataController>().ChangeMagic(q2.points);
            q2 = new Quest(Random.Range(1, 11));
            GameObject chiPar = Instantiate(questDoneParticle, image2.transform.position, Quaternion.identity);
            chiPar.transform.SetParent(image2.transform, true);
        }

        if (q3.count <= 0)
        {
            //delete and make new q3.
            this.GetComponent<PlayerDataController>().ChangeMagic(q3.points);
            q3 = new Quest(Random.Range(1, 11));
            GameObject chiPar = Instantiate(questDoneParticle, image3.transform.position, Quaternion.identity);
            chiPar.transform.SetParent(image3.transform, true);
        }

        if (q4.count <= 0)
        {
            //delete and make new q4.
            this.GetComponent<PlayerDataController>().ChangeMagic(q4.points);
            q4 = new Quest(Random.Range(1, 11));
            GameObject chiPar = Instantiate(questDoneParticle, image4.transform.position, Quaternion.identity);
            chiPar.transform.SetParent(image4.transform, true);
        }
    }

    public int catchReceive(int index)
    {
        if (q1.type <= 3) //if it is a specific fish quest
        {
            if (q1.direct == index) //and they match
            {
                q1.count--;
            }
        }

        if(q1.type > 3 && q1.type <= 6) //if it is a zone quest
        {
            if(index < 12) //if sunlight
            {
                if(q1.direct == 0)
                {
                    q1.count--;
                }
            }
            else if(index < 18) //if twilight
            {
                if(q1.direct == 1)
                {
                    q1.count--;
                }
            }
            else if(index < 24) //if midnight
            {
                if(q1.direct == 2)
                {
                    q1.count--;
                }
            }
            else if(index < 30) //if abyssal
            {
                if(q1.direct == 3)
                {
                    q1.count--;
                }
            }
            else //if trench
            {
                if(q1.direct == 4)
                {
                    q1.count--;
                }
            }
        }
        if (q2.type <= 3) //if it is a specific fish quest
        {
            if (q2.direct == index) //and they match
            {
                q2.count--;
            }
        }

        if (q2.type > 3 && q2.type <= 6) //if it is a zone quest
        {
            if (index < 12) //if sunlight
            {
                if (q2.direct == 0)
                {
                    q2.count--;
                }
            }
            else if (index < 18) //if twilight
            {
                if (q2.direct == 1)
                {
                    q2.count--;
                }
            }
            else if (index < 24) //if midnight
            {
                if (q2.direct == 2)
                {
                    q2.count--;
                }
            }
            else if (index < 30) //if abyssal
            {
                if (q2.direct == 3)
                {
                    q2.count--;
                }
            }
            else //if trench
            {
                if (q2.direct == 4)
                {
                    q2.count--;
                }
            }
        }
        if (q3.type <= 3) //if it is a specific fish quest
        {
            if (q3.direct == index) //and they match
            {
                q3.count--;
            }
        }

        if (q3.type > 3 && q3.type <= 6) //if it is a zone quest
        {
            if (index < 12) //if sunlight
            {
                if (q3.direct == 0)
                {
                    q3.count--;
                }
            }
            else if (index < 18) //if twilight
            {
                if (q3.direct == 1)
                {
                    q3.count--;
                }
            }
            else if (index < 24) //if midnight
            {
                if (q3.direct == 2)
                {
                    q3.count--;
                }
            }
            else if (index < 30) //if abyssal
            {
                if (q3.direct == 3)
                {
                    q3.count--;
                }
            }
            else //if trench
            {
                if (q3.direct == 4)
                {
                    q3.count--;
                }
            }
        }
        if (q4.type <= 3) //if it is a specific fish quest
        {
            if (q4.direct == index) //and they match
            {
                q4.count--;
            }
        }

        if (q4.type > 3 && q4.type <= 6) //if it is a zone quest
        {
            if (index < 12) //if sunlight
            {
                if (q4.direct == 0)
                {
                    q4.count--;
                }
            }
            else if (index < 18) //if twilight
            {
                if (q4.direct == 1)
                {
                    q4.count--;
                }
            }
            else if (index < 24) //if midnight
            {
                if (q4.direct == 2)
                {
                    q4.count--;
                }
            }
            else if (index < 30) //if abyssal
            {
                if (q4.direct == 3)
                {
                    q4.count--;
                }
            }
            else //if trench
            {
                if (q4.direct == 4)
                {
                    q4.count--;
                }
            }
        }

        if (q1.type > 9) //if it is a big fish quest
        {
            if (q1.direct == index) //and they match
            {
                q1.count--; ;
            }
        }
        return index;
    }

    public void MiscQuestActivity(int miscDirect)
    {
        if(q1.type <= 9 && q1.type > 6) //if it is a misc quest
        {
            if(q1.direct == miscDirect) // 0 = go to ocean floor, 1 = get a gold dash, 2 = swim through hydro. vent, 3 = check the compendium
            {
                q1.count--;
            }
        }
        if (q2.type <= 9 && q2.type > 6) //if it is a misc quest
        {
            if (q2.direct == miscDirect) // 0 = go to ocean floor, 1 = get a gold dash, 2 = swim through hydro. vent, 3 = check the compendium
            {
                q2.count--;
            }
        }
        if (q3.type <= 9 && q3.type > 6) //if it is a misc quest
        {
            if (q3.direct == miscDirect) // 0 = go to ocean floor, 1 = get a gold dash, 2 = swim through hydro. vent, 3 = check the compendium
            {
                q3.count--;
            }
        }
        if (q4.type <= 9 && q4.type > 6) //if it is a misc quest
        {
            if (q4.direct == miscDirect) // 0 = go to ocean floor, 1 = get a gold dash, 2 = swim through hydro. vent, 3 = check the compendium
            {
                q4.count--;
            }
        }
    }
	
	string getInfo(int line, int indexVal)
	{
		indexVal = indexVal + 1;
		string sout = "";
		
		try{
			StreamReader reader = new StreamReader(path);
			for(int i = 1; i < line; i++)
			reader.ReadLine();
			char c;
			c = 'i';
			byte indexCount=0;
			while(reader.Peek()>-1){
				c = (char)reader.Read();
				if(c==';'){
					if(indexCount>=indexVal)
					{
						reader.Read();
						break;
					}
					indexCount++;
					sout="";
				}
				sout+=c;
			}	
				reader.Close();
		}
		catch{
			
		}
		sout = sout.Substring(1, sout.Length - 1);
	    return sout;
	}

}
