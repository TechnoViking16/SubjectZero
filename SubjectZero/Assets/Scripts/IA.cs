using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour
{
    public Transform target;//set target from inspector instead of looking in Update
    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;

    Rigidbody2D rid;

    void Start()
    {
        //enemyRotation = this.transform.localRotation;
        rid = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementIA();
        rotateIA();
    }

    void movementIA()
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
    }

    void rotateIA()
    {
        //rid.transform.eulerAngles = new Vector3((transform.position.y, transform.position.x, transform.position.z) transform.rotation);
    }
}
