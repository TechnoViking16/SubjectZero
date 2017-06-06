using UnityEngine;
using System.Collections;

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


    public GameObject bullet;
    //public float speed = 5.0f;
    public Rigidbody2D bulletPrefab;
    public Rigidbody2D bulletPrefab1;
    public Rigidbody2D bulletPrefab2;
    public Rigidbody2D bulletPrefab3;
    public Rigidbody2D bulletPrefab4;
    public Rigidbody2D bulletPrefab5;
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
        //Destroy(gameObject);
    }

    void rotateIA()
    {
        Vector3 direction = targetStation.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }


    /*I9Enumerator TakeDamageBoss(int damage, int seconds)
    {
        gotHit = true;
        //substract damage from health
        currentHealth -= damage;
        //wait for x seconds
        gameObject.GetComponent<SpriteRenderer>().sprite = DMG3;
        yield return new WaitForSeconds(seconds);
        //after x seconds, the player can get hit again
        gotHit = false;
    }*/
}
