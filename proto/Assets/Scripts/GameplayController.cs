using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


/*
 
    - Get the index of Question and Trap tiles 
    - Control the win condition of the game
    - Control which tile to active and close
     
*/

public class GameplayController : MonoBehaviour
{

    /*
     * Controller > Game Control:
            (deprecate)Trap End Rnage = configure number of trap
            (deprecate)Set Win Game Turn = configure turn to end a game
            Question Timer = set the period of time for a question to appear
            Passed Startpoint Score = set point of player getting after pass the startpoint
            Trap Appreance Time = set trap screen appear time
            Question Feedback TIme = set the time of question's answer shown to player

        Players:
            (deprecate)Player Size = configure the number player 

        Assets > Prefabs > Player > Player Move Speed:
            Speed = Configure the move speed of all player,, higher speed, move faster
            Dice Speed = COnfigure dice speed
            Dice Iterate = Configure how many turn dice need to roll
        
         
         */

    private int[] trapTiles, questionTiles;
    private List<int> listTiles;
    public static List<int> trapTileList, questionTileList;

    [SerializeField]
    private int trapEndRange = 3;
    [SerializeField]
    private GameObject questionControllerScript;
    [SerializeField]
    private GameObject trapControllerScript;
    [SerializeField]
    private Text gameTurnText;
    [SerializeField]
    private int setWinGameTurn;
    [SerializeField]
    private GameObject endGamePanel;
    [SerializeField]
    private GameObject endGamePanel2;
    [SerializeField]
    private float timeCountdown = 1f;
    [SerializeField]
    private float timeCountdown2 = 1f;
    [SerializeField]
    private float trapAppreanceTime = 2f;
    [SerializeField]
    private float questionTimer = 10f;
    [SerializeField]
    private Text questionTimerText;

    public int passedStartpointScore = 10;
    public static int gameTurn = 0;
    public GameObject panelScene;
    public GameObject trapPanelScene;

    private float questionTimerReplacement;
    private float trapAppreanceTimeReplacement;

    public GameObject pausePanel;
    public static bool pauseActive = true;

    [SerializeField]
    private float questionFeedbackTimer = 3f;

    private float questionFeedbackTimerReplacement;

    private void Awake()
    {
        //initialize the tiles before the game starts
        getTrapTiles();
        getQuestionTiles();
    }

    // Start is called before the first frame update
    void Start()
    {
        questionTimerReplacement = questionTimer;
        trapAppreanceTimeReplacement = trapAppreanceTime;
        questionFeedbackTimerReplacement = questionFeedbackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //set game turn text
        gameTurnText.GetComponent<Text>().text = gameTurn.ToString();

        //check to see if the game turn is end after either question or trap scene is closed
        if (gameTurn == ButtonScript.gameTurn)
        {
            panelScene.SetActive(false);
            trapPanelScene.SetActive(false);
            if (timeCountdown < 0)
            {
                endGamePanel.SetActive(true);
                if (timeCountdown2 < 0)
                {
                    PlayerController.endGameTrigger = true;
                    endGamePanel.SetActive(false);
                    endGamePanel2.SetActive(true);
                }
                timeCountdown2 -= Time.deltaTime;
            }

            timeCountdown -= Time.deltaTime;

        }
        else
        {
            //active question scene upon land at question tiles
            questionTileActive();
        }

        //unpause,, close pause panel
        if (pauseActive)
        {
            pausePanel.SetActive(false);
            PlayersMove.actionHalt = true; //set to true,, for some fucknig reason, it automatically set back to false,, dogshit,, waste my 1 hour because of this bs scrript,, im so fuxikkng done
        }//pause,, open pause panel
        else
        {
            pausePanel.SetActive(true);
        }
    }

    //get trap tiles in random value
    void getTrapTiles()
    {
        trapTiles = new int[ButtonScript.trapSize];

        listTiles = new List<int>(Waypointer.points.Length);

        trapTileList = new List<int>(trapTiles.Length);

        for (int i = 0; i < listTiles.Capacity; i++)
        {
            listTiles.Add(i);
        }

        listTiles.RemoveAt(0); //remove start point from the list

        //eliminate duplicates
        for (int x = 0; x < trapTiles.Length; x++)
        {
            int numberPlceHolder = Random.Range(1, listTiles.Count);
            trapTiles[x] = listTiles[numberPlceHolder];
            listTiles.RemoveAt(numberPlceHolder);

            trapTileList.Add(trapTiles[x]);
            Debug.Log("Trap Tiles: " + trapTiles[x]);
        }
    }

    //get question tiles by the remaining tiles after trap tiles
    void getQuestionTiles()
    {
        questionTiles = new int[listTiles.Count];

        questionTileList = new List<int>(listTiles.Count);

        for (int i = 0; i < listTiles.Count; i++)
        {
            questionTiles[i] = listTiles[i];

            questionTileList.Add(listTiles[i]);
        }

    }

    void questionTileActive()
    {

        //only do at the first time
        if (PlayerCount.starter)
        {
            //check if player land on question tile
            if (questionTileList.Contains(PlayerCount.player[0].GetComponent<PlayersMove>().endPoint))
            {
                // check if player is in the tile position and if the question is answered
                if (PlayerCount.player[0].transform.position == Waypointer.points[PlayerCount.player[0].GetComponent<PlayersMove>().endPoint].position
                    && QuestionController.questionAnswered == false && QuestionController.answerSelected == false)
                {
                    PlayersMove.runTrigger = false;
                    //check if the question is still active,,,it will always get question first only set panel to active
                    if (QuestionController.questionIdle)
                    {
                        panelScene.SetActive(true);
                        if (questionTimer < 0)
                        {
                            QuestionController.questionExpired = true;
                            if (questionFeedbackTimer < 0)
                            {
                                QuestionController.questionAnswered = true;
                                QuestionController.questionExpired = false;
                                panelScene.SetActive(false);
                                questionTimer = questionTimerReplacement; //reset the question timer to default timer
                                questionFeedbackTimer = questionFeedbackTimerReplacement;
                            }
                            questionFeedbackTimer -= Time.deltaTime;
                        }
                        questionTimerText.text = ((int)questionTimer).ToString();
                        questionTimer -= Time.deltaTime;

                    }
                    else
                    {
                        questionControllerScript.GetComponent<QuestionController>().getQuestion();
                    }

                }//when question is closed
                else
                {
                    //when the answer in question panel is selected,, after an answer is selected, it show user the correct answer in x seconds
                    if(QuestionController.questionAnswered == false && QuestionController.answerSelected == true)
                    {
                        questionTimer = questionTimerReplacement; //reset the question timer to default timer

                        if (questionFeedbackTimer < 0)
                        {
                            QuestionController.questionAnswered = true;
                            panelScene.SetActive(false);
                            questionTimer = questionTimerReplacement; //reset the question timer to default timer
                            questionFeedbackTimer = questionFeedbackTimerReplacement;
                        }

                        questionFeedbackTimer -= Time.deltaTime;
                    }
                }
            }
            else if (trapTileList.Contains(PlayerCount.player[0].GetComponent<PlayersMove>().endPoint))
            {
                if (PlayerCount.player[0].transform.position == Waypointer.points[PlayerCount.player[0].GetComponent<PlayersMove>().endPoint].position && TrapController.trapIsActive == false
                    && TrapController.trapIsActive2 == false)
                {
                    PlayersMove.runTrigger = false;
                    Debug.Log("Trap Active");
                    trapPanelScene.SetActive(true);
                    trapControllerScript.GetComponent<TrapController>().trapTrigger();

                }
                else
                {
                    if (TrapController.trapIsActive)
                    {
                        if (trapAppreanceTime < 0)
                        {
                            trapPanelScene.SetActive(false);
                            TrapController.trapIsActive = false;
                            TrapController.trapIsActive2 = true;
                            trapAppreanceTime = trapAppreanceTimeReplacement;
                        }
                        trapAppreanceTime -= Time.deltaTime;
                    }

                }
            }
            else //start point
            {

            }
        }
        else
        {
            //check if player land on question tile
            if (questionTileList.Contains(PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint)
                && PlayerController.tempLastEndpoint[PlayerController.currentPlayer - 1] != PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint)
            {
                // check if player is in the tile position and if the question is answered
                if (PlayerCount.player[PlayerController.currentPlayer - 1].transform.position == Waypointer.points[PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint].position
                    && QuestionController.questionAnswered == false && QuestionController.answerSelected == false)
                {
                    PlayersMove.runTrigger = false;
                    //check if the question is still active,,,it will always get question first only set panel to active
                    if (QuestionController.questionIdle)
                    {
                        panelScene.SetActive(true);
                        if (questionTimer < 0)
                        {
                            QuestionController.questionExpired = true;
                            if (questionFeedbackTimer < 0)
                            {
                                QuestionController.questionAnswered = true;
                                QuestionController.questionExpired = false;
                                panelScene.SetActive(false);
                                questionTimer = questionTimerReplacement; //reset the question timer to default timer
                                questionFeedbackTimer = questionFeedbackTimerReplacement;
                            }

                            questionFeedbackTimer -= Time.deltaTime;
                        }
                        questionTimerText.text = ((int)questionTimer).ToString();
                        questionTimer -= Time.deltaTime;
                    }
                    else
                    {
                        questionControllerScript.GetComponent<QuestionController>().getQuestion();
                    }
                }
                else
                {
                    if (QuestionController.questionAnswered == false && QuestionController.answerSelected == true)
                    {
                        questionTimer = questionTimerReplacement; //reset the question timer to default timer

                        if (questionFeedbackTimer < 0)
                        {
                            QuestionController.questionAnswered = true;
                            panelScene.SetActive(false);
                            questionTimer = questionTimerReplacement; //reset the question timer to default timer
                            questionFeedbackTimer = questionFeedbackTimerReplacement;
                        }

                        questionFeedbackTimer -= Time.deltaTime;
                    }
                }

            }//check if player lands on trap tile
            else if (trapTileList.Contains(PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint)
                && PlayerController.tempLastEndpoint[PlayerController.currentPlayer - 1] != PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint)
            {
                if (PlayerCount.player[PlayerController.currentPlayer - 1].transform.position == Waypointer.points[PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint].position
                    && TrapController.trapIsActive == false && TrapController.trapIsActive2 == false)
                {
                    PlayersMove.runTrigger = false;
                    Debug.Log("Trap Active");
                    trapPanelScene.SetActive(true);
                    trapControllerScript.GetComponent<TrapController>().trapTrigger();
                }
                else
                {
                    if (TrapController.trapIsActive)
                    {
                        if (trapAppreanceTime < 0)
                        {
                            trapPanelScene.SetActive(false);
                            TrapController.trapIsActive = false;
                            TrapController.trapIsActive2 = true;
                            trapAppreanceTime = trapAppreanceTimeReplacement;
                            PlayersMove.passedStartpoint2 = true;
                        }
                        trapAppreanceTime -= Time.deltaTime;
                    }
                }
            }
            else//start point
            {
                if (PlayerCount.player[PlayerController.currentPlayer - 1].transform.position == Waypointer.points[PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint].position
                    && PlayersMove.passedStartpoint)
                {
                    PlayersMove.runTrigger = false;
                    PlayerController.playerScore1[PlayerController.currentPlayer - 1] += passedStartpointScore;
                    GameObject getText = GameObject.Find("player" + (PlayerController.currentPlayer - 1) + "Score");
                    getText.GetComponent<Text>().text = PlayerController.playerScore1[PlayerController.currentPlayer - 1].ToString();
                    PlayersMove.passedStartpoint = false;
                    PlayersMove.passedStartpoint2 = true;
                }
            }

        }


    }




}
