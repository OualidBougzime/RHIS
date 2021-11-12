using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour {
    //Gestion de l'input vertical de la souris
    public float speed = 5.0f;

	
	// Update is called once per frame
	void Update () {
        float ver= Input.GetAxis("Mouse Y") * speed;
        transform.Rotate(-ver, 0, 0);
	}
}
