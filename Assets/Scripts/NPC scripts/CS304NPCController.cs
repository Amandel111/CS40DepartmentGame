using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

/// <summary>
/// Controls the NPC in Science hub that needs help on CS304 material
/// </summary>
public class CS304NPCController : MonoBehaviour
{
    //declare variables
    NPCConversation cs304NPC;
    SceneController eventsTracker;
    Animator playerAnim;
    Transform playerTransform;
    bool startedConvo;
    Animator NPCAnim;
    CanvasController correctSound;
    public float dialogueRange = 4;

    void Start()
    {
        //initialize variables
        cs304NPC = GameObject.FindGameObjectWithTag("CS304").GetComponent<NPCConversation>();
        eventsTracker = FindObjectOfType<SceneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        NPCAnim = GameObject.FindGameObjectWithTag("CS304").GetComponent<Animator>();
        correctSound = FindObjectOfType<CanvasController>();
    }
    void Update()
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

            //can't restart dialogue
            if (!ConversationManager.Instance.GetBool("collidedWithDialogue"))
            {
                startedConvo = false;
            }

            //if player leaves range, convesation ends
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
            ConversationManager.Instance.StartConversation(cs304NPC);
        }
    }
    /// <summary>
    /// Sets dialogue branches depending on player's current progress in game
    /// </summary>
    public void SetNPCDialogue()
    {
        //check if player currently has the ability to help NPC, play different dialogue accordingly
        if (eventsTracker.cs304Finished)
        {
            ConversationManager.Instance.SetBool("cs304Finished", true);
        }
        if (eventsTracker.helpedCS304 == true)
        {
            ConversationManager.Instance.SetBool("hasHelped", true);
        }
        if (eventsTracker.eventsCompleted == eventsTracker.TOTAL_EVENTS)
        {
            ConversationManager.Instance.SetBool("hasFinished", true);
        }
    }

    /// <summary>
    /// Starts HelpedPeerCoroutine and updates SceneController's memory of player's progress in game
    /// </summary>
    public void HelpedPeer()
    {
        //letting program know we have helped our peer
        eventsTracker.helpedCS304 = true;
        eventsTracker.helpedCounter++;
        eventsTracker.eventsCompleted++;
        StartCoroutine(HelpedPeerCoroutine());
    }

    /// <summary>
    /// plays sound and animation corresponding to player helping NPC
    /// </summary>
    /// <returns></returns>
    private IEnumerator HelpedPeerCoroutine()
    {
        correctSound.correctSound.Play();
        playerAnim.SetBool("helpedPeer", true);
        yield return new WaitForSeconds(3f);
        playerAnim.SetBool("helpedPeer", false);
    }
}