using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBox : MonoBehaviour {

    private Player player;
    private Vector2 spike;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spike = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.Damage(1);
            Debug.Log("Hit");
            int direction;
            if (player.transform.position.x > spike.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }
    }
}
