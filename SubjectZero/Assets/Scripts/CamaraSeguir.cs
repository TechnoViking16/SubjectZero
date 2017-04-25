using System.Collections;



using UnityEngine;



public class CamaraSeguir : MonoBehaviour
{

    GameObject player;

    bool followPlayer = true;




    // Use this for initialization

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

    }



    // Update is called once per frame

    void Update()
    {

        if (followPlayer == true)

            camFollowPlayer();

    }

    public void SetFollowPlayer(bool val)

    {

        followPlayer = val;

    }

    void camFollowPlayer()

    {

        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        this.transform.position = newPos;

    }

}


