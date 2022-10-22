using DialogueEditor;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    /*
     * This script controls all the dialogue in the game, ie the events required to start dialogue in-game
     */
    public NPCConversation conversation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //start a convo with npc
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collision.gameObject.CompareTag("player"))
            {
                ConversationManager.Instance.StartConversation(conversation);
            }
        }
    }
}
