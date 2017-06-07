﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject blood;

    //PLAYER
    public GameObject Player;
    public Player playerHealth;

    //BOSS
    public GameObject Boss;
    public Boss bossHealth;

    // VARIABLES OF DAMAGE
    public int attackDamageToPlayer = 17;
    public int attackDamageToIA = 34;

    //SPRITES DMG
    public Sprite DMG1;
    public Sprite DMG2;
    public Sprite DMG3;
    public Sprite DMG4;

    //DAÑO BOSS
    float currentDelay = 0f;
    bool colourChangeCollision = false;
    public float colourChangeDelay = 0.5f;

    void Start()
    {
        //DAÑO AL PLAYER
        Player = GameObject.Find("Player");
        playerHealth = Player.GetComponent<Player>();

        //BOSS
        Boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        //Boss.GetComponent<Boss>()<SpriteRenderer>().sprite = DMG4;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        currentDelay = Time.time + colourChangeDelay;

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

        if (col.gameObject.tag == "boss")
        {
            Destroy(gameObject);
            if (col.gameObject.GetComponent<Boss>().currentHealth > 0)
            {
                col.gameObject.GetComponent<Boss>().TakeDamage(3);
            }
        }
    }
}

