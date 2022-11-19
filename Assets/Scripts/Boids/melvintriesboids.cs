using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class melvintriesboids : MonoBehaviour
{
    #region Vars
    Rigidbody2D rb;
    public List<GameObject> nearbyBoids = new List<GameObject>();

	//3 boid rules parameters
    public float cohesionStrength;
    public float cohesionDistance;
    public float separationStrength;
    public float separationDistance;
    public float alignmentStrength;
    public float alignmentDistance;
	
	public float rayDistance;
	public float rayHoriForce;
    public float rayVertForce;
    public float sensorOffset = 0f;

    //wall parameters
    public int xmin, xmax, ymin, ymax;
	public float wallStrength;

	//speed limit
    public float vLim;
    #endregion
    // Start is called before the first frame update
    #region Working Functions
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
		
		//make boid list
		checkBoids();
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {		
		//run boid movement
        moveBoid();
		//limit speed
        limitVelocity(gameObject);

        //calculate heading

        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rb.velocity.y,rb.velocity.x)*180 / Mathf.PI);
    }
    #endregion
    void moveBoid()
    {
        Vector2 v1, v2, v3, v4, v5;
        v1 = new Vector2(0, 0);
        v2 = new Vector2(0, 0);
        v3 = new Vector2(0, 0);
        v4 = new Vector2(0, 0);
        v5 = new Vector2(0, 0); 
        
		
		
		RaycastHit2D rayUp = Physics2D.Raycast(transform.position, transform.right + transform.up * sensorOffset , rayDistance);
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, transform.right + transform.up * -sensorOffset, rayDistance);

        Vector3 drawUp = transform.right + transform.up * sensorOffset;
        Vector3 drawDown = transform.right + transform.up * -sensorOffset;

        Debug.DrawRay(transform.position, drawUp.normalized * rayDistance, Color.green);
        Debug.DrawRay(transform.position, drawDown.normalized * rayDistance, Color.red);

        if (rayUp.collider != null || rayDown.collider != null)
        {
            bool upTrue = false;
            bool downTrue = false;

            if(rayUp.collider != null && rayUp.collider.gameObject.CompareTag("ground"))
            {
                upTrue = true;
            }

            if(rayDown.collider != null && rayDown.collider.gameObject.CompareTag("ground"))
            {
                downTrue = true;
            }

            v5 = rayDodge(upTrue, downTrue);
        }
		v1 = rule1(this.gameObject); //cohesion
        v2 = rule2(this.gameObject) * separationStrength; //separation
        v3 = rule3(this.gameObject); //alignment
		
        v4 = boundPosition(this.gameObject) * wallStrength; //wall bounding
		//add all the forces to the boid
		
        rb.AddForce(v1);
        rb.AddForce(v2);
        rb.AddForce(v3);
        rb.AddForce(v4);
		rb.AddForce(v5);
    }

     Vector2 rule1(GameObject b) //cohesion
    {
		//this force calculates the average position of nearby boids (within cohesionDistance) and adds force to each boid towards that center. (proportional to distance from center)
        Vector2 averagePosition = new Vector2(0, 0);
        int count = 0;
        for(int i = 0; i < nearbyBoids.Count; i++)
        {
            float dist = Vector3.Distance(this.transform.position, nearbyBoids[i].transform.position);
            if(dist < cohesionDistance)
            {
                averagePosition += new Vector2(nearbyBoids[i].transform.position.x, 0);
                averagePosition += new Vector2(0, nearbyBoids[i].transform.position.y);
                count++;
            }
        }
        averagePosition /= count; //here is where the average position is finalized)

        Vector2 displacement = new Vector2(averagePosition.x - this.transform.position.x, averagePosition.y - this.transform.position.y); //calculates how far off the center is 
        Vector2 cohesionForce = new Vector2(displacement.x * cohesionStrength, displacement.y * cohesionStrength); //translates that into how much force is needed
        return cohesionForce;
    }

    Vector2 rule2(GameObject b) //separation
    {
		//this force attempts to prevent the fish from stacking by pushing each boid away from boids within separationDistance.
        Vector2 separationForce = new Vector2(0, 0);
        for(int i = 0; i < nearbyBoids.Count; i++)
        {
            float dist = Vector3.Distance(this.transform.position, nearbyBoids[i].transform.position); //finds distance between boids
            if(dist < separationDistance)
            {
                Vector2 offset = new Vector2(this.transform.position.x - nearbyBoids[i].transform.position.x, this.transform.position.y - nearbyBoids[i].transform.position.y); //calculates offset which is just vector between boids

                Vector2 normalizedOffset = offset.normalized; //finds direction of vector

                Vector2 force = new Vector2(normalizedOffset.x /= dist, normalizedOffset.y /= dist); //s force inversely proportional to distance (closer = more force)
                separationForce = force;
            }
        }
        return separationForce;
    }

     Vector2 rule3(GameObject b) //alignment
    {
		//this force tries to make the velocities of nearby boids within alignmentDistance be the same. (very similar to cohesion)
        Vector2 averageVelocity = new Vector2(0, 0);
        int count = 0;
        for (int i = 0; i < nearbyBoids.Count; i++)
        {
            float dist = Vector3.Distance(this.transform.position, nearbyBoids[i].transform.position);
            if (dist < alignmentDistance)
            {
                averageVelocity += new Vector2(nearbyBoids[i].GetComponent<Rigidbody2D>().velocity.x, 0);
                averageVelocity += new Vector2(0, nearbyBoids[i].GetComponent<Rigidbody2D>().velocity.y);
                count++;
            }
        }
        averageVelocity /= count; //finds average velocity of nearby boids

        Vector2 alignmentForce = new Vector2(averageVelocity.x * alignmentStrength, averageVelocity.y * alignmentStrength); //builds alignment force in direction of average velocity of nearby boids
        return alignmentForce;
    }

     Vector2 boundPosition(GameObject b) //wall bounds
    {
		//simply adds force back into bounds when boid goes out
        Vector2 v = new Vector2();

        if(b.transform.position.x <= xmin)
        {
            v += new Vector2(10, 0) * wallStrength;
        }
        else if(b.transform.position.x >= xmax)
        {
            v += new Vector2(-10, 0) * wallStrength;
        }
        if (b.transform.position.y <= ymin)
        {
            v += new Vector2(0, 10) * wallStrength;
        }
        else if (b.transform.position.y >= ymax)
        {
            v += new Vector2(0, -10) * wallStrength;
        }
        return v;
    }
	Vector2 rayDodge(bool upHit, bool downHit) //use raycast to dodge walls
	{
        Vector2 returnVec = new Vector2(0, 0);
        if(upHit)
        {
            if (downHit) //if both
            {
                returnVec = transform.right * -rayHoriForce;
            }

            else //if just up
            {
                returnVec = transform.right * -rayHoriForce + transform.up * -rayVertForce;
            }
        }

        else if(downHit) //if just down
        {
            returnVec = transform.right * -rayHoriForce + transform.up * rayVertForce;
        }
        return returnVec;
	}	

    void checkBoids() //build boid list
    {
        foreach (GameObject boid in GameObject.FindGameObjectsWithTag("boid"))
        {
            bool containsBoid = nearbyBoids.Any(i=> i == boid);
            if(!containsBoid)
            {
				if(boid != this.gameObject)
				{
					nearbyBoids.Add(boid);
				}
            }
        }
    }

    void limitVelocity(GameObject b) //speed limit
    {
		//if veloctiy magnitude (speed) is too high, normalizes vector and sets speed to speed limit. 
        if (b.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= vLim)
        {
            b.gameObject.GetComponent<Rigidbody2D>().velocity = b.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            b.gameObject.GetComponent<Rigidbody2D>().velocity *= vLim;
        }
    }
}