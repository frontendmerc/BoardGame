using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float diceSpeed = 0.3f;
    [SerializeField]
    private int diceIterate = 10;

    private Transform target;
    //endpoint = player's destination
    public int endPoint = 0;
    private int startPoint, tempEndPoint = 0;

    public static bool triger = false;
    private bool gateway = false;
    public static bool disableNext = true;

    public static bool passedStartpoint = false;
    public static bool passedStartpoint2 = false;

    public static bool playerArrow = false;

    private int dice;
    private Text diceText;

    public static bool actionHalt = true;
    private bool resumeBtnClick = false;

    private GameplayController gameplayController;
    public static bool runTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        diceText = GameObject.Find("DiceButtonText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        pauseKeyActive();
        if (actionHalt)
        {
            playerMoving();

            //disable space key if player object is still moving until it reach the end point
            if (disableNext)
            {
                //Debug.Log("111 " + disableNext);
                trigger();
                if (triger)
                {
                    StartCoroutine("Move");
                }
            }

            triger = false;
        }

    }

    void pauseKeyActive()
    {
        //pause
        if (actionHalt)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause(false);
            }
        }//unpause
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause(true);
            }
        }

        if (resumeBtnClick)
        {
            Pause(true);
            resumeBtnClick = false;
        }

    }

    private void Pause(bool pause)
    {
        GameplayController.pauseActive = pause;
        actionHalt = pause;
    }

    void trigger()
    {
        if (DiceButtonControl.rollDice)
        {
            triger = true;
            gateway = true;
            disableNext = false;
        }

    }


    private IEnumerator Move()
    {

        //execute for the first player at the first turn
        if (PlayerCount.starter)
        {
            PlayerController.currentPlayer++;

        }
        for (int i = 0; i < diceIterate; i++)
        {
            dice = Random.Range(1, 7);
            //dice = 13;
            diceText.text = dice.ToString();
            yield return new WaitForSeconds(diceSpeed);
        }

        startPoint = endPoint;
        endPoint += dice;
        tempEndPoint = endPoint; //unfilter endpoint

        //if the exceed the length of the waypoint, then reset to 0 and add the remaining moves
        if (endPoint >= Waypointer.points.Length)
        {
            endPoint -= Waypointer.points.Length;
            passedStartpoint = true;

        }

        runTrigger = true;

        Debug.Log("Current player: " + PlayerController.currentPlayer);
        Debug.Log("You rolled: " + dice);
        Debug.Log("Start Point: " + startPoint);
        Debug.Log("End Point: " + endPoint);

    }


    void playerMoving()
    {

        Vector3 dir = Waypointer.points[startPoint].position - transform.position;
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        //execute after player roll dice // player's move animation
        if (tempEndPoint > startPoint)
        {
            if (transform.position == Waypointer.points[startPoint].position)
            {
                startPoint++;
                if (startPoint == Waypointer.points.Length)
                {
                    startPoint = 0;
                    tempEndPoint = endPoint;
                }

                Debug.Log(startPoint + " < " + endPoint);
            }

        }

        //execute after player's animation is finsh // player's has reach the endpoint
        if ((transform.position == Waypointer.points[endPoint].position && gateway == true && QuestionController.questionAnswered == true)
            || (transform.position == Waypointer.points[endPoint].position && gateway == true && TrapController.trapIsActive2 == true)
            || passedStartpoint2)
        {

            //delete duplicates question and refill all the question after questionModelList's question is empty
            if (QuestionController.questionAnswered)
            {
                QuestionController.questionModelList.RemoveAt(QuestionController.randomQuestionValue);
                if (QuestionController.questionModelList.Count == 0)
                {
                    for (int i = 0; i < QuestionController.questionModel.questionList.Length; i++)
                    {
                        QuestionController.questionModelList.Add(QuestionController.questionModel.questionList[i]);

                    }
                }
            }

            playerArrow = true;

            PlayerController.tempLastEndpoint[PlayerController.currentPlayer - 1] = PlayerCount.player[PlayerController.currentPlayer - 1].GetComponent<PlayersMove>().endPoint;

            if (PlayerController.currentPlayer >= PlayerCount.player.Length)
            {
                PlayerController.currentPlayer = 0;
                GameplayController.gameTurn++;
            }

            PlayerController.getTurn(PlayerController.currentPlayer);

            PlayerController.currentPlayer++;

            PlayerCount.starter = false;
            passedStartpoint2 = false;
            gateway = false;
            disableNext = true;

            QuestionController.questionAnswered = false;
            QuestionController.answerSelected = false;
            TrapController.trapIsActive2 = false;

            DiceButtonControl.rollDice = false;

            
        }

    }

    public void ResumeButton()
    {
        resumeBtnClick = true;
        pauseKeyActive();
    }
}
