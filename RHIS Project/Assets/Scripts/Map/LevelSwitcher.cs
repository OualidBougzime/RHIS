using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    private LvlManager lvl;

    private void Awake()
    {

        lvl = gameObject.AddComponent<LvlManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        lvl.LoadNextLvl();
    }

}
