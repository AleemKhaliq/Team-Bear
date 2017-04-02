using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public int enemyAmount;
    public int enemyCounter;
    private float xMin;
    private float xMax;
    private float yLevel;
    public float spawnDelay;
    private float countdown;
    private bool inArea;

    private BoxCollider2D BoxCollider2D;    
    public GameObject enemy;

	// Use this for initialization
	void Start ()
    {
        countdown = spawnDelay;
        BoxCollider2D = GetComponent<BoxCollider2D>();        
        xMin = transform.position.x - (BoxCollider2D.size.x/2);
        xMax = transform.position.x + (BoxCollider2D.size.x/2);
        yLevel = -2.55f;
        inArea = false;
        enemyCounter = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(enemyCounter < enemyAmount)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0 && inArea)
            {
                Spawn();
                enemyCounter++;
                countdown = spawnDelay;
                Debug.Log("Spawn");
            }                        
        }        
    }

    void Spawn()
    {
        Vector2 randomPos = new Vector2(Random.Range(xMin, xMax), yLevel);

        GameObject drone = enemy;

        Instantiate(drone, randomPos, transform.rotation);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inArea = true;
            Debug.Log("In Spawn Area");
        }
    }
}