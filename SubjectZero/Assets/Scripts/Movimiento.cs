using UnityEngine;

public class Movimiento : MonoBehaviour {
    public bool moviendo = false;
    float velocidad = 8.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mov();	
	}

    void Mov() {
       
        
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
        if (Input.GetKey(KeyCode.S)!=true&& Input.GetKey(KeyCode.D) != true&& Input.GetKey(KeyCode.W) != true&& Input.GetKey(KeyCode.A) != true)
        {
            
            moviendo = false;
        }

    }




}
