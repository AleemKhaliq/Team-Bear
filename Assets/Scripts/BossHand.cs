using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    public bool hitObject;
    public bool active;

    private Rigidbody2D getBody;

    // Use this for initialization
    void Start()
    {
        //getBody = gameObject.GetComponent<Rigidbody2D>();
        active = false;
    }
	
	// Update is called once per frame
	void Update()
    {
        //StartCoroutine(Slam());

    }

    IEnumerator Slam()
    {
        active = true;
        if(!hitObject)
        {
            transform.position = new Vector2 (transform.position.x, transform.position.y - 1);
        }
        yield return new WaitForSeconds(0.25f);
        active = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
