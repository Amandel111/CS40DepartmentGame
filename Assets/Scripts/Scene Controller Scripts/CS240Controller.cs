using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls dialogue and events in CS240 scene
/// </summary>
public class CS240Controller : MonoBehaviour
{
    //declare variables
    SceneController eventsTracker;
    NPCConversation cs240Convo;
    bool startedConvo;
    Animator playerAnim;
    Transform playerTransform;
    public float dialogueRange = 4;
    Animator NPCAnim;
    void Start()
    {
        //initialize variables
        eventsTracker = FindObjectOfType<SceneController>();
        cs240Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        NPCAnim = GameObject.FindGameObjectWithTag("CS240").GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;

        //set previous scene to trigger player spawnpoint
        SceneController.previousScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        //if player in convo...
        if (startedConvo)
        {
            //set talking animation
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

            //cannot restart conversation if in middle of convo
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
        //if collision game object is player and not in conversation
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start converation
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs240Convo);
            
            //if CS240 quiz is completed, trigger different dialogue branch
            if (eventsTracker.cs240Finished)
            {
                ConversationManager.Instance.SetBool("finishedCS240", true);
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
        eventsTracker.GetRandomQuestion(eventsTracker.questionsCS240);
    }
}