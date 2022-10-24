using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS231Controller : MonoBehaviour
{
    // Start is called before the first frame update
    LevelOneController eventTracker;
    NPCConversation cs231Convo;
    Animator playerAnim;
    bool startedConvo;
    Animator NPCAnim;

    Transform playerTransform;
    public float dialogueRange = 4;
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        cs231Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        LevelOneController.previousScene = SceneManager.GetActiveScene().name;
        NPCAnim = NPCAnim = GameObject.FindGameObjectWithTag("CS231").GetComponent<Animator>();
    }

    private void Update()
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
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs231Convo); //figure out how to prevent convo from restarting --> maybe && !(talkingAnimation active) u can interact w npc
            if (eventTracker.cs231Finished) //more elegant way to do this
            {
                ConversationManager.Instance.SetBool("finishedCS231", true);
            }

        }
    }

    public void CallQuizInDialogue()
    {
        if (eventTracker.questionsCS231.Count == 0)
        {
            Debug.Log("out of questions");
            return;
        }
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS231);
    }
}
