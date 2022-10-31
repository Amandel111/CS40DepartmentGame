using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;


/// <summary>
/// This class controls the NPC in Science hub that needs help on CS240 material
/// </summary>
public class CS240NPCController : MonoBehaviour
{
    //Declare variables
    NPCConversation cs240NPC;
    SceneController eventsTracker;
    Animator playerAnim;
    bool startedConvo;
    public float dialogueRange = 4;
    Animator NPCAnim;
    CanvasController correctSound;
    Transform playerTransform;
    void Start()
    {
        //initialize variables
        cs240NPC = GameObject.FindGameObjectWithTag("CS240").GetComponent<NPCConversation>();
        eventsTracker = FindObjectOfType<SceneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        NPCAnim = GameObject.FindGameObjectWithTag("CS240").GetComponent<Animator>();
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

            //can't restart convo if currently in convo
            if (!ConversationManager.Instance.GetBool("collidedWithDialogue"))
            {
                startedConvo = false;
            }

            //if player leaves range, convo ends
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
        //if collision game object is player and not in conversation...
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start conversation
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs240NPC);
        }

        //if assignment finished...
        if (eventsTracker.eventsCompleted == eventsTracker.TOTAL_EVENTS)
        {
            //trigger according dialogue
            ConversationManager.Instance.SetBool("hasFinished", true);
        }
    }

    /// <summary>
    /// Sets triggers for different dialogue branch depending on player's current progress
    /// </summary>
    public void SetNPCDialogue()
    {
        //check if player currently has the ability to help NPC, play different dialogue accordingly
        if (eventsTracker.cs240Finished)
        {
            ConversationManager.Instance.SetBool("cs240Finished", true);
        }
        if (eventsTracker.helpedCS240 == true)
        {
            ConversationManager.Instance.SetBool("hasHelped", true);
        }
    }


    /// <summary>
    /// Triggers start of HelpedPeerCoroutine and increments SceneController's event completion tracker
    /// </summary>
    public void HelpedPeer()
    {
        //letting program know we have helped our peer
        eventsTracker.helpedCS240 = true;
        eventsTracker.eventsCompleted++;
        eventsTracker.helpedCounter++;
        StartCoroutine(HelpedPeerCoroutine());
        //add particle system
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