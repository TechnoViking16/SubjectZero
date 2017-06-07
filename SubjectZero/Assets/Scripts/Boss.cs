using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{

    public Transform target;//set target from inspector instead of looking in Update
    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;

    public Transform targetStation;
    private float speed = 5.0f;

    //Audio
    public AudioClip sonidoDisparoIA;
    private AudioSource source;
    private float volLowRange = .3f;
    private float volHighRange = .8f;



    [SerializeField]
    GameObject PistolaIA1;
    [SerializeField]
    GameObject PistolaIA2;
    [SerializeField]
    GameObject PistolaIA3;
    [SerializeField]
    GameObject PistolaIA4;
    [SerializeField]
    GameObject PistolaIA5;
    [SerializeField]
    GameObject PistolaIA6;
    [SerializeField]
    GameObject PistolaDER1;
    [SerializeField]
    GameObject PistolaDER2;
    [SerializeField]
    GameObject PistolaDER3;
    [SerializeField]
    GameObject PistolaDER4;
    [SerializeField]
    GameObject PistolaDER5;
    [SerializeField]
    GameObject PistolaIZQ1;
    [SerializeField]
    GameObject PistolaIZQ2;
    [SerializeField]
    GameObject PistolaIZQ3;
    [SerializeField]
    GameObject PistolaIZQ4;
    [SerializeField]
    GameObject PistolaIZQ5;



    public GameObject bullet;
    //public float speed = 5.0f;
    public Rigidbody2D bulletPrefab;
    public Rigidbody2D bulletPrefab1;
    public Rigidbody2D bulletPrefab2;
    public Rigidbody2D bulletPrefab3;
    public Rigidbody2D bulletPrefab4;
    public Rigidbody2D bulletPrefab5;
    public Rigidbody2D bulletPrefabDER1;
    public Rigidbody2D bulletPrefabDER2;
    public Rigidbody2D bulletPrefabDER3;
    public Rigidbody2D bulletPrefabDER4;
    public Rigidbody2D bulletPrefabDER5;
    public Rigidbody2D bulletPrefabIZQ1;
    public Rigidbody2D bulletPrefabIZQ2;
    public Rigidbody2D bulletPrefabIZQ3;
    public Rigidbody2D bulletPrefabIZQ4;
    public Rigidbody2D bulletPrefabIZQ5;

    public float bulletSpeed = 500;
    public float attackSpeed = 0.5f;
    public float FireRate = 0.5f;
    public float NextFire;


    private Collider2D colisionCono;
    public bool encontrado;
    private bool encontrar;

    //HEALTH
    bool isDead;
    bool damaged;
    public int startingHealth = 100;
    public int currentHealth;

    private float dist;

    //AMMO
    public Rigidbody2D ammoPrefab;
    public int ammoNum;

    //BOOLEANS DE MOVIMIENTO
    bool derecha;
    bool izquierda;
    bool gotHit;

    //SPRITES
    public Sprite DMG1;

    //IMAGEN FINAL
    public Image IMG1;
    float delay = 3;
    bool timerEnabled;

    //PARED DESTRUCTIVA
    public GameObject wallDestruct;

    void Start()
    {

        colisionCono = GetComponentInChildren<Collider2D>();
        enemyRotation = this.transform.localRotation;
        source = GetComponent<AudioSource>();
        encontrado = false;

        //HEALTH
        currentHealth = startingHealth;

        //AMMO
        ammoNum = 5;

        //VELOCIDAD MOVIMIENTO
        speed = 3.0f;

        //BOOLEANS DE MOVIMIENTO
        derecha = false;
        izquierda = false;



    }




    void Update()
    {
        if(isDead==false)
        {
            if (izquierda == false && derecha == false)
            {
                transform.Translate(-Vector3.right * speed * Time.deltaTime);
            }
            else if (izquierda == true && derecha == false)
            {
                transform.Translate(-Vector3.right * speed * Time.deltaTime);
            }
            else if (derecha == true && izquierda == false)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            disparos();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DMG1;
        }
       
    }





    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "wallIzquierda")
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            izquierda = false;
            derecha = true;
            
        }
        else if(col.name == "wallDerecha")
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            izquierda = true;
            derecha = false;
        }
    }


    void disparos()
    {
        if (Time.time > NextFire)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(sonidoDisparoIA, vol);

            NextFire = Time.time + FireRate;

            //PRIMER CAÑON
            Rigidbody2D bPrefab1 = Instantiate(bulletPrefab, new Vector3(PistolaIA1.transform.position.x, PistolaIA1.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1, -1, 0) * bulletSpeed);
            bPrefab1.transform.Rotate(Vector3.forward * -135);

            //SEGUNDO CAÑON
            Rigidbody2D bPrefab2 = Instantiate(bulletPrefab, new Vector3(PistolaIA2.transform.position.x, PistolaIA2.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefab2.GetComponent<Rigidbody2D>().AddForce(new Vector3(1,-1,0) * bulletSpeed);
            bPrefab2.transform.Rotate(Vector3.forward * -45);

            //TERCER CAÑON
            Rigidbody2D bPrefab3 = Instantiate(bulletPrefab, new Vector3(PistolaIA3.transform.position.x, PistolaIA3.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefab3.GetComponent<Rigidbody2D>().AddForce(Vector3.down * bulletSpeed);
            bPrefab3.transform.Rotate(Vector3.forward * -90);

            //CUARTO CAÑON
            Rigidbody2D bPrefab4 = Instantiate(bulletPrefab, new Vector3(PistolaIA4.transform.position.x, PistolaIA4.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefab4.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1, -1, 0) * bulletSpeed);
            bPrefab4.transform.Rotate(Vector3.forward * -135);

            //QUINTO CAÑON
            Rigidbody2D bPrefab5 = Instantiate(bulletPrefab, new Vector3(PistolaIA5.transform.position.x, PistolaIA5.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefab5.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, -1, 0) * bulletSpeed);
            bPrefab5.transform.Rotate(Vector3.forward * -45);

            //CAÑONES DERECHA
            Rigidbody2D bPrefabDER1 = Instantiate(bulletPrefab, new Vector3(PistolaDER1.transform.position.x, PistolaDER1.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabDER1.GetComponent<Rigidbody2D>().AddForce(Vector3.right * bulletSpeed);
            bPrefabDER1.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabDER2 = Instantiate(bulletPrefab, new Vector3(PistolaDER2.transform.position.x, PistolaDER2.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabDER2.GetComponent<Rigidbody2D>().AddForce(Vector3.right * bulletSpeed);
            bPrefabDER2.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabDER3 = Instantiate(bulletPrefab, new Vector3(PistolaDER3.transform.position.x, PistolaDER3.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabDER3.GetComponent<Rigidbody2D>().AddForce(Vector3.right * bulletSpeed);
            bPrefabDER3.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabDER4 = Instantiate(bulletPrefab, new Vector3(PistolaDER4.transform.position.x, PistolaDER4.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabDER4.GetComponent<Rigidbody2D>().AddForce(Vector3.right * bulletSpeed);
            bPrefabDER4.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabDER5 = Instantiate(bulletPrefab, new Vector3(PistolaDER5.transform.position.x, PistolaDER5.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabDER5.GetComponent<Rigidbody2D>().AddForce(Vector3.right * bulletSpeed);
            bPrefabDER5.transform.Rotate(Vector3.right);


            //CAÑONES IZQUIERDA
            Rigidbody2D bPrefabIZQ1 = Instantiate(bulletPrefab, new Vector3(PistolaIZQ1.transform.position.x, PistolaIZQ1.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabIZQ1.GetComponent<Rigidbody2D>().AddForce(Vector3.left * bulletSpeed);
            bPrefabIZQ1.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabIZQ2 = Instantiate(bulletPrefab, new Vector3(PistolaIZQ2.transform.position.x, PistolaIZQ2.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabIZQ2.GetComponent<Rigidbody2D>().AddForce(Vector3.left * bulletSpeed);
            bPrefabIZQ2.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabIZQ3 = Instantiate(bulletPrefab, new Vector3(PistolaIZQ3.transform.position.x, PistolaIZQ3.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabIZQ3.GetComponent<Rigidbody2D>().AddForce(Vector3.left * bulletSpeed);
            bPrefabIZQ3.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabIZQ4 = Instantiate(bulletPrefab, new Vector3(PistolaIZQ4.transform.position.x, PistolaIZQ4.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabIZQ4.GetComponent<Rigidbody2D>().AddForce(Vector3.left * bulletSpeed);
            bPrefabIZQ4.transform.Rotate(Vector3.right);

            Rigidbody2D bPrefabIZQ5 = Instantiate(bulletPrefab, new Vector3(PistolaIZQ5.transform.position.x, PistolaIZQ5.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
            bPrefabIZQ5.GetComponent<Rigidbody2D>().AddForce(Vector3.left * bulletSpeed);
            bPrefabIZQ5.transform.Rotate(Vector3.right);
        }
    }

    //HEALTH FUNCTIONS
    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        Destroy(wallDestruct);
    }

    void rotateIA()
    {
        Vector3 direction = targetStation.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    //DELAY
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
    }

    void OnPreRender()
    {
        timerEnabled = true;
    }
}
