using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject Projectile;
	public int Force;
    private Rigidbody rigidbody;
    private int vitesse = 20;
    private static int direction;
    private static char key;
    private int rotation;
    Vector3 rotationVector;
	public Animator anim;



    // Start is called before the first frame update
    void Start()
    {
    	rigidbody = GetComponent<Rigidbody>();
    	rotationVector = transform.rotation.eulerAngles;
        
    }

    void goLeft(){
    	key = 'Q';
		anim.SetTrigger("run");
    	direction = -1;
    	rotation = -140;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
    	transform.position = transform.position + new Vector3(vitesse * direction * Time.deltaTime/20, 0, 0); 
    }

    void goRight(){
    	key = 'D';
		anim.SetTrigger("run");
    	direction = 1;
    	rotation = 0;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
    	transform.position = transform.position + new Vector3(vitesse * direction * Time.deltaTime/20, 0, 0); 
    }

    void goUp(){
    	key = 'Z';
		anim.SetTrigger("run");
        direction = 1;
    	transform.position = transform.position + new Vector3(0, vitesse * direction * Time.deltaTime/20, 0);     	
    }

    void goDown(){
    	key = 'S';
		anim.SetTrigger("run");
        direction = -1;
    	transform.position = transform.position + new Vector3(0, vitesse * direction * Time.deltaTime/20, 0);     	
    }

    void dash(){
		anim.SetTrigger("dash");
    	var vitesseDash = 50;
    	if(key == 'Q'){
    		direction = -5;
    		transform.position = transform.position + new Vector3(vitesseDash * direction * Time.deltaTime/20, 0, 0);
    	}

    	if(key == 'D'){
    		direction = 5;
    		transform.position = transform.position + new Vector3(vitesseDash * direction * Time.deltaTime/20, 0, 0);
    	}

    	if(key == 'Z'){
    		direction = 5;
    		transform.position = transform.position + new Vector3(0, vitesseDash * direction * Time.deltaTime/20, 0);
    	}

    	if(key == 'S'){
    		direction = -5;
    		transform.position = transform.position + new Vector3(0, vitesseDash * direction * Time.deltaTime/20, 0);
    	}
    	
    }

	void idle() 
	{
		anim.SetTrigger("idle");	
	}

	void fire()
	{
		anim.SetTrigger("fire");
		//GetComponent<AudioSource>().PlayOneShot(SoundTir);
		GameObject bullet = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
		bullet.transform.position = bullet.transform.position + new Vector3(1,1, 1);
		//Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right) * Force;
		//transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * Force, Space.World);
		Destroy(bullet, 2f);
	}


    // Update is called once per frame
    void Update()
    {
    	if(Input.anyKey){
			if(Input.GetKey(KeyCode.Q)){
    		goLeft();
    		}
			if(Input.GetKey(KeyCode.D)){
				goRight();
			}

			if(Input.GetKey(KeyCode.Z)){
				goUp();
			}
			if(Input.GetKey(KeyCode.S)){
				goDown();
			}
			if(Input.GetKey(KeyCode.Space)){
				dash();
			}
			if(Input.GetButtonDown("Fire1")){
				fire();
    	}
		}else
		{
			idle();
		}
        
    }
}
