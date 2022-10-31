using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

/// <summary>
/// Controls background NPC conversations
/// </summary>
public class NPCDialogueController : MonoBehaviour
{
    //declare variables
    private NPCConversation conversation;
    bool startedConvo;
    Transform playerTransform;
    Animator npcAnim;
    public int dialogueRange = 4;
    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        conversation = GetComponent<NPCConversation>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        npcAnim = GetComponent<Animator>();
    }

    void Update()
    {
        //if player in conversation...
        if (startedConvo)
        {
            //play talking animation
            if (ConversationManager.Instance.GetBool("isTalking"))
            {
                npcAnim.SetBool("isTalking", true);
            }
            else
            {
                npcAnim.SetBool("isTalking", false);
            }

            //can't re-collide and start new conversation mid-convo
            if (!ConversationManager.Instance.GetBool("collidedWithDialogue"))
            {
                startedConvo = false;
            }

            //walk outsude of radius dialogueRange and conversation ends
            if (Vector2.Distance(transform.position, playerTransform.position) > dialogueRange)
            {
                Debug.Log("left range");
                ConversationManager.Instance.SetBool("collidedWithDialogue", false);
                ConversationManager.Instance.SetBool("isTalking", false);
                ConversationManager.Instance.EndConversation();
                npcAnim.SetBool("isTalking", false);
                Debug.Log("turns off convo bc out of range");
                startedConvo = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if game object is player and not currently engaged in conversation...
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            //start a conversation 
            Debug.Log("Has collided");
            startedConvo = true;
            ConversationManager.Instance.StartConversation(conversation);
        }
    }
}