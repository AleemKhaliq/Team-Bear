using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelTrigger : MonoBehaviour
{
    public LevelMarker marker;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if ((Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0))
            {
                marker.isDone = true;
                Debug.Log("Level complete");
                SceneManager.LoadScene("Map");
            }
        }
    }
}