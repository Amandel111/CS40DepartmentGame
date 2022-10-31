using DialogueEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls the homeroom classroom, prevents player from leaving scene too early, controls dialogue w/ teacher, and 
/// holds functions event buttons call from dialogue to submit assignment at end of game
/// </summary>
public class HomeroomManager : MonoBehaviour
{
    NPCConversation homeroomConversation;
    GameObject blockExit;
    bool startedConvo;
    SceneController eventTracker;
    Animator playerAnim;
    Transform playerTransform;
    public float dialogueRange = 4;
    Animator NPCAnim;
    GameObject winPanel;

    void Start()
    {
        //initialize variables
        homeroomConversation = FindObjectOfType<NPCConversation>();
        blockExit = GameObject.FindGameObjectWithTag("blockExit");
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        eventTracker = FindObjectOfType<SceneController>();
        SceneController.previousScene = SceneManager.GetActiveScene().name;
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        NPCAnim = GameObject.FindGameObjectWithTag("Homeroom").GetComponent<Animator>();
        winPanel = GameObject.FindGameObjectWithTag("WinPanel");
        
        //
        if (eventTracker.hasReceivedAssignment)
        {
            Destroy(blockExit);
        }
        SceneController.previousScene = SceneManager.GetActiveScene().name;
        winPanel.SetActive(false);
    }

    void Update()
    {
        
        if (startedConvo)
        {
            if (ConversationManager.Instance.GetBool("receivedAssignment")) //&& blockExit != nul
            {
                eventTracker.hasReceivedAssignment = true;
                Destroy(blockExit);
                //insteaad of received assignment, put an exclamation mark around teacher!
            }
            if (eventTracker.eventsCompleted == eventTracker.TOTAL_EVENTS)
            {
                ConversationManager.Instance.SetBool("isFinished", true);
            }
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
                NPCAnim.SetBool("isTalking", false);
            }
        }


    }
    /// <summary>
    /// sets win UI and starts LoadMenuGameComplete coroutine
    /// </summary>
    public void SubmitAssignment()
    {
        winPanel.SetActive(true);
        StartCoroutine(LoadMenuGameComplete());
    }
    /// <summary>
    /// Loads main menu after game is complete
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadMenuGameComplete()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Main Menu");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the game collided object is a player and isn't already in a conversation...
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start a conversation
            ConversationManager.Instance.StartConversation(homeroomConversation);
            startedConvo = true;
            //if the assignment is finished
            if (eventTracker.isFinished)
            {
                ConversationManager.Instance.SetBool("isFinished", true);
            }
        }

    }

}
