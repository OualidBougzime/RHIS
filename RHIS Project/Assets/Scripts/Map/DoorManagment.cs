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
        if (mySpriteRenderer == null || myCapsuleCollider == null) return;
        mySpriteRenderer.enabled = false;
        myCapsuleCollider.isTrigger = true;
    }

    public void CloseDoor()
    {
        if (mySpriteRenderer == null || myCapsuleCollider == null) return;
        mySpriteRenderer.enabled = true;
        myCapsuleCollider.isTrigger = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (counter == null)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            counter.CloseDoors();
            counter.EnableEnemies();
        }
    }
}
