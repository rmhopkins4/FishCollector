using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nettest : MonoBehaviour
{

    Animator anim;

    public bool isPressed = false;

    public bool busy = false;
    public bool charging = false;
    public bool swinging = false;

    AudioSource audioSource;
    public AudioClip charge1, charge2, charge3;

    public PlayerDataController pdc;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("Button Pressed", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Button Pressed", false);
        }
    }

    void charge1sound()
    {
        audioSource.PlayOneShot(charge1);
        charging = true;
    }
    void charge2sound()
    {
        audioSource.PlayOneShot(charge2);
    }
    void charge3sound()
    {
        audioSource.PlayOneShot(charge3);
    }
    void swingsound()
    {
        audioSource.Play();
        swinging = true;
        charging = false;
    }

    public void tellButtonPressed()
    {
        if (pdc.meter >= 2000)
        {
            anim.SetBool("Button Pressed", true);
            pdc.ChangeMagic(-2000);
        }
    }

    public void tellButtonUnPressed()
    {
        anim.SetBool("Button Pressed", false);
    }

    public void tellBusy()
    {
        busy = true;
    }

    public void tellUnBusy()
    {
        busy = false;
    }
}
