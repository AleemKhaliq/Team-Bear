using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAreaRight : MonoBehaviour
{
    public bool inView;
    private Player player;
    private RangeCheck rangeCheck;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rangeCheck = GameObject.FindGameObjectWithTag("Vision").GetComponent<RangeCheck>();
        inView = false;
    }

    void Update()
    {
        rangeCheck.IsRight = inView;
    }

    /// <summary>
    /// Checks to see if the player enters the Drone's field of view
    /// </summary>
    /// <param name="area"></param>
    void OnTriggerEnter2D(Collider2D area)
    {
        if (area.CompareTag("Player"))
        {
            Debug.Log("Area crossed");
            inView = true;
        }
    }
}
