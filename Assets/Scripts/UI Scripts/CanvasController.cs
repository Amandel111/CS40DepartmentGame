using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    /*
     * This class monitors the answer player selects in the quiz game and does different actions depending on if they're right or wrong
     * */

    GameObject lightbulbIcon;
    GameObject handHoldIcon;
    [SerializeField] private Sprite[] lightbulbs;
    [SerializeField] private Sprite[] handHolds;
    LevelOneController eventController;
    Animator playerAnim;

    private void Start()
    {
        lightbulbIcon = GameObject.FindGameObjectWithTag("lightBulb");
        handHoldIcon = GameObject.FindGameObjectWithTag("handHold");
        eventController = FindObjectOfType<LevelOneController>();
        playerAnim = FindObjectOfType<Animator>();
    }

    public void Update()
    {
        lightbulbIcon.GetComponent<Image>().sprite = lightbulbs[eventController.answeredCounter]; //inefficient bc you're assigning it every frame?
        handHoldIcon.GetComponent<Image>().sprite = handHolds[eventController.helpedCounter];

    }
    public void selectAnswer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(false); //move inside coroutine
        // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_Text>().text == eventController.currentQuestion.correctAnswer) //how do i get this to reference the button i'm clicking
        {
            Debug.Log("correct");
            //do animation/particle system
            //StartCoroutine(CorrectAnim());
            //turn off current dialogue 
            //fill in light bulb icon
            //make it so dialogue system pops up when interacting with teacher, no more questions
            //idea: just have sep level controller and ui for each scene instead of memory
            // ConversationManager.Instance.SetBool(eventController.currentQuestion.classSubjectTrigger, true);
            eventController.eventsCompleted++;
           // lightbulbIcon.GetComponent<Image>().sprite = lightbulbs[eventController.iconCounter];
            eventController.answeredCounter++;

            //is there a cheap way to do this besides using another switch
            //want to know if you have completed specific coursework so we can chang eoutcome of NPC conversations
            switch (player.currentCourse)
            {
                case PlayerController.Course.CS111:
                    eventController.cs111Finished = true;
                    break;
                case PlayerController.Course.CS231:
                    eventController.cs231Finished = true;
                    break;
                case PlayerController.Course.CS304:
                    eventController.cs304Finished = true;
                    break;
                case PlayerController.Course.CS240:
                    eventController.cs240Finished = true;
                    break;
            }

        }
        else
        {
            Debug.Log("incorrect");
            //turn off quiz
        }
    }

    /*
    private IEnumerator CorrectAnim()
    {
        playerAnim.SetBool("correctAnim", true);
        yield return new WaitForSeconds(2);
        playerAnim.SetBool("correctAnim", false);
    }
    */
}
