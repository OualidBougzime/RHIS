using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    public Object object;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            // Pick up object
            target.GetComponent<Player>().objects.add(object);
        }
    }
}
