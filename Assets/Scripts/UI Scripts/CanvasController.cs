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
    LevelOneController eventTracker;
    Animator playerAnim;
    public AudioSource correctSound;
    AudioSource wrongSound;
    AudioSource mouseSound;
    GameObject gameOverPanel;

    private void Start()
    {
        lightbulbIcon = GameObject.FindGameObjectWithTag("lightBulb");
        handHoldIcon = GameObject.FindGameObjectWithTag("handHold");
        eventTracker = FindObjectOfType<LevelOneController>();
        playerAnim = FindObjectOfType<Animator>();
        correctSound = GetComponent<AudioSource>();
        wrongSound = FindObjectOfType<Camera>().GetComponent<AudioSource>();
        mouseSound = FindObjectOfType<PlayerController>().GetComponent<AudioSource>();
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");

        gameOverPanel.SetActive(false);
    }

    public void Update()
    {
        lightbulbIcon.GetComponent<Image>().sprite = lightbulbs[eventTracker.answeredCounter]; //inefficient bc you're assigning it every frame?
        handHoldIcon.GetComponent<Image>().sprite = handHolds[eventTracker.helpedCounter];
    }
    public void selectAnswer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.EnableDisableUI(false); //move inside coroutine
        // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_Text>().text == eventTracker.currentQuestion.correctAnswer) //how do i get this to reference the button i'm clicking
        {
            Debug.Log("correct");
            //do animation/particle system
            StartCoroutine(CorrectAnim());
            eventTracker.eventsCompleted++;
            eventTracker.answeredCounter++;

            //is there a cheap way to do this besides using another switch
            //want to know if you have completed specific coursework so we can chang eoutcome of NPC conversations
            switch (player.currentCourse)
            {
                case PlayerController.Course.CS111:
                    eventTracker.cs111Finished = true;
                    break;
                case PlayerController.Course.CS231:
                    eventTracker.cs231Finished = true;
                    break;
                case PlayerController.Course.CS304:
                    eventTracker.cs304Finished = true;
                    break;
                case PlayerController.Course.CS240:
                    eventTracker.cs240Finished = true;
                    break;
            }

        }
        else
        {
            wrongSound.Play();
            switch (player.currentCourse)
            {
                case PlayerController.Course.CS111:
                    if (eventTracker.questionsCS111.Count == 0)
                    {
                        gameOverPanel.SetActive(true);
                        Debug.Log("out of questions");
                        return;
                    }
                    break;
                case PlayerController.Course.CS231:
                    if (eventTracker.questionsCS231.Count == 0)
                    {
                        gameOverPanel.SetActive(true);
                        Debug.Log("out of questions");
                        return;
                    }
                    break;
                case PlayerController.Course.CS304:
                    if (eventTracker.questionsCS304.Count == 0)
                    {
                        gameOverPanel.SetActive(true);
                        Debug.Log("out of questions");
                        return;
                    }
                    break;
                case PlayerController.Course.CS240:
                    if (eventTracker.questionsCS240.Count == 0)
                    {
                        gameOverPanel.SetActive(true);
                        Debug.Log("out of questions");
                        return;
                    }
                    break;
                    //turn off quiz
            }
        }
    }

        private IEnumerator CorrectAnim()
        {
            playerAnim.SetBool("answeredQuestion", true);
            Debug.Log("correct answer");
            correctSound.Play();
            yield return new WaitForSeconds(2);
            playerAnim.SetBool("answeredQuestion", false);
        }
}
