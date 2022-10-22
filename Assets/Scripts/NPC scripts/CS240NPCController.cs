using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CS240NPCController : MonoBehaviour
{
    /*
     * This class controls the NPC in Science hub that needs help on CS111 material
     * */
    NPCConversation cs240NPC;
    LevelOneController eventsTracker;
    Animator playerAnim;
    bool startedConvo;
    public float dialogueRange = 4;

    Transform playerTransform;
    void Start()
    {
        cs240NPC = GameObject.FindGameObjectWithTag("CS240").GetComponent<NPCConversation>();
        eventsTracker = FindObjectOfType<LevelOneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform; 
    }
    void Update()
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
            if (Vector2.Distance(transform.position, playerTransform.position) > dialogueRange)
            {
                ConversationManager.Instance.SetBool("collidedWithDialogue", false);
                ConversationManager.Instance.SetBool("isTalking", false);
                ConversationManager.Instance.EndConversation();
                startedConvo = false;
                playerAnim.SetBool("isTalking", false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //start a conversation
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs240NPC);
        }
    }
    public void SetNPCDialogue()
    {
        //check if player currently has the ability to help NPC, play different dialogue accordingly
        if (eventsTracker.cs111Finished)
        {
            ConversationManager.Instance.SetBool("cs240Finished", true);
        }
        if (eventsTracker.helpedCS111 == true)
        {
            ConversationManager.Instance.SetBool("hasHelped", true);
        }
    }
    public void HelpedPeer()
    {
        //letting program know we have helped our peer
        eventsTracker.helpedCS240 = true;
        eventsTracker.helpedCounter++;
        //do some animation here, essentially animate character w lightbulb bouncing above head like w lightbulb
        //add particle system
    }
}