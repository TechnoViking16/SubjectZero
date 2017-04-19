using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector3 direction;
    string creator;
    EnemyAttacked attacked;
    public GameObject bloodImpact, wallImpact;
    float timer = 20.0f;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction*17*Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer<=0)
        {
            Destroy(this.gameObject);
        }
	}

    public void setVals(Vector3 dir, string name)
    {
        direction = dir;
        creator = name;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            attacked = col.gameObject.GetComponent<EnemyAttacked>(); 
            attacked.killBullet();
            Instantiate(bloodImpact, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);

        }
    }
}


