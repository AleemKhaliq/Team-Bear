using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public bool faceRight;
    public bool faceLeft;
    public bool attacking;
    public bool inRange;
    public bool attackRange;
    public float speed;
    public float reverseSpeed;
    public float frontWakeRange;
    public float rearWakeRange;
    public float delayAttack;
    private float delayTimer;
    private float distance;
    private float yDistance;
    private float attackTimer = 0.2f;
    private float direction = 0;
    //public bool InRange { get; set; }
    
    private Player player;
    private Rigidbody2D myRigidBody;
    private SpriteRenderer SpriteRenderer;
    private Animator animate;    
    public Collider2D AttackTrigger;
    
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();        
        myRigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();

        faceLeft = true;
        faceRight = false;        
        attacking = false;
        inRange = false;
        attacking = false;
        AttackTrigger.enabled = false;
        attackRange = false;
        delayTimer = delayAttack;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Behave();
        CheckPosition();

        animate.SetBool("Attack", attacking);
        CheckRange();
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

    //Combat and Behaviour logic
    /// <summary>
    /// Makes the drone do one damage to the player if it is in the attack range
    /// </summary>
    void Swing()
    {
        if (attackRange)
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
            //gameObject.GetComponent<Animation>().Play("Drone_Swing");
        }
    }

    /// <summary>
    /// Defines when the drone should chase, attack, or run from the player
    /// </summary>
    void Behave()
    {
        CheckRange();
        if (inRange)
        {
            Chase();
            while(delayTimer >= 0)
            {
                delayTimer -= Time.deltaTime;
                if(delayTimer == 0)
                {
                    Swing();
                    delayTimer = delayAttack;
                    Debug.Log("attack");
                }
            }            
        }
    }

    /// <summary>
    /// Checks how far the player is from the drone on the x and y axis to see if it is in chase/attack range
    /// </summary>
    void CheckRange()
    {
        distance = transform.position.x - player.transform.position.x;
        yDistance = transform.position.y - player.transform.position.y;

        if (faceRight)
        {
            if(distance < 0 && distance < frontWakeRange || distance > 0 && distance < rearWakeRange)
            {
                inRange = true;
                if(distance >= -1 && Math.Abs(yDistance) < 0.2f)
                {
                    attackRange = true;
                    Debug.Log("attack");
                }
                else
                {
                    attackRange = false;
                }
            }
            else
            {
                inRange = false;
                attackRange = false;
            }
        }
        if (faceLeft)
        {
            if(distance > 0 && distance < frontWakeRange || distance < 0 && distance < rearWakeRange)
            {
                inRange = true;
                if (distance <= 1 && Math.Abs(yDistance) < 0.2f)
                {
                    attackRange = true;                    
                }
                else
                {
                    attackRange = false;
                }
            }
            else
            {
                inRange = false;
                attackRange = false;
            }
        }
    }
}
