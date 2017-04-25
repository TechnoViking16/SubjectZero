using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //MOVIMIENTO
    public bool moviendo = false;
    float velocidad = 9.0f;
    [SerializeField]
    GameObject Pistola;

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

    //Audio
    public AudioClip sonidoDisparo;
    private AudioSource source;
    private float volLowRange =.5f;
    private float volHighRange = 1.2f;

    //BARRA DE VIDA

    // Use this for initialization

    // Use this for initialization
    void Start () {
        rid = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Mov();
        rotateCamera();
        disparos();
    }

    void Mov()
    {


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime, Space.World);
            moviendo = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime, Space.World);
            moviendo = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);
            moviendo = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
            moviendo = true;
        }
        if (Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.D) != true && Input.GetKey(KeyCode.W) != true && Input.GetKey(KeyCode.A) != true)
        {

            moviendo = false;
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
}
