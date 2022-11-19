using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turle_script:MonoBehaviour {
    #region Initialize
    void Start() {
        patrol=true;
        phase2=false;
        dead=false;
        rb=GetComponent<Rigidbody2D>();
        GameObject.Find("aRad").GetComponent<CircleCollider2D>().radius=aRadius;
        maxHeatlh=health;
    }
    public static bool patrol, dead,phase2;
    [SerializeField]
    float aRadius, tq;
    [SerializeField]
    int maxHeatlh,health, damage,yMin,yMax;
    [SerializeField]
    Vector2 knockBack;

    Rigidbody2D rb;
    GameObject target;
    void FixedUpdate() {
        if(maxHeatlh/4==health)
            phase2=true;
        if(patrol) patrolMove();
        else seekingMove();
    }
    #endregion
    /*
         * 
         * patrol and attack mode
         * 
         * generally slow moving
         * bool to check if in shell??
         * 
         * 
         * cant hit top, can hit bottom
         * 
         * fleshy bottom.
         * 
         * hdyrothermal vent turrle
         * 
         * 
         * 
         * patrols area, only attacks once in it
         * 
         * 
         * bite attack
         * breath weapons
         * 
         * 
         * 
         * radius of following
         * 
         * melon armor 
         * 
         */

    void patrolMove() {
        Vector2 velocity = Vector2.up;
        velocity=Vector3.Cross((Vector3)velocity,(Vector3)gameObject.transform.position);
        rb.velocity=velocity;
        /*
         * 
         * see if other fish are close, if so set target to that fish
         * 
         * 
         * 
         * 
         */
    }
    void seekingMove() {
        /*
         * determine how close target
         * 
         * if in biting range, always proritize bite
         * 
         * if far, try doing breath attack even it would fail
         * 
         * 
         * 
         * 
         */
    }

    #region Attacks
    IEnumerator biteAttack(float length) {
        yield return new WaitForSeconds(length);
    }
    //0 posion, 1 fire, 2 ice
    IEnumerator breathAttack(float length,byte type) {
        yield return new WaitForSeconds(length);
    }
    IEnumerator cancelAll(float delay) {
        yield return new WaitForSeconds(delay);
        CancelInvoke();
    }


    #endregion

    #region Collisions/Damage
    /*
     * 
     * circle collider, large and around turtule for vision
     * 
     * make sure its a trigger
     * if player enters, turn off patrol
     * otherwise, ignore
     * 
     * if exits trigger, patorl true
     * 
     * 
     */

    void onHit(Vector2 force) {
        health-=damage;
        rb.AddForce(knockBack*Vector2.Dot(force,Vector2.up));
        rb.AddTorque(tq*Vector2.Dot(force,Vector2.up));
    }
    #endregion
}
