using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls Floor One Scene, setting the player spawn points and controlling in-scene NPC dialogue 
/// </summary>
public class HubbController : MonoBehaviour
{
    //declare variables
    SceneController eventTracker;
    Transform playerTransform;
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        
        //set player spawnpoint depending on what the previous scene was 
        switch (SceneController.previousScene)
        {
            case "CS231 Classroom":
                playerTransform.position = new Vector3(14.8f, -9.7f, 0);
                break;
            case "CS304 Classroom":
                playerTransform.position = new Vector3(4.7f, -15.2f, 0);
                break;
            case "CS240 Classroom":
                 playerTransform.position = new Vector3(-6.1f, -20.4f, 0);
                break;
        }

        //set previous scene
        SceneController.previousScene = SceneManager.GetActiveScene().name;

    }

    /// <summary>
    /// Sets dialogue branches depending on player's current progress in game
    /// </summary>
    public void SetNPCDialogue()
    {
        SceneController eventsTracker = FindObjectOfType<SceneController>();
        if (eventsTracker.cs231Finished)
        {
            ConversationManager.Instance.SetBool("cs231Finished", true);
        }
        if (eventsTracker.cs304Finished)
        {
            ConversationManager.Instance.SetBool("cs304Finished", true);
        }
        if (eventsTracker.cs240Finished)
        {
            ConversationManager.Instance.SetBool("cs240Finished", true);
        }
    }

}
