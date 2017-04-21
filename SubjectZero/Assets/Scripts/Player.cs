using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float speed = 5.0f;

    // Use this for initialization

    // Use this for initialization
    void Start () {
        rid = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
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
        if (Input.GetMouseButtonDown(0))
        {
           
            Instantiate(bullet, new Vector3(Pistola.transform.position.x,Pistola.transform.position.y,this.transform.position.z), transform.rotation);
            //transform.Translate(new Vector3(bullet.transform.position.x * 25 * Time.deltaTime, bullet.transform.position.y * 25 * Time.deltaTime, 0f));
        }
    }
}
