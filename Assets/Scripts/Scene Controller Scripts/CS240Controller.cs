using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class CS240Controller : MonoBehaviour
{
    LevelOneController eventTracker;
    NPCConversation cs240Convo;
    bool startedConvo;
    Animator playerAnim;
    Transform playerTransform;
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        cs240Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();

        /*
        switch (eventTracker.previousScene)
        {
            case "CS231":
                playerTransform.position = new Vector3();
                break;
            case "CS304":
                playerTransform.position = new Vector3();
                break;
            case "CS111":
                playerTransform.position = new Vector3();
                break;
            case "Hub":
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
            ConversationManager.Instance.StartConversation(cs240Convo); //figure out how to prevent convo from restarting --> maybe && !(talkingAnimation active) u can interact w npc
            if (eventTracker.cs240Finished) //more elegant way to do this
            {
                ConversationManager.Instance.SetBool("finishedCS240", true);
            }

        }
    }

    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS240);
    }
}