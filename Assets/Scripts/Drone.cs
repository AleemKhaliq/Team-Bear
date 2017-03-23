using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public bool faceRight;
    public bool faceLeft;
    public bool attacking;
    public float speed;
    public float reverseSpeed;
    private float attackTimer = 0.2f;
    private float direction = 0;


    private Player player;
    private Rigidbody2D myRigidBody;
    private SpriteRenderer SpriteRenderer;
    private Animator animate;
    private RangeCheck rangeCheck;
    public Collider2D AttackTrigger;
    
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rangeCheck = GameObject.FindGameObjectWithTag("Vision").GetComponent<RangeCheck>();
        myRigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        faceLeft = true;
        faceRight = false;        
        attacking = false;
        animate.SetBool("Attack", attacking);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Behave();
        Chase();

        CheckPosition();
        if (faceRight && player.transform.position.x < transform.position.x + (direction * 1) || faceLeft && player.transform.position.x > transform.position.x + (direction * 1))
        {
            Debug.Log("close");            
            attacking = true;
            AttackTrigger.enabled = true;

            if (attacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    AttackTrigger.enabled = false;
                }
            }
        }
	}

    /// <summary>
    /// Checks to see which side of the enemy the player is on
    /// </summary>
    void CheckPosition()
    {
        if (player.transform.position.x > transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
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
        if(direction == 1)
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
        if (direction == 1)
        {
            BackLeft();
        }
        else
        {
            BackRight();
        }
    }

    // Attack logic
    bool Swing()
    {
        return true;
    }

    // Combined Behaviour
    void Behave()
    {
        if (rangeCheck.inRange)
        {
            Chase();
        }
    }
}
