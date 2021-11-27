using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Player;
    private Vector3 offset = new Vector3(0, 0, -7);

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + offset;
    }
}
