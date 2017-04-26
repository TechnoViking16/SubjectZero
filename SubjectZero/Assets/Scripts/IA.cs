
using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour
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
    GameObject PistolaIA;
    public GameObject bullet;
    //public float speed = 5.0f;
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 500;
    public float attackSpeed = 0.5f;
    public float FireRate = 0.5f;
    public float NextFire;


    private Collider2D colisionCono;
    public bool encontrado;
    private bool encontrar;

    private float dist;

    void Start()
    {

        colisionCono = GetComponentInChildren<Collider2D>();
        enemyRotation = this.transform.localRotation;
        source = GetComponent<AudioSource>();
         encontrado = false;
    }




    void Update()
    {
        
        
         dist = Vector3.Distance(target.position, transform.position);
        

        if (encontrar == true && dist > 7)
        {
            


            rotateIA();
            disparos();
        }
        else if (encontrar == true && dist < 7)
        {
            movementIA();
            rotateIA();
        }
        else {
        }
    }





    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            encontrar = true;
            
        }
        
    }


   



    void movementIA()
    {
        //Distancia del jugador
        float dist = Vector3.Distance(target.position, transform.position);
        
        playerPos = new Vector2(target.localPosition.x, target.localPosition.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position
        transform.position = Vector2.MoveTowards(enemyPos, playerPos, 4 * Time.deltaTime);  
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
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(sonidoDisparoIA, vol);

            NextFire = Time.time + FireRate;

            Rigidbody2D bPrefab = Instantiate(bulletPrefab, new Vector3(PistolaIA.transform.position.x, PistolaIA.transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;

            bPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
        }
    }
}
