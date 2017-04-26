using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //MOVIMIENTO
    public bool moviendo = false;
    float velocidad = 10.0f;
    float Horizontal = Input.GetAxis("Horizontal");
    float Vertical = Input.GetAxis("Vertical");

    //CAMERA
    Vector3 mousePos;
    Vector3 direction;
    Camera cam;
    Rigidbody2D rid;

    //BULLET
    public GameObject bullet;
    //public float speed = 5.0f;
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 500;
    public float attackSpeed = 0.5f;
    public float FireRate = 0.5f;
    public float NextFire;
    [SerializeField]
    GameObject Pistola;

    //Audio
    public AudioClip sonidoDisparo;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.2f;

    //HEALTH
    public int startingHealth = 100;                            
    public int currentHealth;                                   
    public Slider healthSlider;                                 
    public Image damageImage;                                  
    //public AudioClip deathClip;                                
    public float flashSpeed = 5f;                               
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);    

    bool isDead;                                               
    bool damaged;

    // Use this for initialization

    // Use this for initialization
    void Start () {
        rid = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        source = GetComponent<AudioSource>();

        //HEALTH
        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {
        Mov();
        rotateCamera();
        disparos();

        //DAÑO AL JUGADOR
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    void Mov()
    {

        Vector3 vel = new Vector3();
        moviendo = false;

        //NO CHOQUE
        rid.velocity = Vector3.zero;
        rid.angularVelocity = 0;

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("UP");
            Vector3 velUp = new Vector3();
            // just use 1 to set the direction.
            velUp.y = 1;
            vel += velUp;
            moviendo = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 velDown = new Vector3();
            velDown.y = -1;
            vel += velDown;
            moviendo = true;
        }

        // no else here. Combinations of up/down and left/right are fine.
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 velLeft = new Vector3();
            velLeft.x = -1;
            vel += velLeft;
            moviendo = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 velRight = new Vector3();
            velRight.x = 1;
            vel += velRight;
            moviendo = true;
        }

            if (vel.magnitude > 0.001 && moviendo == true)
            {
                Vector3.Normalize(vel);
                vel *= velocidad;
                rid.velocity = vel;
            }
            else
            {
            rid.velocity = Vector3.zero;
            rid.angularVelocity = 0;
            //gameObject.rid.Sleep();
        }

    }

    void rotateCamera()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
    }

    void disparos()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > NextFire)
        {
            float vol = Random.Range(volLowRange,volHighRange);
            source.PlayOneShot(sonidoDisparo, vol);
            NextFire = Time.time + FireRate;
            

            Rigidbody2D bPrefab = Instantiate(bulletPrefab, new Vector3(Pistola.transform.position.x, Pistola.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;

            bPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);


        }
    }


    //HEALTH FUNCTIONS
    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        //CANCELAMOS FUNCIONES
        //Mov.enabled();
        //disparos.enabled();
    }
}
