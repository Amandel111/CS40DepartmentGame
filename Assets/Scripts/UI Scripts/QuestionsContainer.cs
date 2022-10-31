[System.Serializable]

/// <summary>
/// Template for each course's quiz questions and answers
/// </summary>
public class QuestionsContainer
{
    //question/answers node 
    public string question;
    public string answerOne;
    public string answerTwo;
    public string answerThree;
    public string answerFour;
    public string correctAnswer;
    public string classSubjectTrigger; //depending on subject want to activate different dialogue triggers
}
