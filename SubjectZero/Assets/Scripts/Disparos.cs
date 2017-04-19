using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparos : MonoBehaviour
{

    //public GameObject bullet;
    public GameObject currentBulletPrefab;

    private List<GameObject> Projectiles = new List<GameObject>();

    public float bulletVelocity = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (Vector2)((worldMousePos - transform.position));
            direction.Normalize();

            // Creates the bullet locally
            GameObject bullet = (GameObject)Instantiate(
                                    currentBulletPrefab,
                                    transform.position + (Vector3)(direction * 0.5f),
                                    Quaternion.identity);

            // Adds velocity to the bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;
        }
    }
}