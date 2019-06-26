using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class WorkingCode : MonoBehaviour
{

    [SerializeField]
    private int trapEndRange = 3;

    private int[] trapTiles, questionTiles;

    private List<int> listTiles;
    private List<int> trapTileList, questionTileList;

    private Transform player; //get player
    public GameObject panelScene;
    public GameObject trapPanelScene;

    [SerializeField]
    private GameObject questionControllerScript;
    [SerializeField]
    private GameObject trapControllerScript;

    public static int gameTurn = 0;
    [SerializeField]
    private Text gameTurnText;
    [SerializeField]
    private int setWinGameTurn;
    [SerializeField]
    private GameObject endGamePanel;
    [SerializeField]
    private GameObject endGamePanel2;

    public float timeCountdown = 1f;
    public float timeCountdown2 = 1f;

    private float trapAppreanceTime = 2f;

    private void Awake()
    {

        //initialize the tiles before the game starts
        getTrapTiles();
        getQuestionTiles();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameTurnText.GetComponent<Text>().text = gameTurn.ToString();

        //if (gameTurn == setWinGameTurn && QuestionController.questionAnswered == true)
        //{
        //    panelScene.SetActive(false);
        //    trapPanelScene.SetActive(false);
        //    if (timeCountdown < 0)
        //    {
        //        endGamePanel.SetActive(true);
        //        if (timeCountdown2 < 0)
        //        {
        //            endGamePanel.SetActive(false);
        //            endGamePanel2.SetActive(true);
        //        }
        //        timeCountdown2 -= Time.deltaTime;
        //    }

        //    timeCountdown -= Time.deltaTime;

        //}
        //else
        //{
        //    //active question scene upon land at question tiles
        //    questionTileActive();
        //    //active trap scene upon land at trap tiles
        //    trapTileActive();
        //}

        //active question scene upon land at question tiles
        questionTileActive();
        //active trap scene upon land at trap tiles
        //trapTileActive();


    }

    //get trap tiles in random value
    void getTrapTiles()
    {
        trapTiles = new int[trapEndRange];

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
                if (PlayerCount.player[0].transform.position == Waypointer.points[PlayerCount.player[0].GetComponent<PlayersMove>().endPoint].position && QuestionController.questionAnswered == false)
                {
                    //check if the question is still active,,,it will always get question first only set panel to active
                    if (QuestionController.questionIdle)
                    {

                        panelScene.SetActive(true);
                    }
                    else
                    {

                        questionControllerScript.GetComponent<QuestionController>().getQuestion();
                    }

                }
                else
                {
                    panelScene.SetActive(false);

                }
            }
            else if (trapTileList.Contains(PlayerCount.player[0].GetComponent<PlayersMove>().endPoint))
            {
                if (PlayerCount.player[0].transform.position == Waypointer.points[PlayerCount.player[0].GetComponent<PlayersMove>().endPoint].position && TrapController.trapIsActive == false
                    && TrapController.trapIsActive2 == false)
                {
                    Debug.Log("Trap Active");
                    trapPanelScene.SetActive(true);
                    trapControllerScript.GetComponent<TrapController>().trapTrigger();

                }
                else
                {
                    if (trapAppreanceTime < 0)
                    {
                        trapPanelScene.SetActive(false);
                        TrapController.trapIsActive = false;
                        TrapController.trapIsActive = true;
                        trapAppreanceTime = 2f;
                    }
                    trapAppreanceTime -= Time.deltaTime;

                }
            }
            else //start point
            {

            }
        }
        else
        {
            if (PlayerController.currentPlayer == 0)
            {
                if (questionTileList.Contains(PlayerCount.player[PlayerController.currentPlayer].GetComponent<PlayersMove>().endPoint))
                {
                    //Debug.Log("2 Current Player: " + PlayerController.currentPlayer);
                    // check if player is in the tile position and if the question is answered
                    if (PlayerCount.player[PlayerController.currentPlayer].transform.position == Waypointer.points[PlayerCount.player[PlayerController.currentPlayer].GetComponent<PlayersMove>().endPoint].position
                        && QuestionController.questionAnswered == false)
                    {
                        //check if the question is still active,,,it will always get question first only set panel to active
                        if (QuestionController.questionIdle)
                        {
                            panelScene.SetActive(true);
                        }
                        else
                        {
                            questionControllerScript.GetComponent<QuestionController>().getQuestion();
                        }

                    }
                    else
                    {
                        panelScene.SetActive(false);
                    }

                }
                else if (trapTileList.Contains(PlayerCount.player[PlayerController.currentPlayer].GetComponent<PlayersMove>().endPoint))
                {
                    if (PlayerCount.player[PlayerController.currentPlayer].transform.position == Waypointer.points[PlayerCount.player[PlayerController.currentPlayer].GetComponent<PlayersMove>().endPoint].position
                        && TrapController.trapIsActive == false)
                    {
                        Debug.Log("Trap Active");
                        trapPanelScene.SetActive(true);
                        trapControllerScript.GetComponent<TrapController>().trapTrigger();

                    }
                    else
                    {
                        trapPanelScene.SetActive(false);
                    }
                }
                else
                {

                }
            }
            else
            {
                //check if player land on question tile
                if (questionTileList.Contains(PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint))
                {
                    //Debug.Log("2 Current Player: " + PlayerController.currentPlayer);
                    // check if player is in the tile position and if the question is answered
                    if (PlayerCount.player[PlayerController.currentPlayer - 1].transform.position == Waypointer.points[PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint].position
                        && QuestionController.questionAnswered == false)
                    {
                        //check if the question is still active,,,it will always get question first only set panel to active
                        if (QuestionController.questionIdle)
                        {
                            panelScene.SetActive(true);
                        }
                        else
                        {
                            questionControllerScript.GetComponent<QuestionController>().getQuestion();
                        }

                    }
                    else
                    {
                        panelScene.SetActive(false);
                    }

                }
                else if (trapTileList.Contains(PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint))
                {
                    if (PlayerCount.player[PlayerController.currentPlayer - 1].transform.position == Waypointer.points[PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint].position
                        && TrapController.trapIsActive == false)
                    {
                        Debug.Log("Trap Active");
                        trapPanelScene.SetActive(true);
                        trapControllerScript.GetComponent<TrapController>().trapTrigger();

                    }
                    else
                    {
                        trapPanelScene.SetActive(false);
                    }
                }
                else
                {

                }
            }

        }

    }

    void trapTileActive()
    {
        for (int i = 0; i < trapTiles.Length; i++)
        {
            if (PlayerCount.starter)
            {
                if (PlayerCount.player[0].GetComponent<PlayersMove>().endPoint == trapTiles[i])
                {
                    if (PlayerCount.player[0].transform.position == Waypointer.points[trapTiles[i]].position && TrapController.trapIsActive == false)
                    {
                        Debug.Log("Trap Active");
                        trapPanelScene.SetActive(true);
                        trapControllerScript.GetComponent<TrapController>().trapTrigger();

                    }
                    else
                    {
                        trapPanelScene.SetActive(false);
                    }

                }
            }
            else
            {
                if (PlayerController.currentPlayer == 0)
                {
                    if (PlayerCount.player[PlayerController.currentPlayer].GetComponent<PlayersMove>().endPoint == trapTiles[i])
                    {
                        if (PlayerCount.player[PlayerController.currentPlayer].transform.position == Waypointer.points[trapTiles[i]].position && TrapController.trapIsActive == false)
                        {
                            Debug.Log("Trap Active");
                            trapPanelScene.SetActive(true);
                            trapControllerScript.GetComponent<TrapController>().trapTrigger();

                        }
                        else
                        {
                            trapPanelScene.SetActive(false);
                            Debug.Log("Trap Deactive");
                        }

                    }
                }
                else
                {
                    if (PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint == trapTiles[i])
                    {
                        if (PlayerCount.player[PlayerController.currentPlayer - 1].transform.position == Waypointer.points[trapTiles[i]].position && TrapController.trapIsActive == false)
                        {
                            Debug.Log("Trap Active");
                            trapPanelScene.SetActive(true);
                            trapControllerScript.GetComponent<TrapController>().trapTrigger();

                        }
                        else
                        {
                            trapPanelScene.SetActive(false);
                            Debug.Log("Trap Deactive");
                        }

                    }
                }
            }

        }
    }

}
