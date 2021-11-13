using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidbody;
    private int vitesse = 20;
    private int direction;
    private static char key;
    private int rotation;
    Vector3 rotationVector;
    // Start is called before the first frame update
    void Start()
    {
    	rigidbody = GetComponent<Rigidbody>();
    	rotationVector = transform.rotation.eulerAngles;
        
    }

    void goLeft(){
    	key = 'Q';
    	direction = -1;
    	rotation = -140;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
    	transform.position = transform.position + new Vector3(vitesse * direction * Time.deltaTime/20, 0, 0); 
    }

    void goRight(){
    	key = 'D';
    	direction = 1;
    	rotation = 0;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
    	transform.position = transform.position + new Vector3(vitesse * direction * Time.deltaTime/20, 0, 0); 
    }

    void goUp(){
    	key = 'Z';
        direction = 1;
    	transform.position = transform.position + new Vector3(0, vitesse * direction * Time.deltaTime/20, 0);     	
    }

    void goDown(){
    	key = 'S';
        direction = -1;
    	transform.position = transform.position + new Vector3(0, vitesse * direction * Time.deltaTime/20, 0);     	
    }

    void dash(){
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


    // Update is called once per frame
    void Update()
    {
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


        
    }
}
