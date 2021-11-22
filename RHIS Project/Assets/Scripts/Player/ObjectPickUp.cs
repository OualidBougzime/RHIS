using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    public ItemObject objectToPick;
    public int quantity;

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            // Pick up object
            if (objectToPick.typeObject == ItemType.Ammo)
            {
                target.GetComponent<Weapon>().AddAmmo(quantity);
            }
            else
            {
                target.GetComponent<Inventory>().Add(objectToPick);
            }
            
        }
    }
}
