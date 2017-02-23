using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed = 35f;
    public float maxSpeed = 5;
    public float jumpStrength = 150f;
    public bool grounded;
    public bool crouched;
    public bool roll;
    public bool canRoll;
    private float rollTime = 0.25f;  //Time spent rolling
    private float rollCool = 0.5f;  //Roll cooldown
    public bool dash;
    public bool canDash;
    private float dashTime = 0.25f;  //Time spent dashing
    public float timer;
    
    public bool noMove; //Prevent movement
    public int faceDirection;   //Direction Player is facing

    public bool canFloat;
    public bool attacking;
    public bool doSwing;
    private float attackCool = 0.25f; //Cooldown between swings
    private float attackTimer;
    public bool canUppercut;
    public bool uppercut;
    public bool slam;
    public bool charged;    //Whether or not attck has been held long enough for charge moves
    public float heldTime;  //Ammount of time attack has been held

    public int maxHealth = 5;
    public int curHealth;

    private Rigidbody2D getBody;
    public Collider2D AttackTrigger;
    public Collider2D CrouchAttackTrigger;
    public Collider2D UppercutAttackTrigger;
    public Collider2D SlamAttackTrigger;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        getBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;
        canUppercut = true;
        attacking = false;
        AttackTrigger.enabled = false;
        CrouchAttackTrigger.enabled = false;
        UppercutAttackTrigger.enabled = false;
        SlamAttackTrigger.enabled = false;
        roll = false;
        canRoll = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(getBody.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsCrouched", crouched);
        anim.SetBool("Roll", roll);
        anim.SetBool("Dash", dash);
        anim.SetBool("Attack", attacking);
        anim.SetBool("Uppercut", uppercut);
        anim.SetBool("Slam", slam);

        //Rotation
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            faceDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            faceDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            getBody.AddForce(Vector2.up * jumpStrength);
        }

        //Check if charged
        if (Input.GetButtonDown("Fire1"))
        {
            heldTime = Time.time;
        }
        if (Input.GetButton("Fire1"))
        {
            noMove = true;
            if (Time.time - heldTime > 1f)
            {
                charged = true;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            noMove = false;
            heldTime = 0;
            charged = false;
        }

        //Swing
        if (Input.GetButtonUp("Fire1") && !charged && !uppercut && !attacking && !slam)
        {
            attacking = true;
            if(!crouched)
            {
                AttackTrigger.enabled = true;
                anim.SetTrigger("Swing");
            }
            else if (crouched)
            {
                CrouchAttackTrigger.enabled = true;
            }
            attackTimer = attackCool;
        }
        if (Input.GetButtonUp("Fire1") && charged && !uppercut && !attacking && !slam)
        {
            attacking = true;
            if (!crouched)
            {
                doSwing = true;
                AttackTrigger.enabled = true;
            }
            else if (crouched)
            {
                CrouchAttackTrigger.enabled = true;
            }
            attackTimer = attackCool;
        }
        /*if (Input.GetButtonUp("Fire1") && !charged && !grounded && !uppercut && !attacking)
        {
            StartCoroutine(airFloat());
        }*/
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
                CrouchAttackTrigger.enabled = false;
            }
        }
        /*if (Input.GetButtonUp("Fire1") && !charged && uppercut && !attacking)
        {
            attacking = true;
            attackTimer = attackCool;
        }
        if (Input.GetButtonUp("Fire1") && charged && uppercut && !attacking)
        {
            if (!crouched)
            {
                AttackTrigger.enabled = true;
            }
            else if (crouched)
            {
                CrouchAttackTrigger.enabled = true;
            }
            attacking = true;
            attackTimer = attackCool;
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
                    CrouchAttackTrigger.enabled = false;
                }
            }
        }*/

        //Uppercut
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetButtonDown("Fire1") && canUppercut)
        {
            canUppercut = true;
            StartCoroutine(useUppercut());
            if (!grounded)
            {
                canUppercut = false;
            }
        }

        if (grounded)
        {
            canUppercut = true;
        }

        //Slam
        if (Input.GetAxisRaw("Vertical") < 0 && Input.GetButtonDown("Fire1") && !uppercut && !grounded)
        {
            slam = true;
            SlamAttackTrigger.enabled = true;
        }

        if (slam && !grounded)
        {
            getBody.velocity = new Vector2(0, -15f);
        }
        if (slam && grounded)
        {
            slam = false;
            SlamAttackTrigger.enabled = false;
        }
        //Dash
        /*if (Input.GetKey("left shift"))
        {
            dash = true;
        }
        if (!Input.GetKey("left shift"))
        {
            dash = false;
        }
        if (dash && Input.GetAxisRaw("Horizontal") != 0)
        {
            getBody.AddForce((Vector2.right * 150f) * Input.GetAxis("Horizontal"));
        }*/

        //Roll
        if (Input.GetKeyDown("left shift") && grounded && !roll && canRoll)
        {
            roll = true;
            timer = rollTime;
            noMove = true;
        }

        if (roll)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                //getBody.AddForce((Vector2.right * 150) * faceDirection);
                getBody.velocity = new Vector2(12.5f * faceDirection, 0);
            }
            else
            {
                roll = false;
                canRoll = false;
                noMove = false;
                timer = rollCool;
            }
        }

        if (!canRoll)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                canRoll = true;
            }
        }

        //Air Dash
        //Only gets one charge per jump
        if (Input.GetKeyDown("left shift") && !grounded && !dash && canDash)
        {
            dash = true;
            timer = dashTime;
            noMove = true;
        }
        if (dash)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                getBody.velocity = new Vector2(12.5f * faceDirection, 0);
            }
            else
            {
                dash = false;
                canDash = false;
                noMove = false;
            }
        }
        if (grounded)
        {
            canDash = true;
        }


        //Crouch
        if (Input.GetAxisRaw("Vertical") < 0 && grounded && Input.GetAxis("Horizontal") == 0)
        {
            crouched = true;
        }
        else
        {
            crouched = false;
        }

        //Health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator airFloat()
    {
        bool doFloat = true;
        while (doFloat)
        {
            getBody.velocity = (new Vector2(0, 0));
            yield return new WaitForSeconds(1f);
            doFloat = false;
        }
    }

    IEnumerator useUppercut()
    {
        Vector2 temp = getBody.velocity;
        noMove = true;
        uppercut = true;
        UppercutAttackTrigger.enabled = true;
        getBody.velocity = new Vector2(0f, 15f);
        yield return new WaitForSeconds(0.25f);
        getBody.velocity = new Vector2(0f, 0f);
        noMove = false;
        uppercut = false;
        UppercutAttackTrigger.enabled = false;
    }

    void FixedUpdate()
    {
        Vector3 antiVelocity = getBody.velocity;
        antiVelocity.x *= 0.9f;
        antiVelocity.y = getBody.velocity.y;
        antiVelocity.z = 0.0f;

        //Friction
        if (grounded)
        {
            getBody.velocity = antiVelocity;
        }

        //Walking
        float input = Input.GetAxisRaw("Horizontal");

        if (!noMove)
        {
            getBody.AddForce((Vector2.right * speed) * input);
        }

        //Limiting Speed
        if (getBody.velocity.x > maxSpeed && !noMove)
        {
            getBody.velocity = new Vector2(maxSpeed, getBody.velocity.y);
        }

        if (getBody.velocity.x < -maxSpeed && !noMove)
        {
            getBody.velocity = new Vector2(-maxSpeed, getBody.velocity.y);
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Red_Flash");
    }

    public IEnumerator Knockback(float knockDur, float knockDir, float power)
    {
        noMove = true;
        if (knockDir == -1)
        {
            faceDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (knockDir == 1)
        {
            faceDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        getBody.velocity = new Vector2(knockDir * power, power);
        yield return new WaitForSeconds(0.25f);
        getBody.velocity = new Vector2(0f, 0f);
        noMove = false;
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
