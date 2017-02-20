using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMessage : MonoBehaviour {

    public SpriteRenderer render;

    public bool Display { get; set; }

    // Use this for initialization
    void Start ()
    {
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
        Display = false;
    }

    // Update is called once per frame
    void Update ()
    {
        //Displays the sprite when the Display is set to true
        render.enabled = Display;
	}
}
