using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject whiteLight;
    public Light fener;
    private bool onOff = false;
    public float maxIntensity = 2.1f; 
    private float intensity = 0f;
    public float intensityAcceleration = 0.1f;

    void Start()
    {
        fener = whiteLight.GetComponent<Light>();
        fener.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
         if (Input.GetKeyDown("f"))
        {
            if(onOff) 
            {
                intensity = 0f;
                fener.intensity = intensity;
                onOff = false;
            }   
            else
            {
                intensity = 2.1f;
                fener.intensity = intensity;
                onOff = true;
            }
        }
    }

    private void LightOn(){
        while(intensity < maxIntensity) {
            intensity += intensityAcceleration * Time.deltaTime;
            fener.intensity = intensity;
            Debug.Log("intensity = " + intensity);
        }
        fener.intensity = maxIntensity;
    }
}
