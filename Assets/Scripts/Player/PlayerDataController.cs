using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDataController : MonoBehaviour
{
    //set 
    public int health, oxygen, meter;

    public Slider sliderHealth, sliderMeter, sliderOxygen;

    public GameObject netAlert;

    private PlayerInputActions playerInputActions;

    Animator animator;
    //Attacking Variables
    float att;
    bool isAttacking;
    public CapsuleCollider2D lightAttackHitbox;


    public TextMeshProUGUI points;
    public TextMeshProUGUI newPoint;
    public GameObject tmpPrefab;
    int point =0;

    public GameObject magicNet;
    private void Awake()
    {
        health = 100;
        //oxygen = 60;
        //meter = 0;

        playerInputActions = new PlayerInputActions();

        //read actions from input system for attack
        playerInputActions.Player.Attack.performed += cntxt => att = cntxt.ReadValue<float>();
        playerInputActions.Player.Attack.canceled += cntxt => att = 0;

        animator = GetComponent<Animator>();

        points.text = "FISH POINTS: " + point.ToString() + "\nDEPTH:" + Mathf.Round(transform.position.y);

    }
    private void OnEnable() => playerInputActions.Player.Enable();//When object is enabled, inputs can be read
    private void OnDisable() => playerInputActions.Player.Disable();//Vice versa, disabled object not readable
    // Update is called once per frame
    void FixedUpdate()
    {
        if(meter >= 2000)
        {
            netAlert.SetActive(true);
        }
        else
        {
            netAlert.SetActive(false);
        }

        points.text = "FISH POINTS: " + point.ToString() + "\nDEPTH:" + Mathf.Round(transform.position.y);
        sliderHealth.value = health;
        sliderMeter.value = meter;

        if(att == 1 && !isAttacking){
			animator.SetTrigger("attack");
            animator.SetBool("swimming",false);
            isAttacking = true;
            //lightAttackHitbox.enabled = true;
        }
        if(att == 0){
            isAttacking = false;
            //lightAttackHitbox.enabled = false;
            //animator.SetBool("swimming",true);
            animator.ResetTrigger("attack"); 
        }

        if (health<=0)
        {
            quitScript.endGame();
        }
        
        
    }
    public void ChangeMagic(int magChange)
    {
        meter += magChange;
    }
    [SerializeField] GameObject playerHit;
    public void takeDamage(int damTaken)
    {
        health -= damTaken;
        //Instantiate(playerHit, transform.position, Quaternion.identity);
        //sliderHealth.value = health;
    }
    public void addPoints(int p)
    {
        point += p;
        
        tmpPrefab.GetComponent<TextMeshProUGUI>().text = "+" + p;
        tmpPrefab.GetComponent<Animation>().Play();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.name.Contains("Explosion"))
        {
            takeDamage(5);

            gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.GetComponent<Transform>().position - transform.position * 5f);

        }
        if (collision.gameObject.name.Contains("TrenchA"))
        {
            takeDamage(10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Explosion"))
        {
            takeDamage(5);


        }
    }

}
