using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS304Controller : MonoBehaviour
{
    // Start is called before the first frame update
    LevelOneController eventTracker;
    NPCConversation cs304Convo;
    int timesInConversation = 0;
    bool startedConvo;
    Animator playerAnim;

    Transform playerTransform;
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        cs304Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();

        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        /*
        switch (eventTracker.previousScene)
        {
            case "CS231":
                playerTransform.position = new Vector3();
                break;
            case "Hub":
                playerTransform.position = new Vector3();
                break;
            case "CS111":
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
            ConversationManager.Instance.StartConversation(cs304Convo); //figure out how to prevent convo from restarting --> maybe && !(talkingAnimation active) u can interact w npc
            if (eventTracker.cs304Finished) //more elegant way to do this
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

    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS304);
    }
}
