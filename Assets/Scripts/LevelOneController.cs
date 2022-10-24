using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelOneController : MonoBehaviour
{
    public static LevelOneController Instance;

    /*
    * This class controls UI, both adding questions to UI popup screen and icons on popup tracking player progress, and stores memory for all scripts
    * */

    //lists of each class' questions
    public List<QuestionsContainer> questionsCS111;
    public List<QuestionsContainer> questionsCS231;
    public List<QuestionsContainer> questionsCS240;
    public List<QuestionsContainer> questionsCS304;

    public QuestionsContainer currentQuestion;

    //text objects that are in charge of our quiz UI
    private TMP_Text questionText;
    private TMP_Text answerOneText;
    private TMP_Text answerTwoText;
    private TMP_Text answerThreeText;
    private TMP_Text answerFourText;

    public int eventsCompleted;

    public int TOTAL_EVENTS = 8;
    private PlayerController player;

    //bools for checking whether we have taken various quizzes yet or not
    public bool cs240Finished;
    public bool cs304Finished;
    public bool cs111Finished;
    public bool cs231Finished;

    //bools for checking whether we have helped NPC yet or not
    public bool helpedCS111;
    public bool helpedCS231;
    public bool helpedCS304;
    public bool helpedCS240;

    //updates lightbulb UI
    public int answeredCounter = 0;
    public int helpedCounter = 0;

    //location spawning vars
    public static string previousScene;

    public bool hasReceivedAssignment; 

    private void Awake()
    {
        //keep object data in each scene
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //triggers.
        player = FindObjectOfType<PlayerController>();

        previousScene = SceneManager.GetActiveScene().name;
    }



    public void GetRandomQuestion(List<QuestionsContainer> currentArr)
    {
        /*
         * if (currentArray == NULL){
         * Game over screen 
         * return;
         * }
         * */
        player = FindObjectOfType<PlayerController>();
        //UI = FindObjectOfType<Canvas>();
        questionText = GameObject.FindGameObjectWithTag("question").GetComponent<TMP_Text>();
        answerOneText = GameObject.FindGameObjectWithTag("answerOne").GetComponent<TMP_Text>();
        //answerOneText = FindObjectsOfType<TMP_Text>()[2];
        answerTwoText = GameObject.FindGameObjectWithTag("answerTwo").GetComponent<TMP_Text>();
        answerThreeText = GameObject.FindGameObjectWithTag("answerThree").GetComponent<TMP_Text>();
        answerFourText = GameObject.FindGameObjectWithTag("answerFour").GetComponent<TMP_Text>();

        int randomQuestionIndex = UnityEngine.Random.Range(0, currentArr.Count);
        currentQuestion = currentArr[randomQuestionIndex];
        questionText.text = currentArr[randomQuestionIndex].question;
        answerOneText.text = currentArr[randomQuestionIndex].answerOne;
        answerTwoText.text = currentArr[randomQuestionIndex].answerTwo;
        answerThreeText.text = currentArr[randomQuestionIndex].answerThree;
        answerFourText.text = currentArr[randomQuestionIndex].answerFour;
        //currentQuestion.classSubjectTrigger = currentArr[randomQuestionIndex].classSubjectTrigger;
        //unansweredQuestion.RemoveAt(randomQuestionIndex);
        currentArr.RemoveAt(randomQuestionIndex);
    }

}
