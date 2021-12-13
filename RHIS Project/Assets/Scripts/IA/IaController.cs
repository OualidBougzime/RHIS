using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaController : MonoBehaviour
{
    public float Vitesse = 50;  //Vitesse de déplacement
    public Vector2 Offset ;     //Vecteur qui nous permet de controller le déplacement
    public Animator anim;
    private string target = "Player";
    private int direction = 1;  //Pas d'incrément de la direction
    private int rotation;       //Position de l'IA lors du déplacement
    Vector3 rotationVector;
    [SerializeField] float minimumDistanceToAttack;
    [SerializeField] bool idling;
    [SerializeField] int damage;

    [SerializeField] int attackPossibility = 1000;
    [SerializeField] int attackCooldown = 1000;

    private Renderer myRenderer;
    private Canvas myCanvas;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        myCanvas = GetComponentInChildren<Canvas>();
    }

    private void Start()
    {
        Offset = new Vector2(transform.position.x - 10 , transform.position.x + 10); // initialisation du champ d'allez retour en fonction de la position initiale de l'IA
        rotationVector = transform.rotation.eulerAngles;

        Disable();
    }
	void Update () 
    {
        if (attackPossibility < attackCooldown)
        {
            attackPossibility++;
        }

        if(idling == false)
        {
            if(isNearPlayer(target,7) == true)
            {
                if(isFarFromPlayer(target,minimumDistanceToAttack) == true)
                {
                    moveToAttack(target);
                }else
                {
                    if (attackPossibility >= attackCooldown)
                    {
                        attack(target);
                        attackPossibility = 0;
                    }
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
        }else
        {
            //anim.SetTrigger("idle");
        }
        
    }
	
    public void Disable()   //Met l'ennemi en pause jusqu'à l'arriver du joueur
    {
        if (myRenderer == null) return;
        idling = true;
        myRenderer.enabled = false;
        myCanvas.enabled = false;
    }

    public void Enable()   //Active l'ennemi
    {
        if (myRenderer == null) return;
        myRenderer.enabled = true;
        myCanvas.enabled = true;
        idling = false;
    }

    private IEnumerator myWaiter(float x)
    {
        //Wait for x seconds
        yield return new WaitForSeconds(x);
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
        goWithTag[0].GetComponent<PlayerStatus>().GetDamage(damage);
    }


    private void moveToAttack(string tag)
    {       
        anim.SetTrigger("run");
        var wayPoint = GameObject.FindGameObjectWithTag(tag).transform;
        var wayPointPos = new Vector3(wayPoint.position.x, wayPoint.position.y, wayPoint.position.z);
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

    public void SetTarget(string newTarget)
    {
        target = newTarget;
    }

}
