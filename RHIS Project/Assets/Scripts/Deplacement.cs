using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
    	rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKey(KeyCode.Q)){
    		rigidbody2D.velocity = new Vector2(-2, rigidbody2D.velocity.y);
    	}
    	if(Input.GetKey(KeyCode.D)){
    		rigidbody2D.velocity = new Vector2(2, rigidbody2D.velocity.y);
    	}

    	if(Input.GetKey(KeyCode.Z)){
    		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 4);
    	}
    	if(Input.GetKey(KeyCode.S)){
    		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -2);
    	}
    	if(Input.GetKey(KeyCode.Space)){
    		rigidbody2D.velocity = new Vector2(5, rigidbody2D.velocity.y);
    	}
        
    }
}
