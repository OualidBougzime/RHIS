using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public PlayerController Player;

    public Text text;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Player = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Munitions: "+Player.myWeapon.GetComponent<Weapon>().AmmoInMagazine + "/" + Player.myWeapon.GetComponent<Weapon>().TotalAmmo;
    }
}
