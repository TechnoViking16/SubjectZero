using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //MOVIMIENTO
    public bool moviendo = false;
    float velocidad = 10.0f;


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
    public AudioClip musica;
    private AudioSource sourceMuscia;

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

    //TEXTO DE AMMO
    public Text AmmoCount;
    int counterAmmo;

    //CURSOR
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    //RESPAWN (ACTUAL SCENE)
    Scene ActualScene;
    //public Vector3 respawnPoint;

    //ARMAS EN POSESION
    bool scopeta;
    bool rifle;
    bool pistola;
    public GameObject arma;

    //ARMA SELECCIONADA
    bool scopeta_select;
    bool rifle_select;
    bool pistola_select;

    //DETECTOR DE ARMAS
    Collider2D col;

    //SPRITES ARMAS
    public Sprite PistolaMen;
    public Sprite ScopetaMen;
    public Sprite RifleMen;

    //PANTALLA DE MUERTE
    public Image PantallaDeMuerte;

    // Use this for initialization
    void Start () {
        rid = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        source = GetComponent<AudioSource>();
        sourceMuscia = GetComponent<AudioSource>();
        sourceMuscia.PlayOneShot(musica);

        //HEALTH
        damageImage.enabled = false;
        currentHealth = startingHealth;

        //AMMO
        counterAmmo = 60;
        AmmoCount.text = "AMMO " + counterAmmo;

        //RESPAWN (Escena Actual)
        ActualScene = SceneManager.GetActiveScene();
        //respawnPoint = transform.position;

        //SCOPE
        //mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        //Cursor.SetCursor(cursorTexture, mousePos, cursorMode);

        //ARMAS EN POSESION
        scopeta = false;
        rifle = false;
        pistola = true;

        //ARMA SELECCIONADA
        pistola_select = true;

        //PANTALLA DE MUERTE
        PantallaDeMuerte.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Mov();
        rotateCamera();
        armas();

        //DAÑO AL JUGADOR
        if (damaged)
        {
            damageImage.enabled = true;
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;

        //SCOPE
        //Cursor.SetCursor(cursorTexture, new Vector3(transform.position.x, transform.position.y, transform.position.z), cursorMode);
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
            Vector3 velUp = new Vector3();
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
        }

    }

    void rotateCamera()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
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

    //PANTALLA DE MUERTE
    /*private IEnumerator sizeImage()
    {

    }*/


    void Death()
    {

        isDead = true;
        //Destroy(gameObject);

        //RESPAWNEAMOS DESPUES DE UNA PARADA DE TIEMPO
        //SceneManager.LoadScene(ActualScene.name);
        PantallaDeMuerte.enabled = true;

        //transform.position = respawnPoint;
    }



    void armas()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("PISTOLA MEN");
            GetComponent<SpriteRenderer>().sprite = PistolaMen;
            pistola_select = true;
            scopeta_select = false;
            rifle_select = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (scopeta == true)
            {
                Debug.Log("ESCOPETA MEN");
                GetComponent<SpriteRenderer>().sprite = ScopetaMen;
                pistola_select = false;
                scopeta_select = true;
                rifle_select = false;
            }
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (rifle == true)
            {
                Debug.Log("RIFLE MEN");

                //CHANGE THE SPRITE OF RIFLE
                GetComponent<SpriteRenderer>().sprite = RifleMen;
                pistola_select = false;
                scopeta_select = false;
                rifle_select = true;
            }
        }

        //DISPAROS SEGUN LA ARMA
        if (Input.GetMouseButtonDown(0) && Time.time > NextFire && pistola_select==true)
        {
            FireRate = 0.5f;
            if (counterAmmo <= 0)
            {
                AmmoCount.text = "AMMO " + counterAmmo;
            }
            else
            {
                counterAmmo--;
                AmmoCount.text = "AMMO " + counterAmmo;
                source.PlayOneShot(sonidoDisparo, 1);
                NextFire = Time.time + FireRate;


                Rigidbody2D bPrefab = Instantiate(bulletPrefab, new Vector3(Pistola.transform.position.x, Pistola.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;

                bPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
            }
        }
        else if(Input.GetMouseButtonDown(0) && Time.time > NextFire && scopeta_select == true)
        {
            if (counterAmmo <= 0)
            {
                AmmoCount.text = "AMMO " + counterAmmo;
            }
            else
            {
                counterAmmo--;
                AmmoCount.text = "AMMO " + counterAmmo;
                source.PlayOneShot(sonidoDisparo, 1);
                NextFire = Time.time + FireRate;


                Rigidbody2D bPrefab = Instantiate(bulletPrefab, new Vector3(Pistola.transform.position.x, Pistola.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;

                bPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
            }
        }
        else if (Input.GetMouseButton(0) && Time.time > NextFire && rifle_select == true)
        {
            FireRate = 0.2f;
            if (counterAmmo <= 0)
            {
                AmmoCount.text = "AMMO " + counterAmmo;
            }
            else
            {
                counterAmmo--;
                AmmoCount.text = "AMMO " + counterAmmo;
                source.PlayOneShot(sonidoDisparo, 1);
                NextFire = Time.time + FireRate;


                Rigidbody2D bPrefab = Instantiate(bulletPrefab, new Vector3(Pistola.transform.position.x, Pistola.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;

                bPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
            }
        }

    }

    //RECOGIDA DE ARMAS
    private void OnTriggerEnter2D(Collider2D col)
    {

        if(col.tag == "scopeta")
        {
            scopeta = true;
        }
        else if (col.tag == "rifle")
        {
            rifle = true;
            arma = GameObject.FindGameObjectWithTag("rifle");
            Destroy(arma);
            Debug.Log("RIFLE MEN");
        }

    }

}
