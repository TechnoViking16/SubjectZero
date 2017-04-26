﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject blood;
    public GameObject Player;
    public Player playerHealth;
    // Use this for initialization
    public int attackDamageToPlayer = 17;
    public int attackDamageToIA = 34;

    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        Player = GameObject.Find("Player");
        playerHealth = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Door")
        {
            Destroy(gameObject);
        }
        else if( col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);

        }
        
        else if (col.gameObject.tag == "Player")
        {
            //timer = 0.1f;
            Destroy(gameObject);
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamageToPlayer);
            }
        }

    }
}

