using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //OnCollisionEnter2D();
        //OnTriggerEnter("InteriorWalls");
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag=="Door")
        {
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
