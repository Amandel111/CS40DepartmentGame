using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubbController : MonoBehaviour
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
            case "CS231 Classroom":
                playerTransform.position = new Vector3(14.8f, -9.7f, 0); //fix hard coding
                break;
            case "CS304 Classroom":
                playerTransform.position = new Vector3(4.7f, -15.2f, 0);
                break;
            case "CS240 Classroom":
                 playerTransform.position = new Vector3(-6.1f, -20.4f, 0);
                break;
        }

        LevelOneController.previousScene = SceneManager.GetActiveScene().name;

    }

    public void SetNPCDialogue()
    {
        LevelOneController eventsTracker = FindObjectOfType<LevelOneController>();
       // if (eventsTracker.cs111Finished)
        //{
         //   ConversationManager.Instance.SetBool("cs111Finished", true);
        //}
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
