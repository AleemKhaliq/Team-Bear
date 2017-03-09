using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour {

    public int maxHealth = 1;
    public int curHealth;
    private Rigidbody2D getBody;    //Object's RigidBody2D
    private Player player;
    private BoxCollider2D playerBody;
    private Vector2 getPos;         //Position of attatched object
    public Vector2 strng;           //Attack strength
    private int direction;

    // Use this for initialization
    void Start ()
    {
        curHealth = maxHealth;
        getBody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (player.transform.position.x > getBody.transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }

    public void KnockBack()
    {
        bool airFloat = false;

        if (player.crouched)
        {
            strng.Set(5, 5);
        }
        if (player.slam)
        {
            strng.Set(10, 0.5f);
        }
        if (player.uppercut)
        {
            strng.Set(0, 15);
            airFloat = true;
        }
        else
        {
            strng.Set(5, 2.5f);
        }

        strng.x *= direction;
        StartCoroutine(Knock(strng, airFloat));
    }

    IEnumerator Knock(Vector2 strng, bool airFloat)
    {
        bool doKnock = true;
        while (doKnock)
        {
            getBody.velocity = strng;
            yield return new WaitForSeconds(0.25f);
            if (airFloat)
            {
                getBody.velocity = new Vector2(0f, 0f);
                Debug.Log("Float");
            }
            doKnock = false;
        }
    }
}
