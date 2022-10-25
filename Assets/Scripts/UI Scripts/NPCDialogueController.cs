using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCDialogueController : MonoBehaviour
{
    private NPCConversation conversation;
    bool startedConvo;
    Transform playerTransform;
    Animator playerAnim;
    public int dialogueRange = 4;
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
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
            if (Vector2.Distance(transform.position, playerTransform.position) > dialogueRange)
            {
                Debug.Log("left range");
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
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            Debug.Log("Has collided");
            startedConvo = true;
            ConversationManager.Instance.StartConversation(conversation);

        }
    }
}