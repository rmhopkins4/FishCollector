using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class quitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1;
            GameObject.Find("Global Handler").GetComponent<LightChanger>().enabled = false;
            GameObject.Find("Global Handler").GetComponent<cameraController>().enabled = false;
            SceneManager.LoadScene("endScreen");
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
        }
    }
    public static void endGame()
    {
        Time.timeScale = 1;
        GameObject.Find("Global Handler").GetComponent<LightChanger>().enabled = false;
        GameObject.Find("Global Handler").GetComponent<cameraController>().enabled = false;
        SceneManager.LoadScene("endScreen");
    }
    public static void quitOut()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
}
