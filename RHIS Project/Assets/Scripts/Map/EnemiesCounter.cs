using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCounter
{
    private int nbrEnemies;
    private List<DoorManagment> doors;
    public EnemiesCounter(int nbrEnemies, List<DoorManagment> doors)
    {
        this.nbrEnemies = nbrEnemies;
        this.doors = doors;
        OpenDoors(true);
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
