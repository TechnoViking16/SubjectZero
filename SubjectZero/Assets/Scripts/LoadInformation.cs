using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public void LoadAllInformation()
    {
        //AMMO
        Player.counterAmmo = PlayerPrefs.GetInt("Ammo");

        //ARMAS
        Player.rifle = PlayerPrefs.GetInt("Rifle");
        Player.rifle = PlayerPrefs.GetInt("Scopeta");
    }
}
