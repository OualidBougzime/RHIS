using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCounter
{
    private int nbrEnemies;
    private List<DoorManagment> doors;
    private List<IaController> ennemies;
    public EnemiesCounter(int nbrEnemies, List<DoorManagment> doors, List<IaController> enemies)
    {
        this.nbrEnemies = nbrEnemies;
        this.doors = doors;
        this.ennemies = enemies;
        OpenDoors(true);
        DisableEnemies();
    }

    private void DisableEnemies()
    {
        foreach (IaController ia in ennemies)
        {
            ia.Disable();
        }
    }

    public void EnableEnemies()
    {
        foreach (IaController ia in ennemies)
        {
            ia.Enable();
        }
    }

    public void KillEnemy()
    {
        nbrEnemies--;
        OpenDoors();
    }

    private void OpenDoors()
    {
        OpenDoors(false);
    }

    private void OpenDoors(bool force)
    {
        if (nbrEnemies > 0 && !force)
        {
            return;
        }
        foreach (DoorManagment door in doors)
        {
            door.OpenDoor();
        }
    }

    public void CloseDoors()
    {
        if (nbrEnemies <= 0)
        {
            return;
        }
        foreach (DoorManagment door in doors)
        {
            door.CloseDoor();
        }
    }
}
