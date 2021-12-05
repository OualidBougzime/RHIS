using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{
    private int nbrLvl = 3;

    internal void LoadNextLvl()
    {
        int lvl = SceneManager.GetActiveScene().buildIndex;

        if (lvl >= nbrLvl-1)
        {
            print("Fin du Jeu. Bravo"); //TODO: la fin
            return;
        }
        SceneManager.LoadScene(lvl + 1);
    }
}
