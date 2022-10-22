using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS231Controller : MonoBehaviour
{
    // Start is called before the first frame update
    LevelOneController eventTracker;
    NPCConversation cs231Convo;
    Animator playerAnim;
    bool startedConvo;

    Transform playerTransform;
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        cs231Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();

        //if (eventTracker.previousScene == "CS231")
        //{
        //   playerTransform.position = new Vector3();
        //
        //}
        /*
        switch (eventTracker.previousScene)
        {
            case "Hub":
                playerTransform.position = new Vector3();
                break;
            case "CS304":
                playerTransform.position = new Vector3();
                break;
            case "CS111":
                playerTransform.position = new Vector3();
                break;
            case "CS240":
                playerTransform.position = new Vector3();
                break;
            case "MainMenu":
                playerTransform.position = new Vector3();
                break;
        }

        */
        LevelOneController.previousScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (startedConvo)
        {
            if (ConversationManager.Instance.GetBool("isTalking"))
            {
                playerAnim.SetBool("isTalking", true);
            }
            else
            {
                playerAnim.SetBool("isTalking", false);
            }
            if (!ConversationManager.Instance.GetBool("collidedWithDialogue"))
            {
                startedConvo = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs231Convo); //figure out how to prevent convo from restarting --> maybe && !(talkingAnimation active) u can interact w npc
            if (eventTracker.cs231Finished) //more elegant way to do this
            {
                ConversationManager.Instance.SetBool("finishedCS231", true);
            }

        }
    }

    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS231);
    }
}
