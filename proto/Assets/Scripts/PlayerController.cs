using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int[] playerScore1;

    public static GameObject[] player;

    public static int currentPlayer = 0;

    public static int[] tempLastEndpoint;

    public static bool endGameTrigger = false;
    private bool endGameTrigger2 = true;

    // Start is called before the first frame update
    void Start()
    {
        playerScore1 = new int[PlayerCount.player.Length];
        tempLastEndpoint = new int[PlayerCount.player.Length];

        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            playerScore1[i] = 0;
            tempLastEndpoint[i] = 0;
        }

        playerInfoPanel();
    }

    private void Update()
    {

        if (PlayersMove.playerArrow)
        {

            Vector3 dir;
            if (currentPlayer == 0)
            {
                dir = GameObject.Find("turnArrow" + currentPlayer).transform.position - turnArrowImage.transform.position;
            }
            else
            {
                dir = GameObject.Find("turnArrow" + (currentPlayer - 1)).transform.position - turnArrowImage.transform.position;
            }

            turnArrowImage.transform.Translate(dir * 10 * Time.deltaTime, Space.Self);

            if (turnArrowImage.transform.position == GameObject.Find("turnArrow" + (currentPlayer - 1)).transform.position)
            {
                PlayersMove.playerArrow = false;
            }

        }

        if (endGameTrigger && endGameTrigger2)
        {
            displayFinalScore();
            endGameTrigger2 = false;
        }

    }


    public static void getTurn(int playerIndex)
    {
        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            PlayerCount.player[i].GetComponent<PlayersMove>().enabled = false;
            PlayerCount.player[i].GetComponent<RunAnimation>().enabled = false;
        }

        PlayerCount.player[playerIndex].GetComponent<PlayersMove>().enabled = true;
        PlayerCount.player[playerIndex].GetComponent<RunAnimation>().enabled = true;
    }

    [SerializeField]
    private Text playerIdentityPerfab;
    [SerializeField]
    private Transform playerIdentityPosition;
    [SerializeField]
    private Transform playerIdentityParent;

    [SerializeField]
    private Transform playerScoreParent;
    [SerializeField]
    private Transform playerScorePosition;

    [SerializeField]
    private Transform turnArrowParent;
    [SerializeField]
    private Transform turnArrowPosition;
    [SerializeField]
    private Transform turnArrowPrefabs;

    [SerializeField]
    private Image turnArrowImage;

    //set player info text
    void playerInfoPanel()
    {
        Text playerIdentity;
        Transform turnArrow;
        int positionY = 0;
        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            playerIdentity = Instantiate(playerIdentityPerfab, new Vector3(playerIdentityPosition.position.x,
                playerIdentityPosition.position.y - positionY,
                playerIdentityPosition.position.z), Quaternion.identity) as Text;
            playerIdentity.name = "player" + (i);
            playerIdentity.text = "Player " + (i);
            playerIdentity.transform.parent = playerIdentityParent;

            positionY += 50;
        }

        positionY = 0;

        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            playerIdentity = Instantiate(playerIdentityPerfab, new Vector3(playerScorePosition.position.x,
                playerScorePosition.position.y - positionY,
                playerScorePosition.position.z), Quaternion.identity) as Text;
            playerIdentity.name = "player" + (i) + "Score";
            playerIdentity.text = 0.ToString();
            playerIdentity.transform.parent = playerScoreParent;

            positionY += 50;
        }

        positionY = 0;

        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            turnArrow = Instantiate(turnArrowPrefabs,
                new Vector3(turnArrowPosition.position.x, turnArrowPosition.position.y - positionY, turnArrowPosition.position.z),
                Quaternion.identity);

            turnArrow.name = "turnArrow" + (i);
            turnArrow.parent = turnArrowParent;

            positionY += 50;
        }

        turnArrowImage.transform.position = GameObject.Find("turnArrow0").transform.position;

    }

    [SerializeField]
    private Transform playerFinalScoreIdentityPosition;
    [SerializeField]
    private Transform playerFinalScorePosition;
    [SerializeField]
    private Transform winnerPosition;

    [SerializeField]
    private Image winnerCrown;

    [SerializeField]
    private GameObject winnerPanel;

    public void displayFinalScore()
    {

        int positionY = 0;
        Text playerIdentity;
        Transform winner;
        GameObject panel;

        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            playerIdentity = Instantiate(playerIdentityPerfab,
                new Vector3(playerFinalScoreIdentityPosition.position.x,
                playerFinalScoreIdentityPosition.position.y - positionY,
                playerFinalScoreIdentityPosition.position.z),
                Quaternion.identity) as Text;
            playerIdentity.name = "player Score" + (i);
            playerIdentity.text = playerScore1[i].ToString();
            playerIdentity.transform.parent = playerFinalScoreIdentityPosition;

            positionY += 50;
        }

        positionY = 0;

        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            playerIdentity = Instantiate(playerIdentityPerfab,
                new Vector3(playerFinalScorePosition.position.x,
                playerFinalScorePosition.position.y - positionY,
                playerFinalScorePosition.position.z),
                Quaternion.identity) as Text;
            playerIdentity.name = "Score" + (i);
            playerIdentity.text = "Player " + (i);
            playerIdentity.transform.parent = playerFinalScorePosition;

            positionY += 50;
        }

        positionY = 0;

        for (int i = 0; i < PlayerCount.player.Length; i++)
        {
            winner = Instantiate(turnArrowPrefabs,
                new Vector3(winnerPosition.position.x,
                winnerPosition.position.y - positionY,
                winnerPosition.position.z),
                Quaternion.identity);
            winner.name = "Winner" + (i);
            winner.transform.parent = winnerPosition;

            positionY += 50;
        }

        List<int> playerScoreList2 = new List<int>();
        List<int> playerScoreList = playerScore1.ToList();

        //get highest the index of highest marks
        int m = playerScoreList.Max();
        int p = playerScoreList.IndexOf(m);
        playerScoreList2.Add(p);
        playerScoreList.RemoveAt(p);
        
        //check if any same maximum value of index left
        for (int i = 0; i < playerScoreList.Count; i++)
        {
            if (playerScoreList.Max() == m)
            {
                p = playerScoreList.IndexOf(playerScoreList.Max());
                playerScoreList2.Add(p+1+i);
                playerScoreList.RemoveAt(p);
            }
        }

        

        //create winner panel and text and assign to position
        for (int i = 0; i < playerScoreList2.Count; i++)
        {
            playerIdentity = Instantiate(playerIdentityPerfab, 
                new Vector3(GameObject.Find("Winner"+playerScoreList2[i]).transform.position.x,
                GameObject.Find("Winner" + playerScoreList2[i]).transform.position.y,
                GameObject.Find("Winner" + playerScoreList2[i]).transform.position.z),
                Quaternion.identity) as Text;

            playerIdentity.text = "Winner";
            playerIdentity.transform.parent = GameObject.Find("EndGamePanel").transform;

            //color panel cant work,,, fix later... or not
            //panel = Instantiate(winnerPanel,
            //    new Vector3(GameObject.Find("Winner" + playerScoreList2[i]).transform.position.x,
            //    GameObject.Find("Winner" + playerScoreList2[i]).transform.position.y,
            //    GameObject.Find("Winner" + playerScoreList2[i]).transform.position.z),
            //    Quaternion.identity) as GameObject;

            //panel.transform.parent = GameObject.Find("EndGamePanel").transform;

        }

    }

}
