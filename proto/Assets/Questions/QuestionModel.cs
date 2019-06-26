using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionModel
{
    public QuestionList[] questionList;
}

[System.Serializable]
public class QuestionList
{
    public int question_no;
    public string question;
    public QuestionChoices[] questionChoice;
    public string questionAnswer;
    public int questionScore;
}

[System.Serializable]
public class QuestionChoices
{

    public string choice1;
    public string choice2;
    public string choice3;
    public string choice4;

}

