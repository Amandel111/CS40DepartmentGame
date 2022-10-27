using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CS111NPCController : MonoBehaviour
{
    /*
     * This class controls the NPC in Science hub that needs help on CS111 material
     * */
    NPCConversation cs111NPC;
    LevelOneController eventsTracker;
    Animator playerAnim;
    bool startedConvo;
    Transform playerTransform;
    public float dialogueRange = 4;
    Animator NPCAnim;
    CanvasController correctSound;
    //AudioSource correctSound;
    void Start()
    {
        cs111NPC = GameObject.FindGameObjectWithTag("CS111").GetComponent<NPCConversation>();
        eventsTracker = FindObjectOfType<LevelOneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        NPCAnim = GameObject.FindGameObjectWithTag("CS111").GetComponent<Animator>();
        correctSound = FindObjectOfType<CanvasController>();
    }
    void Update()
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
        //start a conversation
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs111NPC);
        }
        if (eventsTracker.eventsCompleted == eventsTracker.TOTAL_EVENTS)
        {
            ConversationManager.Instance.SetBool("hasFinished", true);
        }
    }
    public void SetNPCDialogue()
    {
        //check if player currently has the ability to help NPC, play different dialogue accordingly
        if (eventsTracker.cs111Finished)
        {
            ConversationManager.Instance.SetBool("cs111Finished", true);
        }
        if (eventsTracker.helpedCS111 == true)
        {
            ConversationManager.Instance.SetBool("hasHelped", true);
        }
    }
    public void HelpedPeer()
    {
        //letting program know we have helped our peer
        eventsTracker.helpedCS111 = true;
        eventsTracker.helpedCounter++;
        eventsTracker.eventsCompleted++;
        StartCoroutine(HelpedPeerCoroutine());
        //add particle system
    }

    private IEnumerator HelpedPeerCoroutine()
    {
        Debug.Log("calls helped peer function");
        //correctSound.Play();
        correctSound.correctSound.Play();
        Debug.Log("Played sound");
        playerAnim.SetBool("helpedPeer", true);
        yield return new WaitForSeconds(3f);
        playerAnim.SetBool("helpedPeer", false);
    }
}
