using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFishIndexHandler : MonoBehaviour
{
    public static byte[] fishes;


	public byte[] fishesDisplay;


	public QuestHandler qh;
	// Start is called before the first frame update

	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	void Start()
	{
		qh = GameObject.Find("TamerPlayer").GetComponent<QuestHandler>();

		fishes = new byte[39];
		//TESTING SUSY BAKY
		//fishes[0] = 0;
		//fishes[1] = 0;
		//fishes[2] = 0;
		//fishes[3] = 0;
		//fishes[4] = 0;
		//fishes[5] = 0;
		//fishes[6] = 0;
		//fishes[7] = 0;
		//fishes[8] = 0;
		//fishes[9] = 0;
		//fishes[10] = 0;
		//fishes[11] = 0;
		//fishes[12] = 0;
		//fishes[13] = 0;
		//fishes[14] = 0;
		//fishes[15] = 0;
		//fishes[16] = 0;
		//fishes[17] = 0;
		//fishes[18] = 0;
		//fishes[19] = 0;
		//fishes[20] = 0;
		//fishes[21] = 0;
		//fishes[22] = 0;
		//fishes[23] = 0;
		//fishes[24] = 0;
		//fishes[25] = 0;
		//fishes[26] = 0;
		//fishes[27] = 0;
		//fishes[28] = 0;
		//fishes[29] = 0;
		//fishes[30] = 0;
		//fishes[31] = 0;
		//fishes[32] = 0;
		//fishes[33] = 0;
		//fishes[34] = 0;
		//fishes[35] = 0;
		//fishes[36] = 0;
		//fishes[37] = 0;
  //      fishes[38] = 0;
		
	}
    
    // Update is called once per frame
    void Update()
    {
		fishesDisplay = fishes;
    }

    public void fishCaught(int index)
    {
		if(fishes[index - 1] == 0)
        {
			fishes[index - 1] = 1;
		}
		qh.catchReceive(index);
		print(index);
    }
}