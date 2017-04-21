using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour
{
    /*
        Transform Player;
        float RotationSpeed = 3.0f;
        float MoveSpeed = 3.0f;

        void Start() {

            Player = GameObject.FindGameObjectWithTag("Player").transform;

        }

        void Update() {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.position - transform.position), RotationSpeed * Time.deltaTime);

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }

    }*/


    public Transform target;//set target from inspector instead of looking in Update
    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;


    void Start()
    {
        enemyRotation = this.transform.localRotation;
    }

    void Update()
    {
        playerPos = new Vector2(target.localPosition.x, target.localPosition.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position

        if (Vector3.Distance(transform.transform.position, target.transform.position) > 0.2)//move towards if not close by 
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos, 4 * Time.deltaTime);
        }
        if (Vector3.Distance(transform.transform.position, target.transform.position) < 0)//stay if too close 
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos, 0 * Time.deltaTime);
        }
        


        if (target.position.x > transform.rotation.z)//rotates enemy to the right if player is to the right  
        {
            //transform.LookAt(target);
            enemyRotation.z = 0;
            transform.localRotation = enemyRotation;
        }/*
        if (target.position.x < transform.rotation.z)//rotates enemy to the left if player is to the left 
        {
            //transform.LookAt(target);
            enemyRotation.z = 180;
            transform.localRotation = enemyRotation;
        }*/
    }
}
