using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaController : MonoBehaviour
{
    public float Vitesse = 50;  //Vitesse de déplacement
    public Vector2 Offset ;     //Vecteur qui nous permet de controller le déplacement
    public Animator anim;
   
    private int direction = 1;  //Pas d'incrément de la direction
    private int rotation;       //Position de l'IA lors du déplacement
    Vector3 rotationVector;
    [SerializeField] float minimumDistanceToAttack;



     private void Start()
    {
        Offset = new Vector2(transform.position.x - 10 , transform.position.x + 10); // initialisation du champ d'allez retour en fonction de la position initiale de l'IA
        rotationVector = transform.rotation.eulerAngles;

    }
	


    //aller à gauche
    private void goLeft () 
    {
        rotation = -180;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
        anim.SetTrigger("walk");
        direction = -1;
        
    }

    // aller à droite
     private void goRight () 
    {
        rotation = 0;
        rotationVector.y = rotation;
        transform.rotation = Quaternion.Euler(rotationVector);
        anim.SetTrigger("walk");
        direction = 1;
       
    }

    private bool isNearPlayer(string tag, float minimumDistance)
    {
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);
    
        for (int i = 0; i < goWithTag.Length; ++i)
        {
            if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
                return true;
        }
    
        return false;
    }

     private bool isFarFromPlayer(string tag, float minimumDistance)
    {
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);
    
        for (int i = 0; i < goWithTag.Length; ++i)
        {
            if (Vector3.Distance(transform.position, goWithTag[i].transform.position) >= minimumDistance/2)
                return true;
        }
    
        return false;
    }


    private void attack(string tag)
    {
        anim.SetTrigger("attack");
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);

    }


    private void moveToAttack()
    {       
        anim.SetTrigger("run");
        var wayPoint = GameObject.Find("Rhis");
        var wayPointPos = new Vector3(wayPoint.transform.position.x, wayPoint.transform.position.y, wayPoint.transform.position.z);
        if(wayPoint.transform.position.x < transform.position.x)
        {        
            rotation = -180;
            rotationVector.y = rotation;
            transform.rotation = Quaternion.Euler(rotationVector);
        }else
        {
            rotation = 0;
            rotationVector.y = rotation;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPointPos, 5 * Time.deltaTime/10); 
    }


	void Update () 
    {
        if(isNearPlayer("Player",20) == true)
        {
            if(isFarFromPlayer("Player",minimumDistanceToAttack) == true)
            {
                moveToAttack();
            }else
            {
                attack("Player");
            }
        }
        else
        { 
            if (transform.position.x > Offset.y)
                goLeft();
            else if(transform.position.x < Offset.x)
                goRight();

            transform.position = transform.position + new Vector3(Vitesse * direction * Time.deltaTime/200, 0, 0); 
        }
        
    }
}
