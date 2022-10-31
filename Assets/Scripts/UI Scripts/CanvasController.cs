using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Controls in-game UI and quiz-calling
/// </summary>
public class CanvasController : MonoBehaviour
{
    //declare variables
    GameObject lightbulbIcon;
    GameObject handHoldIcon;
    [SerializeField] private Sprite[] lightbulbs;
    [SerializeField] private Sprite[] handHolds;
    SceneController eventTracker;
    Animator playerAnim;
    public AudioSource correctSound;
    AudioSource wrongSound;
    AudioSource mouseSound;
    GameObject gameOverPanel;

    private void Start()
    {
        //initialize variables
        lightbulbIcon = GameObject.FindGameObjectWithTag("lightBulb");
        handHoldIcon = GameObject.FindGameObjectWithTag("handHold");
        eventTracker = FindObjectOfType<SceneController>();
        playerAnim = FindObjectOfType<Animator>();
        correctSound = GetComponent<AudioSource>();
        wrongSound = FindObjectOfType<Camera>().GetComponent<AudioSource>();
        mouseSound = FindObjectOfType<PlayerController>().GetComponent<AudioSource>();
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");

        //only want gameOveerPanel UI to trigger if conditions met
        gameOverPanel.SetActive(false);
    }

    public void Update()
    {
        //set that scene's event tracker UI to reflect player's current progress
        lightbulbIcon.GetComponent<Image>().sprite = lightbulbs[eventTracker.answeredCounter];
        handHoldIcon.GetComponent<Image>().sprite = handHolds[eventTracker.helpedCounter];
    }

    /// <summary>
    /// Checks if player selects the correct answer or not and triggers appropriate events, also disabling UI
    /// </summary>
    public void selectAnswer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        //diasble UI
        player.EnableDisableUI(false);

        //if player gets question correct...
        if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_Text>().text == eventTracker.currentQuestion.correctAnswer) //how do i get this to reference the button i'm clicking
        {
            //trigger CorrectAnim coroutine
            StartCoroutine(CorrectAnim());

            //update SceneController's current memory of player progress
            eventTracker.eventsCompleted++;
            eventTracker.answeredCounter++;

            //updated SceneController's memory of which course's quiz has been completed
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
        //if player gets questions wrong...
        else
        {
            //player "wrong" audio
            wrongSound.Play();

            //If player is currently on currentCourse...
            switch (player.currentCourse)
            {
                //if that course has no questions left for the player to answer, game over event triggered
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
            }
        }
    }

    /// <summary>
    /// Triggers animation and music that playes if player gets a question correct
    /// </summary>
    /// <returns></returns>
        private IEnumerator CorrectAnim()
        {
            playerAnim.SetBool("answeredQuestion", true);
            Debug.Log("correct answer");
            correctSound.Play();
            yield return new WaitForSeconds(2);
            playerAnim.SetBool("answeredQuestion", false);
        }
}
