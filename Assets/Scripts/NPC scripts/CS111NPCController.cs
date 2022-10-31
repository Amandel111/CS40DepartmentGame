using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

/// <summary>
/// Controls the NPC in Science hub needing help on CS111 material
/// </summary>
public class CS111NPCController : MonoBehaviour
{
    //declare variables
    NPCConversation cs111NPC;
    SceneController eventsTracker;
    Animator playerAnim;
    bool startedConvo;
    Transform playerTransform;
    public float dialogueRange = 4;
    Animator NPCAnim;
    CanvasController correctSound;
    void Start()
    {
        //initialize variables
        cs111NPC = GameObject.FindGameObjectWithTag("CS111").GetComponent<NPCConversation>();
        eventsTracker = FindObjectOfType<SceneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        NPCAnim = GameObject.FindGameObjectWithTag("CS111").GetComponent<Animator>();
        correctSound = FindObjectOfType<CanvasController>();
    }
    void Update()
    {
        //if player in convo...
        if (startedConvo)
        {
            //set convo animations
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
        //if collision object is player and not in conversation
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start a conversation
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs111NPC);
        }

        //if assignment has been completed...
        if (eventsTracker.eventsCompleted == eventsTracker.TOTAL_EVENTS)
        {
            ///trigger hasFinished dialogue from NPC
            ConversationManager.Instance.SetBool("hasFinished", true);
        }
    }

    /// <summary>
    /// Sets triggers for different dialogue branch depending on player's current progress
    /// </summary>
    public void SetNPCDialogue()
    {
        //check if player currently has finish CS111 quiz...
        if (eventsTracker.cs111Finished)
        {
            //if so, trigger dialogue to help NPC
            ConversationManager.Instance.SetBool("cs111Finished", true);
        }

        //if player has already helped cs111 NPC
        if (eventsTracker.helpedCS111 == true)
        {
            ///trigger branching dialogue
            ConversationManager.Instance.SetBool("hasHelped", true);
        }
    }

    /// <summary>
    /// Triggers start of HelpedPeerCoroutine and increments SceneController's event completion tracker
    /// </summary>
    public void HelpedPeer()
    {
        //letting program know player has helped cs111 NPC
        eventsTracker.helpedCS111 = true;
        eventsTracker.helpedCounter++;
        eventsTracker.eventsCompleted++;
        StartCoroutine(HelpedPeerCoroutine());
    }

    /// <summary>
    /// Play music and animation showing player has helped NPC
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
