using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaController : MonoBehaviour
{
    public float Vitesse = 50;  //Vitesse de déplacement
    public Vector2 Offset ;     //Vecteur qui nous permet de controller le déplacement
   
    private int direction = 1;  //Pas d'incrément de la direction
    private int rotation;       //Position de l'IA lors du déplacement
	
    //aller à gauche
    private void goLeft () 
    {
        rotation = -140;
        direction = -1;
    }

    // aller à droite
     private void goRight () 
    {
        rotation = 0;
        direction = 1;
    }

    private void Start()
    {
        Offset = new Vector2(transform.position.x - 1 , transform.position.x + 1); // initialisation du champ d'allez retour en fonction de la position initiale de l'IA
    }


	void Update () {
        if (transform.position.x > Offset.y)
            goLeft();
        else if(transform.position.x < Offset.x)
            goRight();

        transform.position = transform.position + new Vector3(Vitesse * direction * Time.deltaTime/20, 0, 0); 
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
}
}
