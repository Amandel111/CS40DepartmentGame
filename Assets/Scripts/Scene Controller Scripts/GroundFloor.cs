using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls Ground Floor Scene spawnpoints
/// </summary>
public class GroundFloor : MonoBehaviour
{
    //declare variables
    SceneController eventTracker;
    Transform playerTransform;

    void Start()
    {
        //initialize variables
        eventTracker = FindObjectOfType<SceneController>();
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();

        //Set spawnpoint according depending on what scene player is coming from
        switch (SceneController.previousScene)
        {
            case "CS111 Classroom":
                playerTransform.position = new Vector3(-28.4f, -18f, 0);
                break;
            case "Level One":
                playerTransform.position = new Vector3(-22f, -2.3f, 0);
                break;
        }

        //set previous scene
        SceneController.previousScene = SceneManager.GetActiveScene().name;

    }
}
