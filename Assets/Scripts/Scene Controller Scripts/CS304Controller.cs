using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls dialogue and events in CS304 scene
/// </summary>
public class CS304Controller : MonoBehaviour
{
    //Declare Variables
    SceneController eventTracker;
    NPCConversation cs304Convo;
    int timesInConversation = 0;
    bool startedConvo;
    Animator playerAnim;
    Animator NPCAnim;
    Transform playerTransform;
    public float dialogueRange = 4;

    void Start()
    {
        //initialize variables
        eventTracker = FindObjectOfType<SceneController>();
        cs304Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();

        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        NPCAnim = GameObject.FindGameObjectWithTag("CS304").GetComponent<Animator>();

        //set previous scene to trigger player spawnpoint
        SceneController.previousScene = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        //if player in conversation...
        if (startedConvo)
        {
            //trigger talking animation
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

            //can't restart conversation
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
        //if collision object is player and not in convo...
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start convo
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs304Convo);
            
            //Depending on player's progress, set triggers to choose branching dialogue
            if (eventTracker.cs304Finished)
            {
                ConversationManager.Instance.SetBool("finishedCS304", true);
            }
            if (timesInConversation > 0) //more elegant way to do this
            {
                ConversationManager.Instance.SetBool("givenIndependenceSpeech", true);
            }
            timesInConversation++;

        }
    }

    /// <summary>
    /// Enable quiz UI, load a random quesiton into quiz
    /// </summary>
    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS304);
    }
}
