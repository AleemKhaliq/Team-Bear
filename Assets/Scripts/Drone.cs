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
    public float reverseSpeed;

    private Player player;
    private Rigidbody2D myRigidBody;
    private SpriteRenderer SpriteRenderer;
    public Animator animate;
    
    public bool Attacking { get; set; }

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myRigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        faceLeft = true;
        faceRight = false;
        health = maximumHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    // Movement - base movement controls
    /// <summary>
    /// Moves the enemy to the right and flips the sprite to face the right
    /// </summary>
    void MoveRight()
    {
        if (faceLeft)
        {
            SpriteRenderer.flipX = true;
            faceLeft = false;
        }
        faceRight = true;        
        myRigidBody.AddForce(new Vector2(speed, 0));
    }

    /// <summary>
    /// Moves the enemy to the left and flips the sprite to face the left
    /// </summary>
    void MoveLeft()
    {
        if (faceRight)
        {
            SpriteRenderer.flipX = false;
            faceRight = false;
        }
        faceLeft = true;
        myRigidBody.AddForce(new Vector2(-speed, 0));
    }

    // Reversing
    /// <summary>
    /// Moves the enemy to the right but flips it to face the left as it is a reversing move
    /// </summary>
    void BackRight()
    {
        if (faceRight)
        {
            SpriteRenderer.flipX = false;
            faceRight = false;
        }
        faceLeft = true;
        myRigidBody.AddForce(new Vector2(speed, 0));
    }

    /// <summary>
    /// Moves the enemy to the left but flips it to face the right as it is a reversing move
    /// </summary>
    void BackLeft()
    {
        if (faceLeft)
        {
            SpriteRenderer.flipX = true;
            faceLeft = false;
        }
        faceRight = true;
        myRigidBody.AddForce(new Vector2(-speed, 0));
    }

    /// <summary>
    /// Makes the enemy chase after the player imperfectly
    /// </summary>
    void Chase()
    {
        if(player.transform.position.x > transform.position.x)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    /// <summary>
    /// Makes the enemy run away from the player in reverse
    /// </summary>
    void Run()
    {
        if (player.transform.position.x > transform.position.x)
        {
            BackLeft();
        }
        else
        {
            BackRight();
        }
    }

    // Attack logic
    void Swing()
    {

    }

    // Combined Behaviour
    void Behave()
    {

    }
}
