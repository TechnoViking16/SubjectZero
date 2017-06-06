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
    static public int counterAmmo = 60;

    //CURSOR
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector3 hotSpot = Vector3.zero;

    //RESPAWN (ACTUAL SCENE)
    Scene ActualScene;
    //public Vector3 respawnPoint;

    //ARMAS EN POSESION
    static public int scopeta = 0;
    static public int rifle = 0;
    static public int pistola = 0;
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

    //IMAGENES ARMAS INVENTARIO
    public Image rifleSprite;
    public Image blackImage;
    public Image whiteImagePistola;
    public Image whiteImageRifle;
    static public bool rifleImage = false;

    //PANTALLA DE MUERTE
    public Image PantallaDeMuerte;
    
    //COUNTER LEVEL
    public int counterLevel;
    public GameObject Ammunition;

    //FURIA
    public float currentSlowMo = 0.0f;
    public float slowTimeAllowed = 2.0f;
    public Slider furySlider;
    public float startingFury = 100.0f;
    public float currentFury;
    public Image furyImage;
    bool furiaActive;

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
        AmmoCount.text = counterAmmo.ToString(); 

        //RESPAWN (Escena Actual)
        ActualScene = SceneManager.GetActiveScene();

        //ARMA SELECCIONADA
        pistola_select = true;

        //PANTALLA DE MUERTE
        PantallaDeMuerte.enabled = false;

        //CAMBIO DE NIVEL
        counterLevel = 1;

        //FURY INICIAL
        furyImage.enabled = false;
        currentFury = startingHealth;
        furiaActive = false;

        //RIFLE DESACTIVADO DE PRIMERAS
        if(rifleImage == false)
        {
            rifleSprite.enabled = false;
            whiteImageRifle.enabled = false;
            blackImage.enabled = false;
        }

        //ARMA SELECCIONADA
        whiteImagePistola.color = new Color(255, 195, 0, 255);
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt("pause") == 0 )
        {
            Mov();
            rotateCamera();
            armas();
            BulletTime();
        }

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
            //SCOPE
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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

    public void Death()
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
            GetComponent<SpriteRenderer>().sprite = PistolaMen;
            pistola_select = true;
            scopeta_select = false;
            rifle_select = false;
            whiteImagePistola.color = new Color(255, 195, 0, 255);
            whiteImageRifle.color = new Color(255, 255, 255, 255);

        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (scopeta == 1)
            {
                GetComponent<SpriteRenderer>().sprite = ScopetaMen;
                pistola_select = false;
                scopeta_select = true;
                rifle_select = false;
            }
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (rifle == 1)
            {
                //CHANGE THE SPRITE OF RIFLE
                GetComponent<SpriteRenderer>().sprite = RifleMen;
                pistola_select = false;
                scopeta_select = false;
                rifle_select = true;
                whiteImageRifle.color = new Color(255, 195, 0, 255);
                whiteImagePistola.color = new Color(255, 255, 255, 255);
            }
        }

        //DISPAROS SEGUN LA ARMA
        if (Input.GetMouseButtonDown(0) && Time.time > NextFire && pistola_select==true)
        {
            FireRate = 0.5f;
            if (counterAmmo <= 0)
            {
                AmmoCount.text = counterAmmo.ToString();
            }
            else
            {
                counterAmmo--;
                AmmoCount.text = counterAmmo.ToString();
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
                AmmoCount.text = counterAmmo.ToString();
            }
            else
            {
                counterAmmo--;
                AmmoCount.text = counterAmmo.ToString();
                source.PlayOneShot(sonidoDisparo, 1);
                NextFire = Time.time + FireRate;


                Rigidbody2D bPrefab = Instantiate(bulletPrefab, new Vector3(Pistola.transform.position.x, Pistola.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;

                bPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
            }
        }
        else if (Input.GetMouseButton(0) && Time.time > NextFire && rifle_select == true)
        {
            FireRate = 0.15f;
            if (counterAmmo <= 0)
            {
                AmmoCount.text = counterAmmo.ToString();
            }
            else
            {
                counterAmmo--;
                AmmoCount.text = counterAmmo.ToString();
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
            scopeta = 1;
        }
        else if (col.tag == "rifle")
        {
            //Debug.Log(counterLevel);
            rifle = 1;
            arma = GameObject.FindGameObjectWithTag("rifle");
            Destroy(arma);
            rifleImage = true;
            rifleSprite.enabled = true;
            whiteImageRifle.enabled = true;
            blackImage.enabled = true;

        }
        else if (col.name == "finalLevel")
        {
            switch (counterLevel)
            {
                case 1:
                    counterLevel++;
                    SceneManager.LoadScene("Nivel" + counterLevel);
                break;
                case 2:
                    counterLevel++;
                    SceneManager.LoadScene("Nivel" + counterLevel);
                break;
            }
                
        }

        if(col.tag == "AmmoNum")
        {
            Ammunition = GameObject.FindGameObjectWithTag("AmmoNum");
            counterAmmo = counterAmmo + 5;
            AmmoCount.text = counterAmmo.ToString();

            //DESTRUIMOS EL PREFAB
            Destroy(GameObject.Find("AmmoCount(Clone)"));
        }
    }


    void BulletTime()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.4f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                furiaActive = true;
                furyImage.enabled = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                furiaActive = false;
                furyImage.enabled = false;
            }

            if (Time.timeScale == 0.4f)
            {
                currentSlowMo += Time.deltaTime;
            }

            if (currentSlowMo > slowTimeAllowed)
            {
                currentSlowMo = 0.0f;
                Time.timeScale = 1.0f;
            }

           
        }
        if (furiaActive == true)
        {
            currentFury -= 0.2f;
            furySlider.value = currentFury;

            if(currentFury<=0)
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                furiaActive = false;
                furyImage.enabled = false;
            }
            }
        }
    }

