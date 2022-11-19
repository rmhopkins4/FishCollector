using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sliderController : MonoBehaviour
{
	
	public GameObject sliderText;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Slider>().value = flipControl.pageGroup;
        sliderText.GetComponent<TextMeshProUGUI>().text = 2*(flipControl.pageGroup)-1 + "/" +  2*(flipControl.pageGroup);
    }
}
