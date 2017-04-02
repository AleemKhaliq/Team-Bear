using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TunnelHome : MonoBehaviour
{
    public int levelNo;
    private int playerLevel;    
    private TunnelMessage tunnelMsg;    
        
	// Use this for initialization
	void Start ()
    {
        tunnelMsg = GameObject.FindGameObjectWithTag("T_Message").GetComponent<TunnelMessage>();
        playerLevel = PlayerPrefs.GetInt("levelReached", 1);        
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

            if (playerLevel <= levelNo)
            {
                PlayerPrefs.SetInt("levelReached", (levelNo + 1));
            }

            Debug.Log("Level complete");
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("Map");
            }

        }        
    }
     
}
