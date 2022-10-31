using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

/// <summary>
/// Controls the NPC in Science hub that needs help on CS231 material
/// </summary>
public class CS231NPCController : MonoBehaviour
{
    //Declare variables
    NPCConversation cs231NPC;
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
        cs231NPC = GameObject.FindGameObjectWithTag("CS231").GetComponent<NPCConversation>(); //can be more efficient to say GetComponent<NPC...> bc this script is attached to the right object
        eventsTracker = FindObjectOfType<SceneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        NPCAnim = GameObject.FindGameObjectWithTag("CS231").GetComponent<Animator>();
        correctSound = FindObjectOfType<CanvasController>();
    }
    void Update()
    {
        //if player in conversation...
        if (startedConvo)
        {
            //start talking animations
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

            //cna't restart convo if currently in convo
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
        //if collision game object is player and not engaged in convo...
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start convo
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs231NPC);
        }
    }

    /// <summary>
    /// Sets triggers for different dialogue branch depending on player's current progress
    /// </summary>
    public void SetNPCDialogue()
    {
        //check if player currently has the ability to help NPC, play different dialogue accordingly
        if (eventsTracker.cs231Finished)
        {
            ConversationManager.Instance.SetBool("cs231Finished", true);
        }
        if (eventsTracker.helpedCS231 == true)
        {
            ConversationManager.Instance.SetBool("hasHelped", true);
        }
        if (eventsTracker.eventsCompleted == eventsTracker.TOTAL_EVENTS)
        {
            ConversationManager.Instance.SetBool("hasFinished", true);
        }
    }


    /// <summary>
    /// Triggers start of HelpedPeerCoroutine and increments SceneController's event completion tracker
    /// </summary>
    public void HelpedPeer()
    {
        //letting program know we have helped our peer
        eventsTracker.helpedCS231 = true;
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