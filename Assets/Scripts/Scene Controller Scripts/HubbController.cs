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
                playerTransform.position = new Vector3(-1, -13, 0); //fix hard coding
                break;
            case "CS304 Classroom":
                playerTransform.position = new Vector3(9, -17, 0);
                break;
            case "CS111 Classroom":
                playerTransform.position = new Vector3(19, -12, 0);
                break;
            case "CS240 Classroom":
                 playerTransform.position = new Vector3(-1.5f, -24, 0);
                break;
        }

        LevelOneController.previousScene = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
