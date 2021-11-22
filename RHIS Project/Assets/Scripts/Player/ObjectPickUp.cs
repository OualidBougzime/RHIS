using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    public ItemObject objectToPick;

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            // Pick up object
            //target.GetComponent<Player>().Inventory.add(objectToPick);
        }
    }
}
