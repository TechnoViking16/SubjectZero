﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject blood;

    //PLAYER
    public GameObject Player;
    public Player playerHealth;

    // VARIABLES OF DAMAGE
    public int attackDamageToPlayer = 17;
    public int attackDamageToIA = 34;

    //SPRITES DMG
    public Sprite DMG1;
    public Sprite DMG2;

    void Start()
    {
        //DAÑO AL PLAYER
        Player = GameObject.Find("Player");
        playerHealth = Player.GetComponent<Player>();

        //DAÑO A LA IA
        /*IA = GameObject.Find("enemy");
        IAhealth = IA.GetComponent<IA>();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Door" || col.gameObject.tag == "Fondo")
        {
            Destroy(gameObject);
        }
        else if( col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);

            if (col.gameObject.GetComponent<IA>().currentHealth > 0)
            {
                col.gameObject.GetComponent<IA>().TakeDamage(attackDamageToIA);
                if(col.gameObject.GetComponent<IA>().currentHealth == 66)
                {
                    col.gameObject.GetComponent<SpriteRenderer>().sprite = DMG1;

                }
                else if(col.gameObject.GetComponent<IA>().currentHealth == 32)
                {
                    col.gameObject.GetComponent<SpriteRenderer>().sprite = DMG2;
                }
                else
                {
                    Player.GetComponent<Player>().currentFury += 10;
                }
            }
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

