using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInformation : MonoBehaviour {

    public static void SaveAllInformation()
    {
        //AMMO
        PlayerPrefs.SetInt("Ammmo", Player.counterAmmo);

        //ARMAS
        PlayerPrefs.SetInt("Rifle", Player.rifle);
        PlayerPrefs.SetInt("Scopeta", Player.scopeta);
    }
}
