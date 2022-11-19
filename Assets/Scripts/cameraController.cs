using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public static Camera main, comp, paus;
    public static GameObject compObj;

    bool a = false;
    bool b = false;
    private PlayerInputActions playerInputActions;
    void Awake()
    {
        main = GameObject.Find("MainCam").GetComponent<Camera>();
        comp = GameObject.Find("CompCam").GetComponent<Camera>();
        paus = GameObject.Find("PauseCam").GetComponent<Camera>();
        compObj = GameObject.Find("CompPref");
        compObj.SetActive(true);
        main.enabled = true;
        comp.enabled = false;
        paus.enabled = false;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Pause.performed += cntxt => camSwitch(1);
        playerInputActions.Player.Compen.performed += cntxt => camSwitch(2);
    }

    private void FixedUpdate()
    {
        if(a == true && b == false)
        {
            compObj.SetActive(false);
            b = true;
        }
        if(a == false)
        {
            a = true;
        }
    }
    private void OnEnable() => playerInputActions.Player.Enable();//When object is enabled, inputs can be read
    private void OnDisable() => playerInputActions.Player.Disable();//Vice versa, disabled object not readable
    void camSwitch(byte isPaused)
    {
        if (isPaused == 1)
        {
            if (main.enabled)
            {
                Time.timeScale = 0;
                //pausCan.SetActive(true);
                paus.enabled = true;
                main.enabled = false;
            }
            else if (comp.enabled)
            {
                paus.enabled = true;
                comp.enabled = false;
            }
            else
            {
                Time.timeScale = 1;
                main.enabled = true;
                paus.enabled = false;
            }
        }
        if (isPaused == 2)
        {
            Time.timeScale = 1;

            if (main.enabled)
            {
                GameObject.Find("TamerPlayer").GetComponent<QuestHandler>().MiscQuestActivity(3);


                compObj.SetActive(true);
                compObj.GetComponentInChildren<flipControl>().LoadBook(flipControl.pageGroup);
                comp.enabled = true;
                main.enabled = false;
            }
            else if (paus.enabled)
            {
                GameObject.Find("TamerPlayer").GetComponent<QuestHandler>().MiscQuestActivity(3);


               compObj.SetActive(true);
                compObj.GetComponentInChildren<flipControl>().LoadBook(flipControl.pageGroup);
                comp.enabled = true;
                paus.enabled = false;
            }
            else if(compObj.GetComponentInChildren<flipControl>().waitTimer <= 0)
            {
                compObj.SetActive(false);
                Time.timeScale = 1;
                main.enabled = true;
                paus.enabled = false;
            }
        }
    }
}
