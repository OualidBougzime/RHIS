using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect
{
   

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Change the 'color' property of the 'Sprite Renderer'
              transform.position = transform.parent.position;
            sprite.color = new Color(0, 1, 0, 1);
    }
}
