using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    protected SpriteRenderer sprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
