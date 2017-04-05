using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandTrigger : MonoBehaviour {

    public BossHand bossHand;

    private Player player;
    private Vector2 spike;

    // Use this for initialization
    void Start()
    {
        bossHand = gameObject.GetComponentInParent<BossHand>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spike = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            bossHand.hitObject = true;

        }
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
            StartCoroutine(player.Knockback(0.02f, direction, 5f));
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            bossHand.hitObject = true;

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            bossHand.hitObject = false;

        }
    }
}
