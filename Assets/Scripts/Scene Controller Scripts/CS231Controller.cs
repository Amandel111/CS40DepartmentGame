using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls dialogue and events in CS231 scene
/// </summary>
public class CS231Controller : MonoBehaviour
{
    //Declare variables
    SceneController eventsTracker;
    NPCConversation cs231Convo;
    Animator playerAnim;
    bool startedConvo;
    Animator NPCAnim;
    Transform playerTransform;
    public float dialogueRange = 4;

    void Start()
    {
        //initialize variables
        eventsTracker = FindObjectOfType<SceneController>();
        cs231Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        NPCAnim = NPCAnim = GameObject.FindGameObjectWithTag("CS231").GetComponent<Animator>();

        //set previous scene to trigger player spawnpoint
        SceneController.previousScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        //if player in conversation...
        if (startedConvo)
        {
            //trigger talking animations
            if (ConversationManager.Instance.GetBool("isTalking"))
            {
                playerAnim.SetBool("isTalking", true);
                NPCAnim.SetBool("isTalking", true);
            }
            else
            {
                playerAnim.SetBool("isTalking", false);
                NPCAnim.SetBool("isTalking", false);
            }
            
            //can't restart dialogue
            if (!ConversationManager.Instance.GetBool("collidedWithDialogue"))
            {
                startedConvo = false;
            }

            //if player leaves range, conversation ends
            if (Vector2.Distance(transform.position, playerTransform.position) > dialogueRange)
            {
                ConversationManager.Instance.SetBool("collidedWithDialogue", false);
                ConversationManager.Instance.SetBool("isTalking", false);
                ConversationManager.Instance.EndConversation();
                startedConvo = false;
                playerAnim.SetBool("isTalking", false);
                NPCAnim.SetBool("isTalking", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if collision object is player and not in conversation...
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start convo
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs231Convo);

            //update SceneController's memory of current  progress
            if (eventsTracker.cs231Finished)
            {
                ConversationManager.Instance.SetBool("finishedCS231", true);
            }

        }
    }

    /// <summary>
    /// Enable quiz UI, load a random quesiton into quiz
    /// </summary>
    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventsTracker.GetRandomQuestion(eventsTracker.questionsCS231);
    }
}
