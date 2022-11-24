using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Rigidbody player;
    private void Start()
    {
        player = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <-20f )
        {
            player.velocity = new Vector3(0,0, 0);
            transform.position = new Vector3(-6.45f, 5.28f, 1.09f);
        }
    }
}
