using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TunnelHome : MonoBehaviour
{
    private Player player;
    private TunnelMessage tunnelMsg;
    private Vector2 tunnel;
        
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        tunnelMsg = GameObject.FindGameObjectWithTag("T_Message").GetComponent<TunnelMessage>();
        tunnel = gameObject.transform.position;
    }

    /// <summary>
    /// Checks to see if the player has entered over the area of the tunnel
    /// </summary>
    /// <param name="area"></param>
    void OnTriggerStay2D(Collider2D area)
    {
        if (area.CompareTag("Player"))
        {
            Debug.Log("Area crossed");
            // Sets Display boolean to true so that the tunnel message prompt is displayed
            tunnelMsg.Display = true;

            if(Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        //else
        //{
        //    tunnelMsg.Display = false;
        //}
    }
     
}
