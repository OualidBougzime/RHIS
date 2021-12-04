	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Weapon PlayerWeapon;
	public GameObject Projectile;
	public int Force;
    [SerializeField] private int vitesse = 1;
    private static int direction;
    private static char key;
    private int rotation;
    Vector3 rotationVector;
	public Animator anim;
	[SerializeField] int dashPossibility = 1000;

<<<<<<< HEAD

	private Transform poisonCircle;
    private Rigidbody myRigidbody;
    private Vector3 speed;

=======
>>>>>>> 77f40a4 (Fixing Reload exception)
	//Adding DontDestroyOnLoad
	

    // Start is called before the first frame update
    void Start()
    {
    	rotationVector = transform.rotation.eulerAngles;
        myRigidbody = GetComponent<Rigidbody>();
		poisonCircle = transform.GetChild(1).GetComponentInChildren<Transform>();
    }

	Vector3 cartesianToIsometric(Vector3 cartesian){
		var isometric = new Vector3();
		isometric.x = cartesian.x - cartesian.y;
		isometric.y = (cartesian.x + cartesian.y) / 2;
		return isometric;

	}

    void goLeft(){
    	key = 'Q';
		anim.SetTrigger("run");
    	direction = -1;
		rotation = -180;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
		poisonCircle.rotation = Quaternion.identity;
		//transform.position = transform.position + new Vector3(vitesse * direction * Time.deltaTime/20, 0, 0); 
		speed += new Vector3(direction * vitesse, 0);
    }

    void goRight(){
    	key = 'D';
		anim.SetTrigger("run");
    	direction = 1;
		rotation = 0;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
		poisonCircle.rotation = Quaternion.identity;
		//transform.position = transform.position + new Vector3(vitesse * direction * Time.deltaTime/20, 0, 0); 
		speed += new Vector3(direction * vitesse, 0);
    }

    void goUp(){
    	key = 'Z';
		anim.SetTrigger("run");
        direction = 1;
		rotation = -180;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
		poisonCircle.rotation = Quaternion.identity;
		//transform.position = transform.position + new Vector3(0, vitesse * direction * Time.deltaTime/20, 0);
		speed += new Vector3(0, direction * vitesse);
    }

    void goDown(){
    	key = 'S';
		anim.SetTrigger("run");
        direction = -1;
		rotation = 0;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
		poisonCircle.rotation = Quaternion.identity;
        //transform.position = transform.position + new Vector3(0, vitesse * direction * Time.deltaTime/20, 0);
        speed += new Vector3(0, direction * vitesse);	
    }

    void dash(){
		if(dashPossibility == 1000){
			anim.SetTrigger("dash");
			var vitesseDash = 200;
			if(key == 'Q'){
				direction = -5;
				transform.position = transform.position + cartesianToIsometric(new Vector3(vitesseDash * direction * Time.deltaTime/20, 0, 0));
			}

			if(key == 'D'){
				direction = 5;
				transform.position = transform.position + cartesianToIsometric(new Vector3(vitesseDash * direction * Time.deltaTime/20, 0, 0));
			}

			if(key == 'Z'){
				direction = 5;
				transform.position = transform.position + cartesianToIsometric(new Vector3(0, vitesseDash * direction * Time.deltaTime/20, 0));
			}

			if(key == 'S'){
				direction = -5;
				transform.position = transform.position + cartesianToIsometric(new Vector3(0, vitesseDash * direction * Time.deltaTime/20, 0));
			}
			dashPossibility = 0;
		}
    	
    }

	void idle() 
	{
		anim.SetTrigger("idle");	
	}

	void fire()
	{
		if (PlayerWeapon != null && PlayerWeapon.AmmoInMagazine != 0)
		{
			anim.SetTrigger("fire");
			PlayerWeapon.AmmoInMagazine--;

			//GetComponent<AudioSource>().PlayOneShot(SoundTir);
			GameObject bullet = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
			bullet.transform.position = bullet.transform.position + new Vector3(1,1, 1);
			//Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right) * Force;
			//transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * Force, Space.World);
			Destroy(bullet, 2f);
		}
		
	}

	void reload()
	{
		if (PlayerWeapon != null)
		{
			PlayerWeapon.ReloadWeapon();
		}
	}


    // Update is called once per frame
    void Update()
    {
		if(dashPossibility<1000)
			dashPossibility+=1;
        speed = new Vector3(0, 0);
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
			if(Input.GetKey(KeyCode.R)){
				reload();
			}
			if(Input.GetButtonDown("Fire1")){
				fire();
			}
		}else
		{
			idle();
		}
        myRigidbody.velocity = cartesianToIsometric(speed/10);
    }
}
