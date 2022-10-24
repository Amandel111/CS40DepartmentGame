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
    Animator NPCAnim;

    Transform playerTransform;
    public float dialogueRange = 4;
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        cs304Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();

        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        NPCAnim = GameObject.FindGameObjectWithTag("CS304").GetComponent<Animator>();
        LevelOneController.previousScene = SceneManager.GetActiveScene().name;
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
        if (eventTracker.questionsCS304.Count == 0)
        {
            Debug.Log("out of questions");
            return;
        }
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS304);
    }
}
