using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
	public GameObject Projectile;
	public int Force;
	//public AudioClip SoundTir;
    // Start is called before the first frame update
    void Start()
    {
    	
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetButtonDown("Fire1")){
    		//GetComponent<AudioSource>().PlayOneShot(SoundTir);
    		GameObject Bullet = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
    		Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right) * Force;
    		Destroy(Bullet, 2f);
    	}
        
    }
}
