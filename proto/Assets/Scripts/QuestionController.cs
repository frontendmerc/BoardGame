using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public static QuestionModel questionModel;
    public static List<QuestionList> questionModelList;
    public static int randomQuestionValue;
    public static bool questionAnswered = false;
    public static bool questionIdle = false;
    public static int questionScore;
    public static bool answerSelected = false;
    public static bool questionExpired = false;

    [SerializeField]
    public Text questionText;
    [SerializeField]
    public Text choice1, choice2, choice3, choice4, questionDifficulty;

    public GameObject[] questionChoice;
    private int correctAnswer;
    private string questionAnswer;

    private void Awake()
    {
        //Get questions ffrom json file
        string jsonFilePath = File.ReadAllText(Application.dataPath + ButtonScript.questionSet);
        questionModel = JsonUtility.FromJson<QuestionModel>(jsonFilePath);

    }

    private void Start()
    {
        questionModelList = new List<QuestionList>(questionModel.questionList.Length);
        for (int i = 0; i < questionModel.questionList.Length; i++)
        {
            questionModelList.Add(questionModel.questionList[i]);
        }
    }

    private void Update()
    {
        if(answerSelected || questionExpired)
        {
            ShowCorrectAnswer();
        }
        else
        {
            DisableCorrectAnswer();
        }
    }

    public void getQuestion()
    {

        randomQuestionValue = Random.Range(0, questionModelList.Count);

        questionText.GetComponent<Text>().text = questionModelList[randomQuestionValue].question;

        choice1.GetComponent<Text>().text = questionModelList[randomQuestionValue].questionChoice[0].choice1;
        choice2.GetComponent<Text>().text = questionModelList[randomQuestionValue].questionChoice[0].choice2;
        choice3.GetComponent<Text>().text = questionModelList[randomQuestionValue].questionChoice[0].choice3;
        choice4.GetComponent<Text>().text = questionModelList[randomQuestionValue].questionChoice[0].choice4;

        questionAnswer = questionModelList[randomQuestionValue].questionAnswer ;

        questionDifficulty.GetComponent<Text>().text = questionModelList[randomQuestionValue].questionScore.ToString();

        questionIdle = true;

    }

    void DisableCorrectAnswer()
    {
        for (int i = 0; i < questionChoice.Length; i++)
        {
            questionChoice[i].GetComponent<Button>().interactable = true;
        }
    }

    void ShowCorrectAnswer()
    {
        ColorBlock color;

        for (int i = 0; i < questionChoice.Length; i++)
        {
            questionChoice[i].GetComponent<Button>().interactable = false;
            color = questionChoice[i].GetComponent<Button>().colors;
            color.disabledColor = Color.red;
            questionChoice[i].GetComponent<Button>().colors = color;
        }

        if (questionModelList[randomQuestionValue].questionChoice[0].choice1 == questionModelList[randomQuestionValue].questionAnswer)
        {
            correctAnswer = 1;

        }else if (questionModelList[randomQuestionValue].questionChoice[0].choice2 == questionModelList[randomQuestionValue].questionAnswer)
        {
            correctAnswer = 2;
        }
        else if (questionModelList[randomQuestionValue].questionChoice[0].choice3 == questionModelList[randomQuestionValue].questionAnswer)
        {
            correctAnswer = 3;
        }
        else if (questionModelList[randomQuestionValue].questionChoice[0].choice4 == questionModelList[randomQuestionValue].questionAnswer)
        {
            correctAnswer = 4;
        }

        switch (correctAnswer)
        {
            case 1:
                color = questionChoice[0].GetComponent<Button>().colors;
                color.disabledColor = Color.green;
                questionChoice[0].GetComponent<Button>().colors = color;

                break;
            case 2:
                color = questionChoice[1].GetComponent<Button>().colors;
                color.disabledColor = Color.green;
                questionChoice[1].GetComponent<Button>().colors = color;

                break;
            case 3:
                color = questionChoice[2].GetComponent<Button>().colors;
                color.disabledColor = Color.green;
                questionChoice[2].GetComponent<Button>().colors = color;

                break;
            case 4:
                color = questionChoice[3].GetComponent<Button>().colors;
                color.disabledColor = Color.green;
                questionChoice[3].GetComponent<Button>().colors = color;

                break;
        }
            
                
    }

    public void selectChoice1()
    {
        if (choice1.text == questionAnswer)
        {
            if (PlayerCount.starter)
            {
                Debug.Log("Corrext");
                PlayerController.playerScore1[0] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player0Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[0].ToString();

                //PlayerCount.starter = false;
                GameObject.Find("QuestionScene").SetActive(false);
                PlayerController.currentPlayer = 1;
            }
            else
            {
                Debug.Log("Corrext");
                if (PlayersMove.passedStartpoint)
                {
                    PlayerController.playerScore1[PlayerController.currentPlayer - 1] += GameObject.Find("GameControl").GetComponent<GameplayController>().passedStartpointScore;
                    PlayersMove.passedStartpoint = false;
                    PlayersMove.passedStartpoint2 = true;
                }
                PlayerController.playerScore1[PlayerController.currentPlayer - 1] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player" + (PlayerController.currentPlayer - 1) + "Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[PlayerController.currentPlayer - 1].ToString();

            }

        }
        else
        {
            Debug.Log("False");
        }

        answerSelected = true;
        questionIdle = false;
    }

    public void selectChoice2()
    {
        if (choice2.text == questionAnswer)
        {
            if (PlayerCount.starter)
            {
                Debug.Log("Corrext");
                PlayerController.playerScore1[0] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player0Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[0].ToString();

                //PlayerCount.starter = false;
                GameObject.Find("QuestionScene").SetActive(false);
                PlayerController.currentPlayer = 1;
            }
            else
            {
                Debug.Log("Corrext");
                if (PlayersMove.passedStartpoint)
                {
                    PlayerController.playerScore1[PlayerController.currentPlayer - 1] += GameObject.Find("GameControl").GetComponent<GameplayController>().passedStartpointScore;
                    PlayersMove.passedStartpoint = false;
                    PlayersMove.passedStartpoint2 = true;
                }
                PlayerController.playerScore1[PlayerController.currentPlayer - 1] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player" + (PlayerController.currentPlayer - 1) + "Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[PlayerController.currentPlayer - 1].ToString();

            }
        }
        else
        {
            Debug.Log("False");
        }

        answerSelected = true;
        questionIdle = false;
    }

    public void selectChoice3()
    {
        if (choice3.text == questionAnswer)
        {
            if (PlayerCount.starter)
            {
                Debug.Log("Corrext");
                PlayerController.playerScore1[0] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player0Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[0].ToString();

                //PlayerCount.starter = false;
                GameObject.Find("QuestionScene").SetActive(false);
                PlayerController.currentPlayer = 1;
            }
            else
            {
                Debug.Log("Corrext");
                if (PlayersMove.passedStartpoint)
                {
                    PlayerController.playerScore1[PlayerController.currentPlayer - 1] += GameObject.Find("GameControl").GetComponent<GameplayController>().passedStartpointScore;
                    PlayersMove.passedStartpoint = false;
                    PlayersMove.passedStartpoint2 = true;
                }
                PlayerController.playerScore1[PlayerController.currentPlayer - 1] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player" + (PlayerController.currentPlayer - 1) + "Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[PlayerController.currentPlayer - 1].ToString();

            }
        }
        else
        {
            Debug.Log("False");
        }

        answerSelected = true;
        questionIdle = false;
    }

    public void selectChoice4()
    {
        if (choice4.text == questionAnswer)
        {
            if (PlayerCount.starter)
            {
                Debug.Log("Corrext");
                PlayerController.playerScore1[0] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player0Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[0].ToString();

                //PlayerCount.starter = false;
                GameObject.Find("QuestionScene").SetActive(false);
                PlayerController.currentPlayer = 1;
            }
            else
            {
                Debug.Log("Corrext");
                if (PlayersMove.passedStartpoint)
                {
                    PlayerController.playerScore1[PlayerController.currentPlayer - 1] += GameObject.Find("GameControl").GetComponent<GameplayController>().passedStartpointScore;
                    PlayersMove.passedStartpoint = false;
                    PlayersMove.passedStartpoint2 = true;
                }
                PlayerController.playerScore1[PlayerController.currentPlayer - 1] += questionModelList[randomQuestionValue].questionScore;
                GameObject getText = GameObject.Find("player" + (PlayerController.currentPlayer - 1) + "Score");
                getText.GetComponent<Text>().text = PlayerController.playerScore1[PlayerController.currentPlayer - 1].ToString();

            }
        }
        else
        {
            Debug.Log("False");
        }

        answerSelected = true;
        questionIdle = false;
    }

}
