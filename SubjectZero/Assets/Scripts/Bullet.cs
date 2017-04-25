using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject blood;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Door")
        {
            Destroy(gameObject);
        }
        else if( col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
        
    }
}
