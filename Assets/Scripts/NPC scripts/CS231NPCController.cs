using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CS231NPCController : MonoBehaviour
{
    /*
     * This class controls the NPC in Science hub that needs help on CS111 material
     * */
    NPCConversation cs231NPC;
    LevelOneController eventsTracker;
    Animator playerAnim;
    bool startedConvo;
    void Start()
    {
        cs231NPC = GameObject.FindGameObjectWithTag("CS231").GetComponent<NPCConversation>();
        eventsTracker = FindObjectOfType<LevelOneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //start a conversation
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs231NPC);
        }
    }
    public void SetNPCDialogue()
    {
        //check if player currently has the ability to help NPC, play different dialogue accordingly
        if (eventsTracker.cs111Finished)
        {
            ConversationManager.Instance.SetBool("cs231Finished", true);
        }
        if (eventsTracker.helpedCS111 == true)
        {
            ConversationManager.Instance.SetBool("hasHelped", true);
        }
    }
    public void HelpedPeer()
    {
        //letting program know we have helped our peer
        eventsTracker.helpedCS231 = true;
        eventsTracker.helpedCounter++;
        //do some animation here, essentially animate character w lightbulb bouncing above head like w lightbulb
        //add particle system
    }
}