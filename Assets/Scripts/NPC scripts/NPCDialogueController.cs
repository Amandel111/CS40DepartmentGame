using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCDialogueController : MonoBehaviour
{
    private NPCConversation conversation;
    bool startedConvo;
    Transform playerTransform;
    Animator npcAnim;
    public int dialogueRange = 4;
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        npcAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (startedConvo)
        {
            if (ConversationManager.Instance.GetBool("isTalking"))
            {
                npcAnim.SetBool("isTalking", true);
            }
            else
            {
                npcAnim.SetBool("isTalking", false);
            }
            if (!ConversationManager.Instance.GetBool("collidedWithDialogue"))
            {
                startedConvo = false;
            }
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
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            Debug.Log("Has collided");
            startedConvo = true;
            ConversationManager.Instance.StartConversation(conversation);
        }
    }
}