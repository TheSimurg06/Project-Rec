using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flashlight;
    public GameObject whiteLight;
    private Light fener;
    private bool onOff = false;
    public float maxIntensity = 2.1f; 
    private float intensity = 2.1f;
    public float intensityAcceleration = 0.1f;

    void Start()
    {
        //fener = whiteLight.GetComponent<Light>();
        //fener.intensity = 0f;
        whiteLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
         if (Input.GetKeyDown("f"))
        {
            if(onOff) 
            {
                whiteLight.SetActive(false);
                onOff = false;
            }   
            else
            {
                whiteLight.SetActive(true);
                onOff = true;
            }
        }
    }

}
