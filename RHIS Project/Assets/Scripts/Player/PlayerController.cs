	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameObject myWeapon;
	private Weapon playerWeapon;
	public GameObject Projectile;
	public int Force;
    [SerializeField] private int vitesse = 1;
    private static int direction;
    private static char key=' ';
    private int rotation;
    Vector3 rotationVector;
	public Animator anim;
	[SerializeField] int dashPossibility = 1000;
	[SerializeField] int dashCooldown = 1000;

	[SerializeField] private int dashSpeed = 100;
	private Transform poisonCircle;
    private Rigidbody myRigidbody;
	private Transform myTransform;
    private Vector3 speed;

	private static PlayerController instance;


    private void Awake()
    {
		ManageSingleton();
        myRigidbody = GetComponent<Rigidbody>();
		myTransform = GetComponent<Transform>();
		poisonCircle = transform.GetChild(1).GetComponentInChildren<Transform>();
		playerWeapon = myWeapon.GetComponent<Weapon>();
    }
    void Start()
    {
    	rotationVector = transform.rotation.eulerAngles;
    }

	private void ManageSingleton()
    {
		if (instance != null)
        {
			gameObject.SetActive(false);
			Destroy(gameObject);
        }
		else
        {
			instance = this;
			DontDestroyOnLoad(gameObject);
        }
    }

	public GameObject GetGameObject()
    {
		return instance.gameObject;
    }

	Vector3 cartesianToIsometric(Vector3 cartesian){
		if (cartesian.x > 0 && cartesian.y > 0)
        {
			return new Vector3(cartesian.x, cartesian.x / 2);
        }
		else if (cartesian.x > 0 && cartesian.y < 0)
        {
			return new Vector3(-cartesian.y, cartesian.y / 2);
		}
		else if (cartesian.x < 0 && cartesian.y > 0)
		{ 
			return new Vector3 (-cartesian.y, cartesian.y/2);
		}
		else if (cartesian.x < 0 && cartesian.y < 0)
		{
			return new Vector3(cartesian.x, cartesian.x / 2);
		}
		/*var isometric = new Vector3();
		isometric.x = cartesian.x - cartesian.y;
		isometric.y = (cartesian.x + cartesian.y) / 2;
		return isometric;*/
		return cartesian;

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
		//rotation = -180;
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
		//rotation = 0;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
		poisonCircle.rotation = Quaternion.identity;
        //transform.position = transform.position + new Vector3(0, vitesse * direction * Time.deltaTime/20, 0);
        speed += new Vector3(0, direction * vitesse);	
    }

    void dash(){
		if(dashPossibility >= dashCooldown)
		{
			anim.SetTrigger("dash");
			/*var vitesseDash = 200;
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
			}*/
			speed *= dashSpeed;
			dashPossibility = 0;
		}
    	
    }

	void idle() 
	{
		anim.SetTrigger("idle");	
	}

	/*void fire()
	{
		if (PlayerWeapon != null && PlayerWeapon.AmmoInMagazine != 0)
		{
			anim.SetTrigger("fire");
			PlayerWeapon.AmmoInMagazine--;

			GameObject Bullet = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
			if(key==' '){
				Bullet.GetComponent<Rigidbody>().velocity = cartesianToIsometric(transform.TransformDirection(Vector3.right)) * Force;
			}
			if(key == 'Q'){
				Bullet.GetComponent<Rigidbody>().velocity = cartesianToIsometric(transform.TransformDirection(Vector3.right)) * Force;
			}

			if(key == 'D'){
				Bullet.GetComponent<Rigidbody>().velocity = cartesianToIsometric(transform.TransformDirection(Vector3.right)) * Force;
			}

			if(key == 'Z'){
				Bullet.GetComponent<Rigidbody>().velocity = cartesianToIsometric(transform.TransformDirection(Vector3.up)) * Force;
			}
			if(key == 'S'){
				Bullet.GetComponent<Rigidbody>().velocity = cartesianToIsometric(transform.TransformDirection(Vector3.down)) * Force;
			}
    		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1);

        	foreach (Collider collider in hitColliders){
            	if (!collider.gameObject.CompareTag("Player") && !collider.gameObject.CompareTag("Backdrops"))
            	{
                	print(collider);
                    collider.GetComponentInParent<EnemyStatus>().GetDamage(5);
            	}
       	 	}
    		Destroy(Bullet, 2f);
		//}
			//GetComponent<AudioSource>().PlayOneShot(SoundTir);
			GameObject bullet = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
			bullet.transform.position = bullet.transform.position + new Vector3(1,1, 1);
			//Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right) * Force;
			//transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * Force, Space.World);
			Destroy(bullet, 2f);

			// Check if bullet has touched a enemy
			if (PlayerWeapon.Effect == WeaponEffect.POISON)
			{

			} else if (PlayerWeapon.Effect == WeaponEffect.SUPER_POISON)
			{
				
			} else if (PlayerWeapon.Effect == WeaponEffect.FIRE)
			{
				
			} else if (PlayerWeapon.Effect == WeaponEffect.ICE)
			{
				
			} else if (PlayerWeapon.Effect == WeaponEffect.RAGE)
			{
				
			}
		}
		
	}*/

	public void StartFire()
    {
		if (playerWeapon != null)
        {
			playerWeapon.StartFire();
        }
    }

	public void StopFire()
    {
		if (playerWeapon != null)
		{
			StartCoroutine(playerWeapon.StopFire());
		}
	}

	void reload()
	{
		if (playerWeapon != null)
		{
			playerWeapon.ReloadWeapon();
		}
	}


    // Update is called once per frame
    void Update()
    {
		if(dashPossibility<dashCooldown)
			dashPossibility+=1;
        speed = new Vector3(0, 0);

		if (Input.GetButtonUp("Fire1"))
		{
			StopFire();
		}

		if (Input.anyKey){
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
				StartFire();
			}
		}else
		{
			idle();
		}
        myRigidbody.velocity = cartesianToIsometric(speed/10);
    }
}