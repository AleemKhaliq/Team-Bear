using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public int curHealth;
    public int maxHealth;

    public bool awake;
    public bool lookRight;
    public float distance;
    public float wakeRange;
    public float shotInterval;
    public float shotSpeed;
    public float shotTimer;

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootLeft;
    public Transform shootRight;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookRight", lookRight);
        RangeCheck();
        if (target.transform.position.x > transform.position.x && awake)
        {
            lookRight = true;
        }
        if (target.transform.position.x < transform.position.x && awake)
        {
            lookRight = false;
        }
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void RangeCheck()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < wakeRange)
        {
            awake = true;
        }
        if (distance > wakeRange)
        {
            awake = false;
            lookRight = false;
        }
    }

    public void Attack(bool attackRight)
    {
        shotTimer += Time.deltaTime;
        if (shotTimer >= shotInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            if (attackRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootRight.transform.position, shootRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * shotSpeed;
                shotTimer = 0;
            }
            if (!attackRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootLeft.transform.position, shootLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * shotSpeed;
                shotTimer = 0;
            }
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Red_Flash");
    }
}
