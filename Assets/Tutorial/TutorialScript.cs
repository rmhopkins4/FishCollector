using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Heading, Sub1, Sub2;

    [SerializeField] public bool rotateStick, swimWithB, roll, dash, caughtFish, final,sus;
    float b, rolls, dashs ,cat;
    Vector2 a;
    [SerializeField] SpriteRenderer top, bottom;

    [SerializeField] Sprite[] images;

    private PlayerInputActions playerInputActions;

    [SerializeField] PlayerDataController pdc;


    [SerializeField] GameObject fishPrefab;
    // Start is called before the first frame update
    void Awake ()
    {
        playerInputActions = new PlayerInputActions();


        //read actions from input system for movement
        playerInputActions.Player.Move.performed += cntxt => b = cntxt.ReadValue<float>();



        //read actions from input system for rotation
        playerInputActions.Player.Rotate.performed += cntxt => a = cntxt.ReadValue<Vector2>();

        //read actions from input system for dash
        playerInputActions.Player.Dash.performed += cntxt => dashs= cntxt.ReadValue<float>();

        //read actions from input system for roll
        playerInputActions.Player.Roll.performed += cntxt => rolls= cntxt.ReadValue<float>();
        playerInputActions.Player.Roll.canceled += cntxt => rolls = cntxt.ReadValue<float>();
    }
    private void OnEnable() => playerInputActions.Player.Enable();//When object is enabled, inputs can be read
    private void OnDisable() => playerInputActions.Player.Disable();//Vice versa, disabled object not readable

    private void Start()
    {
        
    }
    int timer = 0;
    int roller = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (b > 0)
        {
            swimWithB = true;
        }
        if (a.magnitude > 0)
        {
            rotateStick = true;
        }
        if (rolls > 0)
        {
            dash = true;
            
        }
        if (rolls == 0)
        {
            roll = true;
        }
        print(rolls);
        
        if (cat > 0)
        {

        }
        if (rotateStick && swimWithB)
        {
            Heading.text = "Good Job";
            Sub1.text = "Now try to roll by pressing A";
            Sub2.text = "Then try to dash by pressing A while rolling";
            top.sprite = images[0];
            bottom.sprite = images[0];

            if (roll &&dash)
            {
                Heading.text = "Good Job";
                Sub1.text = "Now try to catch this fish";
                if (!sus)
                {
                    Instantiate(fishPrefab, new Vector2(transform.position.y,transform.position.x+5), Quaternion.identity);
                }
                sus = true;
                Sub2.text = "Use the Left Bumper to catch fish";
                top.sprite = images[1];
                bottom.sprite = images[2];
                if (sus&&GameObject.Find("Gloober 1(Clone)")==null)
                {
                    caughtFish=true;
                }
                if (caughtFish)
                {
                    Heading.text = "Nice";
                    Sub1.text = "Your quests will be at the top left. Complete them if you wish to gain mana";
                    Sub2.text = "Now its time for the final lesson";
                    top.sprite = images[1];
                    bottom.sprite = images[1];
                    timer++;
                    print(timer);
                    if (timer > 200)
                    {
                        final = true;
                    }
                    if (final) {
                        pdc.ChangeMagic(2000);
                        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
                        Heading.text = "THE MAGIC NET";
                        Sub1.text = "When you have a full mana bar, you may use the magic net using the Left Trigger";
                        Sub2.text = "Use the magic net to catch powerful Great Fish. I have filled up your mana. You may catch me";
                        top.sprite = images[1];
                        bottom.sprite = images[3];
                    }
                }
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "attackHitbox" || collision.gameObject.tag == "magicHitbox")
        {
            SceneManager.LoadScene("mainGame");
        }
    }
}
