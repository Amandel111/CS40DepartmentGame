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
    Transform playerTransform;
    public float dialogueRange = 4;
    Animator NPCAnim;
    AudioSource correctSound;
    void Start()
    {
        cs231NPC = GameObject.FindGameObjectWithTag("CS231").GetComponent<NPCConversation>(); //can be more efficient to say GetComponent<NPC...> bc this script is attached to the right object
        eventsTracker = FindObjectOfType<LevelOneController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        NPCAnim = GameObject.FindGameObjectWithTag("CS231").GetComponent<Animator>();

        correctSound = FindObjectOfType<Canvas>().GetComponent<AudioSource>();
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
            ConversationManager.Instance.StartConversation(cs231NPC);
        }
    }
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
    public void HelpedPeer()
    {
        //letting program know we have helped our peer
        eventsTracker.helpedCS231 = true;
        eventsTracker.helpedCounter++;
        eventsTracker.eventsCompleted++;
        StartCoroutine(HelpedPeerCoroutine());
        //add particle system
    }
    private IEnumerator HelpedPeerCoroutine()
    {
        correctSound.Play();
        playerAnim.SetBool("helpedPeer", true);
        yield return new WaitForSeconds(3f);
        playerAnim.SetBool("helpedPeer", false);
    }
}