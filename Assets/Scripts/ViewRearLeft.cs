using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRearLeft : MonoBehaviour
{
    public bool InView { get; set; }
    private Player player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        InView = false;
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
            InView = true;
        }
    }
}
