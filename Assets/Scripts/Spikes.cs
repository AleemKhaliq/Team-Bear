using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private Player player;
    private Vector2 hitObject;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hitObject = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.Damage(1);
            Debug.Log("Hit");
            int direction;
            if (player.transform.position.x > hitObject.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            StartCoroutine(player.Knockback(0.02f, direction, 5f));
        }
    }
}
