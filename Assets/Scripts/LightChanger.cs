using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightChanger : MonoBehaviour
{
    public Light2D playerPointlight;
    public Light2D playerFlashLight;
    float depth;
    float pastdepth;
    public Transform player;
    public PlayerController playerMovement;
    public Rigidbody2D playerRB;

    [SerializeField] AudioSource a;
    [SerializeField] AudioSource b;

    public GameObject splashPart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        depth = player.position.y;

        //changes light intensity and radius based on depth

        if (depth > -900 && depth <= -15)
        {
            playerPointlight.pointLightOuterRadius = 0.05f * depth + 50;
            playerPointlight.intensity = 0.0009f * depth + 1.01f;
            //playerFlashLight.intensity = 0.0035f * depth + 1.075f;
        }
        else if (depth <= -15)
        {
            playerPointlight.pointLightOuterRadius = 5f;
            playerPointlight.intensity = 0.2f;
            //playerFlashLight.intensity = 0.2f;
        }
        else
        {
            playerPointlight.pointLightOuterRadius = 50f;
            playerPointlight.intensity = 1f;
            //playerFlashLight.intensity = 1f;
        }


        if(depth > 1)
        {
            playerMovement.enabled = false;
            playerRB.gravityScale = 3;
            playerRB.angularDrag = 0;
            playerRB.drag = 0.75f;
            playerRB.rotation = Mathf.Lerp(playerRB.rotation, 180f, Time.deltaTime/2);
            a.Pause();
            b.UnPause();
        }
        else
        {
            playerMovement.enabled = true;
            playerRB.gravityScale = 0;
            playerRB.drag = 1;
            playerRB.angularDrag = 0.5f;
            a.UnPause();
            b.Pause();
        }

        //splash
        if(pastdepth > 1 && depth <= 1)
        {
            Instantiate(splashPart, new Vector3(player.position.x, player.position.y,player.position.z - 5), Quaternion.Euler(-90, 0, 0));
            
        }

        if (pastdepth < 1 && depth >= 1)
        {
            Instantiate(splashPart, new Vector3(player.position.x, player.position.y, player.position.z - 5), Quaternion.Euler(-90, 0, 0));
        }

        pastdepth = player.position.y;
    }
}
