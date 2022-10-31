using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GroundFloor : MonoBehaviour
{
    // Start is called before the first frame update

    SceneController eventTracker;
    Transform playerTransform;
    void Start()
    {
        eventTracker = FindObjectOfType<SceneController>();
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        Debug.Log(SceneController.previousScene);
        switch (SceneController.previousScene)
        {
            case "CS111 Classroom":
                playerTransform.position = new Vector3(-28.4f, -18f, 0);
                break;
            case "Level One":
                playerTransform.position = new Vector3(-22f, -2.3f, 0);
                break;
        }

        SceneController.previousScene = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
