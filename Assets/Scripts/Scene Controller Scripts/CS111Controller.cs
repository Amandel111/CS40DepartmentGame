using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS111Controller : MonoBehaviour
{
    LevelOneController eventTracker;
    NPCConversation cs111Convo;
    bool startedConvo;
    Animator playerAnim;
    Transform playerTransform;
    public float dialogueRange;
    // Start is called before the first frame update
    void Start()
    {
        eventTracker = FindObjectOfType<LevelOneController>();
        cs111Convo = FindObjectOfType<NPCConversation>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();

        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();

        LevelOneController.previousScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
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
        if (collision.gameObject.CompareTag("Player") && !startedConvo)
        {
            startedConvo = true;
            ConversationManager.Instance.StartConversation(cs111Convo); 
            if (eventTracker.cs111Finished) //more elegant way to do this
            {
                ConversationManager.Instance.SetBool("finishedCS111", true);
            }

        }

    }

    public void CallQuizInDialogue()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(true);
        eventTracker.GetRandomQuestion(eventTracker.questionsCS111);
    }
}
