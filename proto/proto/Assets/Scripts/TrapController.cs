using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapController : MonoBehaviour
{
    private QuestionModel questionModel;
    public static bool trapIdle = false;
    public static bool trapIsActive = false;
    public static bool trapIsActive2 = false;
    public GameObject trapPanelScene;

    public void trapTrigger()
    {

        //int randomTrapValue = Random.Range(1, 10);
        int randomTrapValue = 1;

        if (PlayerCount.starter)
        {
            PlayerController.playerScore1[0] -= randomTrapValue;
            GameObject getText = GameObject.Find("player0Score");
            getText.GetComponent<Text>().text = PlayerController.playerScore1[0].ToString();

            PlayerCount.starter = false;
            PlayerController.currentPlayer = 1;
        }
        else
        {

            Debug.Log("Corrext");
            if (PlayersMove.passedStartpoint)
            {
                PlayerController.playerScore1[PlayerController.currentPlayer - 1] += GameObject.Find("GameControl").GetComponent<GameplayController>().passedStartpointScore;
                PlayersMove.passedStartpoint = false;

            }
            PlayerController.playerScore1[PlayerController.currentPlayer - 1] -= randomTrapValue;
            GameObject getText = GameObject.Find("player" + (PlayerController.currentPlayer - 1) + "Score");
            getText.GetComponent<Text>().text = PlayerController.playerScore1[PlayerController.currentPlayer - 1].ToString();


        }

        trapIsActive = true;

    }

}
