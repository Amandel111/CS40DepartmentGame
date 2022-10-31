using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls player movement, animation, collision events, and quiz UI
/// </summary>
public class PlayerController : MonoBehaviour
{
    //declare variables
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
    private TMP_Text triesRemainingText;

    private Image lightbulb;
    private Image handHold;

    private Animator anim;
    public enum Course
    {
        CS240,
        CS111,
        CS231,
        CS304,
        MAS
    }

    void Start()
    {
        //initialize variables
        panelUI = FindObjectOfType<VerticalLayoutGroup>();
        popUPImage = GameObject.FindGameObjectWithTag("popUpImage");
        questionText = GameObject.FindGameObjectWithTag("question").GetComponent<TMP_Text>();
        answerOneText = GameObject.FindGameObjectWithTag("answerOne").GetComponent<TMP_Text>();
        answerTwoText = GameObject.FindGameObjectWithTag("answerTwo").GetComponent<TMP_Text>();
        answerThreeText = GameObject.FindGameObjectWithTag("answerThree").GetComponent<TMP_Text>();
        answerFourText = GameObject.FindGameObjectWithTag("answerFour").GetComponent<TMP_Text>();
        triesRemainingText = GameObject.FindGameObjectWithTag("triesRemainingText").GetComponent<TMP_Text>();
        lightbulb = GameObject.FindGameObjectWithTag("lightBulb").GetComponent<Image>();
        handHold = GameObject.FindGameObjectWithTag("handHold").GetComponent<Image>();
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        //quiz dialogue off unless conditions met
        EnableDisableUI(false);
    }

    void FixedUpdate()
    {
        //player movement controls
        myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        if (myRigidBody.velocity.x < -0.1)
        {
            //flip sprite to match walking direction
            GetComponent<SpriteRenderer>().flipX = true;
            if (myRigidBody.velocity.y == 0)
            {
                //set walking anim
                anim.SetBool("walking", true);
            }
            
        }
        else if (myRigidBody.velocity.x > 0.1)
        {
            //flip sprite to match walking direction
            GetComponent<SpriteRenderer>().flipX = false;
            if (myRigidBody.velocity.y == 0)
            {
                //set walking anim
                anim.SetBool("walking", true);
            }
        }
        if ((myRigidBody.velocity.x > -0.1 && myRigidBody.velocity.x < 0.1) || myRigidBody.velocity.y > 0 || myRigidBody.velocity.y < 0)
        {
            anim.SetBool("walking", false);
        }
        anim.SetFloat("ydirection", myRigidBody.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //set course player is currently attempting
        if (collision.gameObject.CompareTag("CS240"))
        {
            currentCourse = Course.CS240;
        }
        if (collision.gameObject.CompareTag("CS231"))
        {
            currentCourse = Course.CS231;
        }
        if (collision.gameObject.CompareTag("CS111"))
        {
            currentCourse = Course.CS111;
        }
        if (collision.gameObject.CompareTag("CS304"))
        {
            currentCourse = Course.CS304;
        }

    }

    /// <summary>
    /// controls quiz UI (buttons, text, background images)
    /// </summary>
    /// <param name="onOff"></param>
    public void EnableDisableUI(bool onOff)
    {
        triesRemainingText.enabled = onOff;
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
