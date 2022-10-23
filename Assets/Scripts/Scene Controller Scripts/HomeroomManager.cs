using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeroomManager : MonoBehaviour
{
    /*
     * This class controls the homeroom classroom, prevents player from leaving scene too early, controls dialogue w/ teacher, and 
     * holds functions event buttons call from dialogue to submit assignment at end of game
     * */
    NPCConversation homeroomConversation;
    GameObject blockExit;
    bool startedConvo;
    LevelOneController eventTracker;
    Animator playerAnim;
    Transform playerTransform;
    public float dialogueRange = 4;
    // Start is called before the first frame update
    void Start()
    {
        homeroomConversation = FindObjectOfType<NPCConversation>();
        //blockExit = GameObject.FindGameObjectWithTag("blockExit");
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        eventTracker = FindObjectOfType<LevelOneController>();
        LevelOneController.previousScene = SceneManager.GetActiveScene().name;
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        
        if (startedConvo)
        {
            if (ConversationManager.Instance.GetBool("receivedAssignment")) //&& blockExit != nul
            {
                //Destroy(blockExit);
                //insteaad of received assignment, put an exclamation mark around teacher!
            }
            if (eventTracker.eventsCompleted == eventTracker.TOTAL_EVENTS)
            {
                ConversationManager.Instance.SetBool("isFinished", true);
            }
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
                Debug.Log("started convo: false");
                startedConvo = false;
            }
            if (Vector2.Distance(transform.position, playerTransform.position) > dialogueRange)
            {
                ConversationManager.Instance.SetBool("collidedWithDialogue", false);
                ConversationManager.Instance.SetBool("isTalking", false);
                ConversationManager.Instance.EndConversation();
                startedConvo = false;
                playerAnim.SetBool("isTalking", false);
            }
        }


    }
    public void submitAssignment()
    {
        //right now Idk how to save memory so that dialogue can call this object even though it doesn't edxist in scene yet
        //sparkles go off
        //end game screen
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            ConversationManager.Instance.StartConversation(homeroomConversation);
            startedConvo = true;
            Debug.Log("startedConvo: true");
        }

    }

}
