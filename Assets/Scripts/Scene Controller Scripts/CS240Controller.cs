using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class CS240Controller : MonoBehaviour
{
    SceneController eventsTracker;
    NPCConversation cs240Convo;
    bool startedConvo;
    Animator playerAnim;
    Transform playerTransform;
    public float dialogueRange = 4;
    Animator NPCAnim;
    void Start()
    {
        eventsTracker = FindObjectOfType<SceneController>();
        cs240Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        NPCAnim = GameObject.FindGameObjectWithTag("CS240").GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        SceneController.previousScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (startedConvo)
        {
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs240Convo); //figure out how to prevent convo from restarting --> maybe && !(talkingAnimation active) u can interact w npc
            if (eventsTracker.cs240Finished) //more elegant way to do this
            {
                ConversationManager.Instance.SetBool("finishedCS240", true);
            }

        }
    }

    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventsTracker.GetRandomQuestion(eventsTracker.questionsCS240);
    }
}