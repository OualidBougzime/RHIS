using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Effect
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        sprite.color = new Color(1, 0, 0, 1);
    }
}
