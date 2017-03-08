using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private int health;
    public int maximumHealth;

    public bool faceRight;
    public bool faceLeft;
    public float speed;

    private Player player;
    private Rigidbody2D myRigidBody;
    public Animator animate;    

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myRigidBody = GetComponent<Rigidbody2D>();
        faceLeft = true;
        faceRight = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveLeft();
	}

    // Movement 
    void MoveRight()
    {
        myRigidBody.AddForce(new Vector2(speed, 0));
    }

    void MoveLeft()
    {
        myRigidBody.AddForce(new Vector2(-speed, 0));
    }

    // Chase logic
    void Chase()
    {

    }

    // Recovery logic
    void Run()
    {

    }

    // Attack logic
    void Hit()
    {

    }
}
