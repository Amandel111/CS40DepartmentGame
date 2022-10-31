using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls dialogue and events in CS111 scene
/// </summary>
public class CS111Controller : MonoBehaviour
{
    //declare variables
    SceneController eventsTracker;
    NPCConversation cs111Convo;
    bool startedConvo;
    Animator playerAnim;
    Animator NPCAnim;
    Transform playerTransform;
    public float dialogueRange;

    void Start()
    {
        //initialize variables
        eventsTracker = FindObjectOfType<SceneController>();
        cs111Convo = FindObjectOfType<NPCConversation>();
        NPCAnim = GameObject.FindGameObjectWithTag("CS111").GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();

        //set previous scene to trigger player spawnpoint
        SceneController.previousScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        //if player in conversation...
        if (startedConvo)
        {
            //trigger talking animation
            if (ConversationManager.Instance.GetBool("isTalking"))
            {
                playerAnim.SetBool("isTalking", true);
                NPCAnim.SetBool( "isTalking", true);
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

            //if player leaves range, dialogue ends
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
        //if collision object is player and not in convo...
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start convo
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs111Convo); 

            //if player conpleted CS111 quiz, trigger branching dialogue
            if (eventsTracker.cs111Finished)
            {
                ConversationManager.Instance.SetBool("finishedCS111", true);
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
            eventsTracker.GetRandomQuestion(eventsTracker.questionsCS111);
    }
}
