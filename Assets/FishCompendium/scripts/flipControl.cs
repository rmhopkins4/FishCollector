using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class flipControl : MonoBehaviour
{
	public float waitTimer = 0.0f;

	bool animIsLeft = false;

	
    public static int pageGroup = 1; //which set of pages i am on, remembered when book is reopened and no new fish needs to be displayed

    //need all the slots to be changed into the correct page's info
    public GameObject pageLName;
    public GameObject pageLDescription;
    public GameObject pageLPoints;
    public GameObject pageLImage;

    public GameObject pageRName;
    public GameObject pageRDescription;
    public GameObject pageRPoints;
    public GameObject pageRImage;

    public GameObject pageAName;
    public GameObject pageADescription;
    public GameObject pageAPoints;
    public GameObject pageAImage;
	

	Animator ArrowLAnim;
	Animator ArrowRAnim;

	AudioSource audioSource;
	public AudioClip[] clipArray = new AudioClip[39];

    private PlayerInputActions UIInputActions;
    public Animation anim;
    public Animator animR;
	
	string path = "Assets/FishCompendium/fishInfo.txt";

    public static bool halfDone = false;
    public static bool fullDone = true;

    int turnDir = 0;

    public Vector2 nav;
    // Start is called before the first frame update
	
    private void Awake()
    {
		audioSource = GetComponent<AudioSource>();

		
		//ArrowLAnim = ArrowL.gameObject.GetComponent<Animator>();
		
		//ArrowRAnim = ArrowR.gameObject.GetComponent<Animator>();
		
        UIInputActions = new PlayerInputActions();

        UIInputActions.Player.PageFlipNav.started += cntxt => nav = cntxt.ReadValue<Vector2>();
        UIInputActions.Player.PageFlipNav.canceled += cntxt => nav = Vector2.zero;

		UIInputActions.Player.SoundL.performed += cntxt => OnLSound();
		UIInputActions.Player.SoundR.performed += cntxt => OnRSound();

	}
	
    private void OnEnable() => UIInputActions.Player.Enable();//When object is enabled, inputs can be read
    private void OnDisable() => UIInputActions.Player.Disable();//Vice versa, disabled object not readable
    
    void FixedUpdate()
    {

		
		waitTimer -=0.02f;
        UpdatePages();
		
		if(waitTimer <= 0)
        {

        }


        if(nav.x > 0 && Mathf.Clamp(waitTimer, 0.0f, 100.0f) == 0.0f && fullDone == true && pageGroup < 19)
        {
			
            halfDone = false; fullDone = false;
            turnDir = 1;
            pageGroup++;


            //set anim to 2x-2
            pageAName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 2, 0);
            pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 2, 1);
            pageAPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 2, 2);
			pageAImage.GetComponent<Image>().sprite = Resources.Load<Sprite>((string)getInfo(2 * pageGroup - 2, 3));
			
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 3] == 0)
			{
				pageADescription.GetComponent<TextMeshProUGUI>().text = "???";
			}
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 3] != 2)
			{
				pageAImage.GetComponent<Image>().color =  new Color(0,0,0,1);
			}
			else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 3] == 2)
			{
				pageAImage.GetComponent<Image>().color = new Color(1,1,1,1);
				pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 2, 1);
			}

            //set right to 2x
            pageRName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 0);
            pageRDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
            pageRPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 2);
			pageRImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup, 3));
			
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 0)
			{
				pageRDescription.GetComponent<TextMeshProUGUI>().text = "???";
			}
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] != 2)
			{
				pageRImage.GetComponent<Image>().color =  new Color(0,0,0,1);
			}
			else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 2)
			{
				pageRImage.GetComponent<Image>().color = new Color(1,1,1,1);
				pageRDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
			}


            anim.Play("pageFlip"); //flip page
            nav.x = 0;
			waitTimer = 0.52f;
        }
        if (nav.x < 0 && Mathf.Clamp(waitTimer, 0.0f, 100.0f) == 0.0f && fullDone == true && pageGroup > 1)
        {
			
            halfDone = false; fullDone = false;
            turnDir = -1;
            pageGroup--;


            //set anim to 2x+1
            pageAName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup + 1, 0);
            pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup + 1, 1);
            pageAPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup + 1, 2);
			pageAImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup + 1, 3));
			
			if(GlobalFishIndexHandler.fishes[2 * pageGroup] == 0)
			{
				pageADescription.GetComponent<TextMeshProUGUI>().text = "???";
			}
			if(GlobalFishIndexHandler.fishes[2 * pageGroup] != 2)
			{
				pageAImage.GetComponent<Image>().color =  new Color(0,0,0,1);
			}
			else if(GlobalFishIndexHandler.fishes[2 * pageGroup] == 2)
			{
				pageAImage.GetComponent<Image>().color = new Color(1,1,1,1);
				pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup + 1, 1);
			}

            //set left to 2x-1
            pageLName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 0);
            pageLDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
            pageLPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 2);
			pageLImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup - 1, 3));
			
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 0)
			{
				pageLDescription.GetComponent<TextMeshProUGUI>().text = "???";
			}
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] != 2)
			{
				pageLImage.GetComponent<Image>().color =  new Color(0,0,0,1);
			}
			else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 2)
			{
				pageLImage.GetComponent<Image>().color = new Color(1,1,1,1);
				pageLDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);

			}


            anim.Play("pageFlipBackwards"); //flip page back
            nav.x = 0;
			waitTimer = 0.52f;
        }
		
		
    }

    void UpdatePages()
    {	
         if (turnDir > 0)
         {            
            if (halfDone == true)
            {
				animIsLeft = true;
				//set anim to 2x - 1
				pageAName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 0);
				pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
				pageAPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 2);
				pageAImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup - 1, 3));
			   
				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 0)
				{
					pageADescription.GetComponent<TextMeshProUGUI>().text = "???";
				}
				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] != 2)
				{
					pageAImage.GetComponent<Image>().color =  new Color(0,0,0,1);
				}
				else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 2)
				{
					pageAImage.GetComponent<Image>().color = new Color(1,1,1,1);
					pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
				}

            }

            if (fullDone == true)
            {
                //set left stuff to 2x-1
                 pageLName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 0);
                 pageLDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
                 pageLPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 2);
				 pageLImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup - 1, 3));
				
				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 0)
				{
					pageLDescription.GetComponent<TextMeshProUGUI>().text = "???";
				}
				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] != 2)
				{
					pageLImage.GetComponent<Image>().color =  new Color(0,0,0,1);
				}
				else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 2)
				{
					pageLImage.GetComponent<Image>().color = new Color(1,1,1,1);
					pageLDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
				}

				turnDir = 0;
				playUnlockAnim(true);
            }
         }

         if (turnDir < 0)
         {
             if (halfDone == true)
             {
				animIsLeft = false;
				//set anim to 2x
				pageAName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 0);
                pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
                pageAPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 2);
				pageAImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup, 3));

				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] ==0)
				{
					pageADescription.GetComponent<TextMeshProUGUI>().text = "???";
				}
				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] != 2)
				{
					pageAImage.GetComponent<Image>().color =  new Color(0,0,0,1);

				}
				else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 2)
				{
					pageAImage.GetComponent<Image>().color = new Color(1,1,1,1);
					pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);

				}
             }

             if (fullDone == true)
             {
                //set right stuff to 2x
                pageRName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 0);
                pageRDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
                pageRPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 2);
				pageRImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup, 3));
				
				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 0)
				{
					pageRDescription.GetComponent<TextMeshProUGUI>().text = "???";
				}
				if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] != 2)
				{
					pageRImage.GetComponent<Image>().color =  new Color(0,0,0,1);
				}
				else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 2)
				{
					pageRImage.GetComponent<Image>().color = new Color(1,1,1,1);
					pageRDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
				}

				turnDir = 0;
				playUnlockAnim(false);
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

	bool lastTurned = true;
	void playUnlockAnim(bool directionTurning)
	{
		lastTurned = directionTurning;
		if(directionTurning == false)
		{
			//play animations on anim and left page
			//anim plays if 2 * pageGroup is unfound
			//left plays if 2* pageGroup - 1 is unfound
			if(GlobalFishIndexHandler.fishes[2*pageGroup - 1] == 1)
			{
				pageAImage.GetComponent<Animation>().Play("fishReveal");
				//GlobalFishIndexHandler.fishes[2*pageGroup - 1] = 2;
				waitTimer = 0.9f;
			}
			if (GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 1)
			{
				pageRImage.GetComponent<Animation>().Play("fishReveal");
				GlobalFishIndexHandler.fishes[2 * pageGroup - 1] = 2;
				waitTimer = 0.9f;


				audioSource.clip = clipArray[2 * pageGroup];
				audioSource.Play((ulong)2);
			}
			if (GlobalFishIndexHandler.fishes[2*pageGroup - 2] == 1)
			{
				pageLImage.GetComponent<Animation>().Play("fishReveal");
				GlobalFishIndexHandler.fishes[2*pageGroup - 2] = 2;
				waitTimer = 0.9f;


				audioSource.clip = clipArray[2 * pageGroup - 1];
				audioSource.Play((ulong)2);
			}
			print("turned left");
		}
		else if(directionTurning == true)
		{
			//play animations on right page and anim
			//anim plays on 2 * pageGroup - 1
			//right plays on 2 * pageGroup
			if(GlobalFishIndexHandler.fishes[2*pageGroup - 2] == 1)
			{
				pageAImage.GetComponent<Animation>().Play("fishReveal");
				//GlobalFishIndexHandler.fishes[2*pageGroup - 2] = 2;
				waitTimer = 0.85f;
			}
			if (GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 1)
			{
				pageLImage.GetComponent<Animation>().Play("fishReveal");
				GlobalFishIndexHandler.fishes[2 * pageGroup - 2] = 2;
				waitTimer = 0.85f;


				audioSource.clip = clipArray[2 * pageGroup - 1];
				audioSource.Play((ulong)2);
			}
			if (GlobalFishIndexHandler.fishes[2*pageGroup - 1] == 1)
			{
				pageRImage.GetComponent<Animation>().Play("fishReveal");
				GlobalFishIndexHandler.fishes[2*pageGroup - 1] = 2;
				waitTimer = 0.85f;


				audioSource.clip = clipArray[2 * pageGroup];
				audioSource.Play((ulong)2);
			}
			
		}
	}

	public void LoadBook(int group)
    {
		if(animIsLeft)
        {
			pageAName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 0);
			pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
			pageAPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 2);
			pageAImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup - 1, 3));
			   
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 0)
			{
				pageADescription.GetComponent<TextMeshProUGUI>().text = "???";
			}
			if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] != 2)
			{
				pageAImage.GetComponent<Image>().color =  new Color(0,0,0,1);
			}
			else if(GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 2)
			{
				pageAImage.GetComponent<Image>().color = new Color(1,1,1,1);
				pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
			}
			playUnlockAnim(true);
		}

        else
        {
			pageAName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 0);
			pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
			pageAPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 2);
			pageAImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup, 3));

			if (GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 0)
			{
				pageADescription.GetComponent<TextMeshProUGUI>().text = "???";
			}
			if (GlobalFishIndexHandler.fishes[2 * pageGroup - 1] != 2)
			{
				pageAImage.GetComponent<Image>().color = new Color(0, 0, 0, 1);

			}
			else if (GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 2)
			{
				pageAImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
				pageADescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
			}
			playUnlockAnim(false);
		}

		//set left stuff to 2x-1
		pageLName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 0);
		pageLDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
		pageLPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 2);
		pageLImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup - 1, 3));

		if (GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 0)
		{
			pageLDescription.GetComponent<TextMeshProUGUI>().text = "???";
		}
		if (GlobalFishIndexHandler.fishes[2 * pageGroup - 2] != 2)
		{
			pageLImage.GetComponent<Image>().color = new Color(0, 0, 0, 1);
		}
		else if (GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 2)
		{
			pageLImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
			pageLDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup - 1, 1);
		}

		turnDir = 0;


		pageRName.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 0);
		pageRDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
		pageRPoints.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 2);
		pageRImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(getInfo(2 * pageGroup, 3));

		if (GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 0)
		{
			pageRDescription.GetComponent<TextMeshProUGUI>().text = "???";
		}
		if (GlobalFishIndexHandler.fishes[2 * pageGroup - 1] != 2)
		{
			pageRImage.GetComponent<Image>().color = new Color(0, 0, 0, 1);
		}
		else if (GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 2)
		{
			pageRImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
			pageRDescription.GetComponent<TextMeshProUGUI>().text = "" + getInfo(2 * pageGroup, 1);
		}

		turnDir = 0;
	}
	void OnLSound()
	{
		if(waitTimer <= 0 && GlobalFishIndexHandler.fishes[2 * pageGroup - 2] == 2)
        {
			audioSource.clip = clipArray[2 * pageGroup - 1];
			audioSource.Play();

			//anim L
			//add wait time
			pageLImage.GetComponent<Animation>().Play("fishGrow");
			if (lastTurned) pageAImage.GetComponent<Animation>().Play("fishGrow");
			waitTimer = 0.25f;
		}
	}
	void OnRSound()
    {
		if (waitTimer <= 0 && GlobalFishIndexHandler.fishes[2 * pageGroup - 1] == 2)
		{
			audioSource.clip = clipArray[2 * pageGroup];
			audioSource.Play();

			//anim R
			//add wait time
			pageRImage.GetComponent<Animation>().Play("fishGrow");
			if (!lastTurned) pageAImage.GetComponent<Animation>().Play("fishGrow");
			waitTimer = 0.25f;
		}
	}
}
