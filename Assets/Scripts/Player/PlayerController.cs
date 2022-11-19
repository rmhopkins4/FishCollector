//Made by Olivia Chiarni with support from Tamer Yalinkilincer

//TAMER'S SILLY NOTE: DO NOT PUT ATTACKS OR GENERAL PLAYER DATA LIKE POINTS AND HEALTH HERE. PUT THOSE IN PlayerDataController!!!!!!!



//MUST REPORT MISC QUEST ACTIVITY TO THIS.QUESTHANDLER.MISCQUESTACTIVITY EVENTUALLY


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Input System Variable, more done in awake
    private PlayerInputActions playerInputActions;

    //Stores lighting information
    public Light2D playerFlashLight;
    bool isOn = false;

    //Stores rotation on stick, move input, dash input
    Vector2 rot;
    float move;
    float dash;

    //animation buisiness
    Animator animator;
    bool isMoving;

    //All Variables for Rolling
    float torqueForce = 1000;
    float rWait = 1, rotVal;
    float rollPressed;
    float lightToggler;
    bool isRolling;
    bool rollAnim;
    bool finishingRoll;

    //Rotation and Dash Varibles
    Vector2 desiredVel;
    float desiredAVel;
    [SerializeField]
    float ddamp = .5f, dWait = 5;
    bool canRoll;
    public static bool isDashing;

    //Stores Rigidbody for Player
    Rigidbody2D rb;



    //Stores camera (for screen shake/effects etc
    public GameObject mainCam;

    //particles!
    public GameObject rotPart;
    public GameObject readyPart;
    public GameObject goldPart;
    public GameObject silverPart;
    public GameObject bronzePart;

    public Gradient goldGrad, silverGrad, bronzeGrad;

    public GameObject magicNetObj;

    #endregion
    #region Start and Stop Functions
    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        

        rotPart = gameObject.transform.Find("rotationparticle").gameObject;
        /*Breakdown of movement system
         * 
         * Make an Input Actions System In Unity, auto generate c# script
         *      NOTE:May need to delete and remake C# Script after additons of new input settings 
         * Create PlayerInputAction in code, as shown bellow:
         *      private PlayerInputActions playerInputActions
         * Initlize playerInputActions in Awake function(runs everytime object is woken up)
         * 
         * To set actions, do the following:
         *      playerInputActions.(name of set of inputs).(name of input).(state to check)...
         *      +=cntxt=>(variable to store data into)
         *  example for storing a vector2
         *      playerInputActions.Player.Move.performed += cntxt => move = cntxt.ReadValue<float>();
         * Important Commands to note:
         *      performed:as it happens
         *      canceled:stop of action
         */

        playerInputActions = new PlayerInputActions();
        //read actions from input system for movement
        playerInputActions.Player.Move.performed += cntxt => move = cntxt.ReadValue<float>();
        playerInputActions.Player.Move.canceled += cntxt => move = 0f;

        //read actions from input system for rotation
        playerInputActions.Player.Rotate.performed += cntxt => rot = cntxt.ReadValue<Vector2>();
        playerInputActions.Player.Rotate.canceled += cntxt => rot = Vector2.zero;

        //read actions from input system for dash
        playerInputActions.Player.Dash.performed += cntxt => onDash();

        //read actions from input system for roll
        playerInputActions.Player.Roll.performed += cntxt => onRoll();

        //read actions from input system for light on
        playerInputActions.Player.LightToggle.performed += cntxt => lightToggler = cntxt.ReadValue<float>();
        playerInputActions.Player.LightToggle.canceled += cntxt => lightToggler = 0;

        playerInputActions.Player.Pause.performed += cntxt => print("pause");
        playerInputActions.Player.Compen.performed += cntxt => print("compen");

        
        playerInputActions.Player.Magic.started += cntxt => magicNetObj.GetComponent<nettest>().tellButtonPressed();
        playerInputActions.Player.Magic.canceled += cntxt => magicNetObj.GetComponent<nettest>().tellButtonUnPressed();
        
        //Initilzaes Variables that may need to be reset after a pause
        rb = gameObject.GetComponent<Rigidbody2D>();
        canRoll = true;
        isRolling = false;
        finishingRoll = false;
        magicNet = false;
        rotVal = 0;


        //Initializes animator
        animator = GetComponent<Animator>();
        if (this.gameObject.tag == "endPlayer")
            isEnd = true;
    }
    bool magicNet;
    private void OnEnable() => playerInputActions.Player.Enable();//When object is enabled, inputs can be read
    private void OnDisable() => playerInputActions.Player.Disable();//Vice versa, disabled object not readable
    #endregion

    [SerializeField]
    float fastfast = 50;
    float timeAtDash;

    [SerializeField]
    bool oldDash = true;
    bool isEnd = false;
    void FixedUpdate()
    {

        //Calculated a velocity the player should go in based on which way it is facing
        desiredVel = transform.up * fastfast * move;//When pressing move button, move is 1, else 0
        if (!isRolling)
            rb.velocity = Vector2.Lerp(rb.velocity, desiredVel, Time.deltaTime * 2);
        //Linerally Interpaltes to the speed player wants to 

        //if not moving, and move started, swim anim.
        if (move == 1)
        {

            if (!isMoving)
            {
                isMoving = true;
                animator.ResetTrigger("enterSwim");
                animator.SetTrigger("enterSwim");
            }
            animator.SetBool("swimming", true);
        }
        //if moving, stop
        if (move == 0)
        {
            if (isMoving)
            {
                isMoving = false;
                animator.ResetTrigger("exitSwim");
                animator.SetTrigger("exitSwim");

            }
            animator.SetBool("swimming", false);
        }


        /*if(rollPressed==1 && !isRolling){
            animator.ResetTrigger("rollTrigger");
            animator.SetTrigger("rollTrigger");
        }*/

        if (isDashing)
        {
            animator.ResetTrigger("dashTrigger");
            animator.SetTrigger("dashTrigger");

            //screen shake
            mainCam.GetComponent<CameraFollow>().CameraShake(0.1f, 0.2f);
            //turn on trail
            //etc.
        }

        if (isRolling&&!finishingRoll)
        {
            rb.transform.Rotate(new Vector3(0, 0, -torqueForce * Time.deltaTime));
            animator.SetBool("rolling", true);
        }
        else
        {
            animator.SetBool("rolling", false);

        }

        if (finishingRoll)
        {
            desiredAVel = -rot.x * 200f;
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, desiredAVel, Time.deltaTime * 10f);
        }

        if (!isRolling && rot != Vector2.zero)
        {
            float theta = (Mathf.Rad2Deg * Mathf.Atan2(rot.y, rot.x) - 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, theta)), Time.deltaTime * 15f);
        }




        //flashlight handlers
        //if light is off, turn on
        if (lightToggler == 1 && !playerFlashLight.enabled)
        {
            playerFlashLight.enabled = true;
            lightToggler = 0;
        }
        //if light is on, turn off
        if (lightToggler == 1 && playerFlashLight.enabled)
        {
            playerFlashLight.enabled = false;
            lightToggler = 0;
        }

        //trail
        if (isDashing == true)
        {
            this.GetComponent<TrailRenderer>().enabled = true;
        }
        else
        {
            this.GetComponent<TrailRenderer>().enabled = false;
        }

        //quest
        if (this.transform.position.y < -1400)
        {
            this.GetComponent<QuestHandler>().MiscQuestActivity(0);
        }

        //smoke
        if(isDashing)
        {
            this.GetComponent<ParticleSystemForceField>().directionY = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(rb.velocity.x), 2) + Mathf.Pow(Mathf.Abs(rb.velocity.y), 2));
        }
        else
        {
            this.GetComponent<ParticleSystemForceField>().directionY = 0;
        }

        //magic
        //print(magicNetObj.GetComponent<nettest>().busy);
        if (magicNetObj.GetComponent<nettest>().charging == true)
        {

                animator.SetBool("doneCharging", false);
                animator.Play("magicCharge");


        }
        if (magicNetObj.GetComponent<nettest>().charging == false)
        {
                animator.SetBool("doneCharging", true);
        }
        

        if (oldDash)
        {
            if ((canRoll && rollPressed != 0) || (isRolling))//Checks if player is already rolling or if they are starting
            {

                if (dash != 0 && !isDashing && isRolling)//sees if player wants to dash out of a roll
                {
                    rollPressed = 0;
                    dash = 0;
                    rb.angularVelocity = 0;//Sets angular Velocity to 0 so you dont go in odd directions after dash
                    if (rot != Vector2.zero)//Makes sure user is inputting something, as would through nullref otherwise
                    {
                        float t = rotPart.GetComponent<ParticleSystem>().time;
                        float dashSize = 0;
                        if (t > .6f)
                        {
                            dashSize = .25f;
                            Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                        }
                        else if (t > .4f)
                        {
                            GameObject.Find("TamerPlayer").GetComponent<QuestHandler>().MiscQuestActivity(1);
                            
                            dashSize = 1f;
                            Instantiate(goldPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = goldGrad;
                        }
                        else if (t > .15f)
                        {
                            dashSize = .5f;
                            Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                        }
                        else
                        {
                            dashSize = .25f;
                            Instantiate(bronzePart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = bronzeGrad;
                        }
                        rb.velocity = 175 * new Vector2(rot.x * dashSize, rot.y * dashSize);
                        //rb.AddForce(rot , ForceMode2D.Force);//Moves player in direction of joystick, forcemode force instead of impulse to make it more smooth
                        timeAtDash = 0;
                        //Checks for changing player rotation to the direciton they are moving
                        //Basic logic is taking the arctangent of the y/x stick values, converting it to deegrees, and correcting by 90deg
                        if (rot.x > 0)
                            rb.rotation = ((Mathf.Rad2Deg * Mathf.Atan((rot.y / rot.x))) - 90);
                        else //can not explain this one buddy
                            rb.rotation = -((Mathf.Rad2Deg * -Mathf.Atan((rot.y / rot.x))) - 90);
                        isDashing = true;
                    }
                    else
                    {
                        //Same thing, just goes off of direction player is facing currently
                        float t = rotPart.GetComponent<ParticleSystem>().time;

                        print(t);
                        float dashSize = 0;
                        if (t > .6f)
                        {
                            dashSize = .25f;
                            Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                        }
                        else if (t > .4f)
                        {
                            GameObject.Find("TamerPlayer").GetComponent<QuestHandler>().MiscQuestActivity(1);
                            
                            dashSize = 1f;
                            Instantiate(goldPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = goldGrad;
                        }
                        else if (t > .15f)
                        {
                            dashSize = .5f;
                            Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                        }
                        else
                        {
                            dashSize = .25f;
                            Instantiate(bronzePart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                            this.GetComponent<TrailRenderer>().colorGradient = bronzeGrad;
                        }
                        rb.velocity = 175 * new Vector2(rb.transform.up.x * dashSize, rb.transform.up.y * dashSize);
                        timeAtDash = 0;
                        if (rb.transform.up.x > 0)
                            rb.rotation = ((Mathf.Rad2Deg * Mathf.Atan((rb.transform.up.y / rb.transform.up.x))) - 90);
                        else
                            rb.rotation = -((Mathf.Rad2Deg * -Mathf.Atan((rb.transform.up.y / rb.transform.up.x))) - 90);
                        isDashing = true;
                    }
                    //Makes sure the player cant roll immeditally after
                    canRoll = false;
                    isRolling = false;
                    if (rb.rotation < 0)//Makes sure rotation is a positve value
                        rb.rotation += 360;
                    StopAllCoroutines();//Stops normal rolling delay
                    if (Input.GetJoystickNames() != null)
                        Gamepad.current.SetMotorSpeeds(5f, 5f);
                    StartCoroutine(waitForRoll(dWait));//Runs faster delay for dash
                    rb.angularVelocity = 0;
                }
                else if (!isRolling&&!finishingRoll)
                {
                    
                    StartCoroutine(currentlyRolling(rWait));//Starts runtime for regular roll

                }

            }
        }
        else
        {
            if (isRolling && !isDashing && newInp == 2)
            {
                newInp = 0;
                rb.angularVelocity = 0;//Sets angular Velocity to 0 so you dont go in odd directions after dash
                if (rot != Vector2.zero)//Makes sure user is inputting something, as would through nullref otherwise
                {
                    float t = rotPart.GetComponent<ParticleSystem>().time;
                    float dashSize = 0;
                    if (t > .6f)
                    {
                        dashSize = .25f;
                        Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                    }
                    else if (t > .4f)
                    {
                        GameObject.Find("TamerPlayer").GetComponent<QuestHandler>().MiscQuestActivity(1);

                        dashSize = 1f;
                        Instantiate(goldPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = goldGrad;
                    }
                    else if (t > .15f)
                    {
                        dashSize = .5f;
                        Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                    }
                    else
                    {
                        dashSize = .25f;
                        Instantiate(bronzePart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = bronzeGrad;
                    }
                    rb.velocity = 175 * new Vector2(rot.x * dashSize, rot.y * dashSize);
                    //rb.AddForce(rot , ForceMode2D.Force);//Moves player in direction of joystick, forcemode force instead of impulse to make it more smooth
                    timeAtDash = 0;
                    //Checks for changing player rotation to the direciton they are moving
                    //Basic logic is taking the arctangent of the y/x stick values, converting it to deegrees, and correcting by 90deg
                    if (rot.x > 0)
                        rb.rotation = ((Mathf.Rad2Deg * Mathf.Atan((rot.y / rot.x))) - 90);
                    else //can not explain this one buddy
                        rb.rotation = -((Mathf.Rad2Deg * -Mathf.Atan((rot.y / rot.x))) - 90);
                    isDashing = true;
                }
                else
                {
                    //Same thing, just goes off of direction player is facing currently
                    float t = rotPart.GetComponent<ParticleSystem>().time;
                    float dashSize = 0;
                    if (t > .6f)
                    {
                        dashSize = .25f;
                        Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                    }
                    else if (t > .4f)
                    {
                        GameObject.Find("TamerPlayer").GetComponent<QuestHandler>().MiscQuestActivity(1);

                        dashSize = 1f;
                        Instantiate(goldPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = goldGrad;
                    }
                    else if (t > .15f)
                    {
                        dashSize = .5f;
                        Instantiate(silverPart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = silverGrad;
                    }
                    else
                    {
                        dashSize = .25f;
                        Instantiate(bronzePart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(-90, 0, 0));
                        this.GetComponent<TrailRenderer>().colorGradient = bronzeGrad;
                    }
                    rb.velocity = 175 * new Vector2(rb.transform.up.x * dashSize, rb.transform.up.y * dashSize);
                    timeAtDash = 0;
                    if (rb.transform.up.x > 0)
                        rb.rotation = ((Mathf.Rad2Deg * Mathf.Atan((rb.transform.up.y / rb.transform.up.x))) - 90);
                    else
                        rb.rotation = -((Mathf.Rad2Deg * -Mathf.Atan((rb.transform.up.y / rb.transform.up.x))) - 90);
                    isDashing = true;
                }
                //Makes sure the player cant roll immeditally after
                canRoll = false;
                isRolling = false;
                if (rb.rotation < 0)//Makes sure rotation is a positve value
                    rb.rotation += 360;
                StopAllCoroutines();//Stops normal rolling delay
                if (Input.GetJoystickNames() != null)
                    Gamepad.current.SetMotorSpeeds(5f, 5f);
                StartCoroutine(waitForRoll(dWait));//Runs faster delay for dash
                rb.angularVelocity = 0;
            }

            else if (!finishingRoll&&newInp == 1&&!isRolling)
            {
                print("starting roll");
                StartCoroutine(currentlyRolling(rWait));//Starts runtime for regular roll
            }
        }
        //Rolling and Dashing Logic

    }
    byte newInp = 0;
    // 0 nothing, 1 rollling, 2 is dash, 3 is post dash
    void onDash()
    {
        if (oldDash)
            dash = 1;
    }
    void onRoll()
    {
        if (oldDash)
        {
            rollPressed = 1;
        }
        else
        {
            if ((canRoll && !isRolling) || (isRolling && !isDashing))
                newInp++;
        }
    }

    #region Timed Enumerators
    //all of these are just functions in enums so they can use wait for seconds, which explains itself
    IEnumerator waitForRoll(float delay)
    {
        finishingRoll = true;
        rotPart.SetActive(false);
        yield return new WaitForSeconds(delay / 2);
        Gamepad.current.SetMotorSpeeds(0, 0);
        yield return new WaitForSeconds(delay / 2);
        readyPart.SetActive(true);
        timeAtDash = -1;
        canRoll = true;
        newInp = 0;
        rollPressed = 0;
        finishingRoll = false;
        isDashing = false;
        CancelInvoke();
    }
    IEnumerator currentlyRolling(float delay)
    {
        print("runnng"+Time.time);
        readyPart.SetActive(false);
        isRolling = true;
        timeAtDash = Time.time;
        rotPart.SetActive(true);
        yield return new WaitForSeconds(delay);
        isRolling = false;
        canRoll = false;
        StartCoroutine(waitForRoll(dWait));
        yield break;
    }
    #endregion
}
