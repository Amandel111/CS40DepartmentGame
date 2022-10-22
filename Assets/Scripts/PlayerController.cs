using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /*
     * Controls player movement, animation, collision events
     * */
    Rigidbody2D myRigidBody;
    public float speed = 1.5f;

    private GameObject popUPImage;
    private VerticalLayoutGroup panelUI;
    public Course currentCourse = Course.CS240;

    private TMP_Text questionText;
    private TMP_Text answerOneText;
    private TMP_Text answerTwoText;
    private TMP_Text answerThreeText;
    private TMP_Text answerFourText;

    private Image lightbulb;
    private Image handHold;

    private Animator anim;

    //public static PlayerController Instance;
    public enum Course
    {
        CS240,
        CS111,
        CS231,
        CS304,
        MAS
    }

    // Start is called before the first frame update
    void Start()
    {
        panelUI = FindObjectOfType<VerticalLayoutGroup>();
        popUPImage = GameObject.FindGameObjectWithTag("popUpImage");
        questionText = GameObject.FindGameObjectWithTag("question").GetComponent<TMP_Text>();
        answerOneText = GameObject.FindGameObjectWithTag("answerOne").GetComponent<TMP_Text>();
        answerTwoText = GameObject.FindGameObjectWithTag("answerTwo").GetComponent<TMP_Text>();
        answerThreeText = GameObject.FindGameObjectWithTag("answerThree").GetComponent<TMP_Text>();
        answerFourText = GameObject.FindGameObjectWithTag("answerFour").GetComponent<TMP_Text>();
        lightbulb = GameObject.FindGameObjectWithTag("lightBulb").GetComponent<Image>();
        handHold = GameObject.FindGameObjectWithTag("handHold").GetComponent<Image>();
        myRigidBody = GetComponent<Rigidbody2D>();
        EnableDisableUI(false);
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //player movement controls
        myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        if (myRigidBody.velocity.x < -0.1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            if (myRigidBody.velocity.y == 0)
            {
                anim.SetBool("walking", true);
            }
            
        }
        else if (myRigidBody.velocity.x > 0.1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            if (myRigidBody.velocity.y == 0)
            {
                anim.SetBool("walking", true);
            }
        }
        if ((myRigidBody.velocity.x > -0.1 && myRigidBody.velocity.x < 0.1) || myRigidBody.velocity.y > 0 || myRigidBody.velocity.y < 0)
        {
            //
            anim.SetBool("walking", false);
        }
        //anim.SetBool("walking", false);
        anim.SetFloat("ydirection", myRigidBody.velocity.y); //will play down animation if velocity is negative, play up anim if pos, play default if 0
                                                             //anim.SetFloat("xdirection", myRigidBody.velocity.x); //should it be .y or .x
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //trigger Pop-Up multiple choice questions in-game
        if (collision.gameObject.CompareTag("CS240"))
        {
            currentCourse = Course.CS240;
            //ConversationManager.Instance.StartConversation(CS240Dialogue);
        }
        if (collision.gameObject.CompareTag("CS231"))
        {
            currentCourse = Course.CS231;
            // ConversationManager.Instance.StartConversation(CS231Dialogue);
        }
        if (collision.gameObject.CompareTag("CS111"))
        {
            currentCourse = Course.CS111;
            //ConversationManager.Instance.StartConversation(CS111Dialogue);
        }
        if (collision.gameObject.CompareTag("CS304"))
        {
            currentCourse = Course.CS304;
            //  ConversationManager.Instance.StartConversation(CS304Dialogue);
        }
        if (collision.gameObject.CompareTag("MAS"))
        {
            currentCourse = Course.MAS;
            //  ConversationManager.Instance.StartConversation(MASDialogue);
        }

    }

    public void EnableDisableUI(bool onOff)
    {
        panelUI.enabled = onOff;
        lightbulb.enabled = !onOff;
        handHold.enabled = !onOff;
        questionText.enabled = onOff;
        answerOneText.enabled = onOff;
        answerTwoText.enabled = onOff;
        answerThreeText.enabled = onOff;
        answerFourText.enabled = onOff;
        popUPImage.GetComponent<Image>().enabled = onOff;
    }

}
