using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagment : MonoBehaviour
{
    [SerializeField] private EnemiesCounter counter;
    private SpriteRenderer mySpriteRenderer;
    private CapsuleCollider myCapsuleCollider;

    private void Awake()
    {
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myCapsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }


    public void SetCounter(EnemiesCounter counter)
    {
        this.counter = counter;
    }

    public void OpenDoor()
    {
        mySpriteRenderer.enabled = false;
        myCapsuleCollider.isTrigger = true;
    }

    public void CloseDoor()
    {
        mySpriteRenderer.enabled = true;
        myCapsuleCollider.isTrigger = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            counter.CloseDoors();
        }
    }
}
