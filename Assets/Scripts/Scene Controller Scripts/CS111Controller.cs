using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS111Controller : MonoBehaviour
{
    LevelOneController eventTracker;
    NPCConversation cs111Convo;
    bool startedConvo;
    Animator playerAnim;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        cs111Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();

        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        /*
        switch (eventTracker.previousScene)
        {
            case "CS231":
                playerTransform.position = new Vector3();
                break;
            case "CS304":
                playerTransform.position = new Vector3();
                break;
            case "Hub":
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

    // Update is called once per frame
    void Update()
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
            ConversationManager.Instance.StartConversation(cs111Convo); //figure out how to prevent convo from restarting --> maybe && !(talkingAnimation active) u can interact w npc
            if (eventTracker.cs111Finished) //more elegant way to do this
            {
                ConversationManager.Instance.SetBool("finishedCS111", true);
            }

        }

    }

    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS111);
    }
}
