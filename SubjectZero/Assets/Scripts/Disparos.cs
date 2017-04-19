using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparos : MonoBehaviour {

    public GameObject proyectilPre;

    private List<GameObject> Proyectiles = new List<GameObject>();

    private float projectileVelocity;

	// Use this for initialization
	void Start () {
        projectileVelocity = 3;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = (GameObject)Instantiate(proyectilPre, transform.position, Quaternion.identity);
            Proyectiles.Add(bullet);
        }	

        for(int i = 0; i < Proyectiles.Count; i++)
        {
            GameObject goBullet = Proyectiles[i];
            if(goBullet != null)
            {
                goBullet.transform.Translate(new Vector3(0, 1) * Time.deltaTime * projectileVelocity);

                Vector3 bulletScreenPos = Camera.main.WorldToScreenPoint(goBullet.transform.position);
                if(bulletScreenPos.y >= Screen.height || bulletScreenPos.y <= 0)
                {
                    DestroyObject(goBullet);
                    Proyectiles.Remove(goBullet);
                }

            }
        }
	}
}
