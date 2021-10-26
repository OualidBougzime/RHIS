using UnityEngine;

public class Deplacement : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;

    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(Input.GetKey(KeyCode.Q)){
            rigidbody2D.velocity = new Vector2(-1, rigidbody2D.velocity.y);
        }

        if(Input.GetKey(KeyCode.D)){
            rigidbody2D.velocity = new Vector2(1, rigidbody2D.velocity.y);
        }

        if(Input.GetKey(KeyCode.Z)){
            rigidbody2D.velocity = new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y, 1);
        }

        if(Input.GetKey(KeyCode.S)){
            rigidbody2D.velocity = new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y, -1);
        }
    }
}