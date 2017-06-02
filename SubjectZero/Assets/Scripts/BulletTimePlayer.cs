using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletTimePlayer : MonoBehaviour
{

    
    public float currentSlowMo = 0.0f;
    public float slowTimeAllowed = 2.0f;
    

    void Start() { }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0.4f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        if (Time.timeScale == 0.4f)
        {
            currentSlowMo += Time.deltaTime;
        }

        if (currentSlowMo > slowTimeAllowed)
        {
            currentSlowMo = 0.0f;
            Time.timeScale = 1.0f;
        }
    }
    */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1.0)
            {
                Time.timeScale = 0.4f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
            else
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }

            if (Time.timeScale == 0.4f)
            {
                currentSlowMo += Time.deltaTime;
            }

            if (currentSlowMo > slowTimeAllowed)
            {
                currentSlowMo = 0.0f;
                Time.timeScale = 1.0f;
            }
        }
    }
 }   
