using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class fishBehaviourScript : MonoBehaviour
{
    /*Behavior Booleans
    0-Patrol BASE
    1-Cower
    2-bounceAbout BASE
    3-Defend
    */
    int switcher;

    AudioSource audioS;
    float delay;

    //neededVars
    Transform tr;
    Rigidbody2D rb;
    public Transform playerPositon;
    public SpriteRenderer sr;


    [SerializeField] bool isBoss;


    //animation components 
    Animator animator;
    #region Shitty Pseudo Code
    /*
     *  serilzaed, nonstatic, two ints, minY and maxY
     * generate a rn
     * 
     * 
     * 
     * 
     */
    #endregion


    public bool[] behaviours = new bool[11];




    //catching handlers
    public GameObject pp;
    public GlobalFishIndexHandler gb;
    public PlayerDataController pdc;
    public int index;

    //patrol vars;
    public float speed;
    public float waitTime;
    bool once;


    public int pointVal;


    //generalmove

    [SerializeField]
    int yMin, yMax, xMin, xMax, maxRange;
    [SerializeField]
    Vector3 point;

    //attackVars
    [SerializeField]
    float AggroRange, innerTargetRange, innerVelMultiplier;

    [SerializeField] Transform anglerLight;


    Vector2 currentPosition;
    Vector2 desiredPosition;

    [SerializeField] bool scalingAllowed;
    void Start()
    {
        delay = Random.Range(5, 25);
        audioS = GetComponent<AudioSource>();


        //pick a random size for fish
        if (scalingAllowed)
        {
            float x = Random.Range(1, 1.75f);
            transform.localScale = new Vector3(x, x, 1);
        }
        
        
        //initialize some stuff
        sr = gameObject.GetComponent<SpriteRenderer>();
        gb = GameObject.Find("Global Handler").GetComponent<GlobalFishIndexHandler>();
        pdc = GameObject.Find("TamerPlayer").GetComponent<PlayerDataController>();
        Physics.IgnoreLayerCollision(2, 2);


        //gloober it up bitch!
        if (gameObject.name.Contains("loober"))
        {
            Color c = new Color();
            c.r = Random.Range(0.0f, 1.0f);
            c.g = Random.Range(0.0f, 1.0f);
            c.b = Random.Range(0.0f, 1.0f);
            c.a = 1;
            sr.color = c;
        }


        //these pick the initial behaviour


        if (behaviours[2])
        {
            switcher = 2;
        }
        else if (behaviours[0])
        {
            switcher = 0;
        }
        else if (behaviours[4])
        {
            switcher = 4;
        }
        else if (behaviours[5])
        {
            switcher = 5;
        }
        else if (behaviours[7])
        {
            cly = transform.position.y;
            switcher = 7;
        }
        else if (behaviours[10])
        {
            switcher = 10;
        }
            


        if (behaviours[0])
        {
            point = pointPicker();
            if(point == Vector3.zero)
            {
                Destroy(gameObject);
            }
        }

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        //pointVal = 1;

        animator = GetComponent<Animator>();

        animator.SetBool("isAnimating", true);

        desiredPosition = new Vector2(tr.position.x + Random.Range(-20, 20), tr.position.y + Random.Range(-5, 5));

    }

    [SerializeField] bool noFlipping;
    void FixedUpdate()
    {
        //sound
        



        //this fixes that upside down fish issue
        
       if(tr.rotation.z<0 && tr.rotation.z> -180 && !noFlipping)
       {
            sr.flipX = true;
       }
       else
       {
            sr.flipX = false;
       }

       //make sure it always knows where the player is
        playerPositon = GameObject.Find("TamerPlayer").GetComponent<Transform>();
        //Attack(GameObject.Find("TamerPlayer"));
        //generalMove();
        //bounceAbout();
        /*if(tr.position.y > 1)
        {
            switcher = 3;
        }
        else
        {
            if (behaviours[2])
            {
                switcher = 2;
            }
            if (behaviours[0])
            {
                switcher = 0;
            }
        }*/

        //handle the wonky attack pattern i have. If the player gets within range of the player, do the attack code.
        if (behaviours[6])
        {
            if (Vector2.Distance(tr.position, playerPositon.position) < AggroRange)
            {
                switcher = 6;
            }
            else
            {
                switcher = 0;
            }
        }
        if (behaviours[9])
        {
            if (Vector2.Distance(tr.position, playerPositon.position) < AggroRange)
            {
                switcher = 9;
            }
            else
            {
                switcher = 0;
            }
        }
        if (behaviours[3])
        {
            if (Vector2.Distance(tr.position, playerPositon.position) < AggroRange)
            {
                switcher = 3;
            }
            else
            {
                switcher = 0;
            }
        }
        if (behaviours[4])
        {
            if (Vector2.Distance(anglerLight.position, playerPositon.position) < AggroRange)
            {
                switcher = 4;
            }
            else
            {
                switcher = 0;
            }
        }
        if (behaviours[8]&&tr.position.y>-5)
        {
            point = new Vector2(tr.position.x,0);
            print("huh");
            switcher= 8;
        }
        if (behaviours[1] && Vector2.Distance(gameObject.transform.position,playerPositon.position)<AggroRange)
        {
            switcher = 1;
        }
        else if(behaviours[2])
        {
            switcher = 2;
        }

        //switch to handle the states of the fish
        switch (switcher)
        {
            case 0: //general movement. Basic bitch fish
                generalMove();
                break;
            case 1: //be scared. Plays an "ishiding" animation. I think only squidger has one.
                Cower();
                break;
            case 2: //pretty much made for squidger. It bounces around.
                bounceAbout();
                break;
            case 3: //just adds gravity.
                Tortuuwa(GameObject.Find("TamerPlayer"));
                break;
            case 4: //actually an empty function
                darkBramble(GameObject.Find("TamerPlayer"));
                break;
            case 5: //not implemented yet. The idea is something that moves along terrain.
                groundMove();
                break;
            case 6: //implementation wonky. Pretty basic. Relies on bouncing off the player.
                Attack(GameObject.Find("TamerPlayer"));
                break;
            case 7:
                skyMove();
                break;
            case 8:
                jump();
                break;
            case 9:
                squidGames();
                break;
            case 10:
                reg();
                break;
            default:
                break;
        }
        
    }

    public GameObject regAttack;
    int regTimer = 200;
    void reg()
    {
        regTimer++;
        if (Vector2.Distance(transform.position, playerPositon.position) < AggroRange)
        {
            
            if(regTimer >= 200)
            {
                regAttack.SetActive(true);
                regTimer = 0;
            }
            rb.angularVelocity = 100f;
        }
        else
        {
            rb.angularVelocity = 0;
            regAttack.SetActive(false);
        }
    }

    public GameObject bombPrefab;
    int poggie = 0;

    int anglerTimer=20;
    void darkBramble(GameObject Target)
    {
        transform.up = Vector2.Lerp(transform.up, playerPositon.position - transform.position, 0.5f);
        anglerTimer++;
        if (anglerTimer>=20)
        {
            rb.AddForce(transform.up * 1000f);
            anglerTimer = 0;
        }
        
    }
    void squidGames()
    {
        poggie++;
        if(poggie >= 7)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            poggie = 0;
        }
        
        transform.up = Vector2.Lerp(transform.up, playerPositon.position - transform.position, 0.007f);
        rb.velocity = transform.up * 100f ;
    }
    bool waiting=false;
    float moveSpeed = 5;
    Vector2 velDamp = new Vector2(10, 10);

    /*~olivia psuedo code~
     * 
     * Dolphin:
     *  general move most of the time
     *  
     *  if at or above y=-2
     *      add force towards top of water and dont pick a point to move to 
     *  when back in water, go back to general move
     */
    [SerializeField] 
    float dolphForce;
    float yPeak = 10;
    void jump(){
        if (tr.position.y < -5&&rb.gravityScale!=0)
        {
            rb.gravityScale = 0;
            switcher = 0;
            
        }
        else if(rb.gravityScale==0)
        {
            rb.gravityScale = 5;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2 * (rb.gravityScale * dolphForce) * (yPeak)));
        }
        else {
            rb.transform.up = rb.velocity;
        }
    }
    float cly = 10;
    float clx = 0;


    void skyMove(){
        if (Vector2.Distance(transform.position, new Vector2(clx,cly)) > 15&&clx!=0)
        {
            //transform.up = Vector2.SmoothDamp(transform.up, point-transform.position,ref velDamp,0.7f);
            rb.velocity = transform.up * speed;
            if (rb.velocity == Vector2.zero)
            {
                clx = Random.Range(-300, 300);
                transform.up = new Vector3(clx, cly, 0) - transform.position;
            }
            Debug.DrawRay(transform.position, new Vector3(clx, cly) - transform.position);
        }
        else
        {
            clx = Random.Range(-300, 300);
            transform.up = new Vector3(clx, cly, 0) - transform.position;
        }
    }
    void groundMove()
    {
        /*
         * 
         * Sludger:
         *  when you spawn, have gravity on
         *  add force to move, ossocsialy send a ray cast out to make sure ground isnt far away
         *  when you generate a point, circle cast to make sure its near ground
         *  small radius for possible points to prevent tryin gto go to another island
         */
    }
    void aboveWater()
    {
        rb.gravityScale = 1;
    }
    [SerializeField]
    float tamerFactor = 10;
    void generalMove()
    {
        if (Vector2.Distance(transform.position, point) > 15)
        {
            
            //transform.up = Vector2.SmoothDamp(transform.up, point-transform.position,ref velDamp, Mathf.Pow(0.1f,speed-1)+0.1f);
            transform.up = Vector2.Lerp(transform.up, point - transform.position, 1/(speed*tamerFactor));
            rb.velocity = transform.up*speed;
            if (rb.velocity == Vector2.zero)
            {
                point = pointPicker();
            }
            Debug.DrawRay(transform.position, point - transform.position);
        }
        else
        {
            point = pointPicker();
            //print(pointPicker());
        }

    }
    
    void bounceAbout()
    {

        animator.SetBool("IsHiding", false);
        animator.SetBool("isAnimating", true);
        currentPosition = transform.position;
        tr.up = Vector2.SmoothDamp(transform.up, desiredPosition - currentPosition, ref velDamp, 0.5f);
        while(desiredPosition.y > 0)
        { 
            desiredPosition = new Vector2(currentPosition.x + Random.Range(-20, 20), currentPosition.y + Random.Range(-5, 0));
        }
        if (Vector2.Distance(transform.position, desiredPosition) > 3)
        {


            rb.AddForce(tr.up * speed);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);

        }
        else
        {
            desiredPosition = new Vector2(currentPosition.x + Random.Range(-20, 20), currentPosition.y + Random.Range(-5, 5));
        }
        if (gameObject.GetComponent<Light2D>().intensity > 0)
        {
            gameObject.GetComponent<Light2D>().intensity -= 0.1f;
        }
        if (gameObject.GetComponent<Light2D>().intensity > 7)
        {
            gameObject.GetComponent<Light2D>().intensity = 7;
        }
        
    }
    void Cower()
    {
        animator.SetBool("isAnimating", false);
        animator.SetBool("IsHiding", true);
    }

    int attackCount = 0;
    void Attack(GameObject target)
    {

        //pick the direction it wants to go (to the player)
        Vector2 targetDirection = target.transform.position - gameObject.transform.position;
        //slerps the rotation to where it wants to be
        transform.up = Vector2.Lerp(transform.up, rb.velocity.normalized, 0.5f);

        if (Vector2.Distance(target.transform.position, gameObject.transform.position) > innerTargetRange) //if too far from player to do attack
        {
            //rb.velocity = Vector2.Lerp(rb.velocity, targetDirection.normalized * speed, 100f); //go towards player
            rb.velocity = targetDirection.normalized * speed;
        }
        else
        {
            rb.velocity = rb.velocity * innerVelMultiplier; //fuckin zoom at the player
        }

        Debug.DrawRay(transform.position, targetDirection);

    }
    [SerializeField] GameObject fireBall;
    public Transform fireBallSpawn;
    void Tortuuwa(GameObject target)
    {
        Vector2 targetDirection = target.transform.position - gameObject.transform.position;
        transform.up = Vector2.Lerp(transform.up, rb.velocity.normalized, 0.05f);
        rb.velocity = targetDirection.normalized * speed;

        poggie++;
        if (poggie >= 100)
        {
            GameObject ball = Instantiate(fireBall, fireBallSpawn.position, Quaternion.identity);
            ball.GetComponent<Transform>().up = gameObject.transform.up;
            poggie = 0;
        }
    }
    [SerializeField] int attackDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            if (behaviours[2]) //if its a squidger
            {
                desiredPosition = new Vector2(currentPosition.x + Random.Range(-20, 20), currentPosition.y + Random.Range(-5, 5)); //pick a new place to go
                if (gameObject.GetComponent<Light2D>().intensity < 7)
                {
                    gameObject.GetComponent<Light2D>().intensity += 2;
                }
            }
            else if (behaviours[8] && switcher == 8)
            {
                switcher = 0;
            }
            else if (switcher == 0)
            {
                point = pointPicker();
            }
        }
        if (behaviours[2])
        {
            if (collision.gameObject.name.Contains("squidger"))
            {
                
                if ((collision.gameObject.GetComponent<Light2D>().intensity > gameObject.GetComponent<Light2D>().intensity))
                {
                    if(gameObject.GetComponent<Light2D>().intensity < 7)
                    {
                        gameObject.GetComponent<Light2D>().intensity += collision.gameObject.GetComponent<Light2D>().intensity + 0.5f / 2;
                    }
                }
            }
        }
        if(collision.gameObject.tag == "Player")
        {
            pdc.takeDamage(attackDamage);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            if (behaviours[8] && switcher == 8)
                switcher = 0;
            //if (behaviours[5])
            //{
            //    rb.AddForce(tr.right*speed);
            //}
            if(switcher == 0)
            {
                point = pointPicker();
            }
        }
    }

    
    Vector2 pointPicker()
    {
        
        //do a pos
        Vector3 pos = Vector3.zero;
        
        
        for(int i = 0; i<1000; i++)
        {

            pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax)); //pick random position within range
            RaycastHit2D ray = Physics2D.Raycast(transform.position, pos - transform.position,Vector2.Distance(transform.position,pos));//throw a ray there
            
            if (ray.collider == null && Vector2.Distance(pos,transform.position)<maxRange) //if it doesnt hit anything
            {
                return pos; //ye found yerself a point
            }
            else
            {
                Debug.DrawRay(transform.position, pos - transform.position); //draw a ray for funzies 
            }
        }
        Destroy(gameObject); //oh it took you more than 100? stinky fish.
        return Vector2.zero; //filler line. needs to be here so there isnt a compiler error
        
    }


    





   
    IEnumerator Wait()
    {
        point = Vector2.zero;
        waiting = true;
        yield return new WaitForSeconds(1);
        waiting = false;
    }

    [SerializeField] GameObject splashParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (behaviours[1]) //if gets scared
            {
                switcher = 1; //get scared
            }
            
        }
        if(collision.gameObject.tag == "waterSiurf")
        {
            if (behaviours[8])
            {
                Instantiate(splashParticle, collision.ClosestPoint(transform.position), Quaternion.Euler(-90,0,0));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "magicHitbox" || collision.gameObject.tag == "attackHitbox") && !isBoss) //caught
        {
            Instantiate(pp, tr.position, Quaternion.identity); //death particles
            pdc.addPoints(pointVal); //add them points
            gb.fishCaught(index); //change the index in fish caught
            audioS.Play();
            Destroy(gameObject); //destroy the fish
        }
        if(collision.gameObject.tag == "magicHitbox"&& isBoss)
        {
            Instantiate(pp, tr.position, Quaternion.identity); //death particles
            pdc.addPoints(pointVal); //add them points
            gb.fishCaught(index); //change the index in fish caught
            audioS.Play();
            Destroy(gameObject); //destroy the fish
        }
        if (collision.gameObject.name.Contains("Mouth"))
        {
            audioS.Play();
            Instantiate(pp, tr.position, Quaternion.identity); //death particles
            Destroy(gameObject); //destroy the fish
        }
    }

}
