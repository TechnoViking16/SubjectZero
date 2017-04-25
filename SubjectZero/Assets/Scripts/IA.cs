
using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour
{
    public Transform target;//set target from inspector instead of looking in Update
    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;

    public Transform targetStation;
    private float speed = 5.0f;

    [SerializeField]
    GameObject PistolaIA;
    public GameObject bullet;
    //public float speed = 5.0f;
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 500;
    public float attackSpeed = 0.5f;
    public float FireRate = 0.5f;
    public float NextFire;


    void Start()
    {
        enemyRotation = this.transform.localRotation;
    }

    void Update()
    {
        movementIA();
        rotateIA();
    }

    void movementIA()
    {
        //Distancia del jugador
        float dist = Vector3.Distance(target.position, transform.position);
        
        playerPos = new Vector2(target.localPosition.x, target.localPosition.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position

        if (dist >= 7.5)//move towards if not close by 
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos, 4 * Time.deltaTime);
            disparos();
        }
        if (dist < 0)//stay if too close 
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos, 0 * Time.deltaTime);
        }
    }

    void rotateIA()
    {
        Vector3 direction = targetStation.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    void disparos()
    {
        if (Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;

            Rigidbody2D bPrefab = Instantiate(bulletPrefab, new Vector3(PistolaIA.transform.position.x, PistolaIA.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;

            bPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
        }
    }
}
