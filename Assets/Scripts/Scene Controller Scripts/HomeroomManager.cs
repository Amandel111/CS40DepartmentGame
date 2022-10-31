using DialogueEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the homeroom classroom, prevents player from leaving scene too early, controls dialogue w/ homeroom teacher, hold submit assignment functionality
/// </summary>
public class HomeroomManager : MonoBehaviour
{
    //declare variables
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
        
        //if player received assignment from homeroom teacher
        if (eventTracker.hasReceivedAssignment)
        {
            //destroy the obstacle preventing player from leaving scene
            Destroy(blockExit);
        }

        //update previous scene to set scene spawn points
        SceneController.previousScene = SceneManager.GetActiveScene().name;

        //UI should be off until conditions met
        winPanel.SetActive(false);
    }

    void Update()
    {
        //if player is in conversation...
        if (startedConvo)
        {
            //trigger dialogue for recieving assignment and blocking exit 
            if (ConversationManager.Instance.GetBool("receivedAssignment"))
            {
                eventTracker.hasReceivedAssignment = true;
                Destroy(blockExit);
                //insteaad of received assignment, put an exclamation mark around teacher!
            }

            //trigger dialogue for player having completed assignment
            if (eventTracker.eventsCompleted == eventTracker.TOTAL_EVENTS)
            {
                ConversationManager.Instance.SetBool("isFinished", true);
            }

            //trigger talking animation for NPC and player
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

            //cannot restart convo through collision if in middle of convo
            if (!ConversationManager.Instance.GetBool("collidedWithDialogue"))
            {
                Debug.Log("started convo: false");
                startedConvo = false;
            }

            //if player walks out of range, conversation ends
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
