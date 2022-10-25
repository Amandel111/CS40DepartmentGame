using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GroundFloor : MonoBehaviour
{
    // Start is called before the first frame update

    LevelOneController eventTracker;
    Transform playerTransform;
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        Debug.Log(LevelOneController.previousScene);
        switch (LevelOneController.previousScene)
        {
            case "CS111 Classroom":
                playerTransform.position = new Vector3(-28.4f, -18f, 0);
                break;
            case "Level One":
                playerTransform.position = new Vector3(-25.1f, 0.8f, 0);
                break;
        }

        LevelOneController.previousScene = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
