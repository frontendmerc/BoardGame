using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour
{
    public int playerSize = 0;

    //get prefabs object
    public GameObject players;
    //store spawn players in a parent object
    public Transform parentObject;
    //get start point waypoint
    public Transform startPointLocation;

    public static GameObject[] player;

    public static bool starter = true;
    
    private void Awake()
    {
        GameObject multiPlayer;
        int increament = 5;
        Vector3 startLocation;
        Quaternion rotation;
        Renderer rend;

        player = new GameObject[ButtonScript.playerSize];

        for (int i = 0; i < ButtonScript.playerSize; i++)
        {
            startLocation = new Vector3(startPointLocation.position.x + increament, startPointLocation.position.y, startPointLocation.position.z + +increament);
            rotation = new Quaternion(0, 1f, 0, 0);

            multiPlayer = Instantiate(players, startPointLocation.position, Quaternion.Euler(0, 90f, 0));
            multiPlayer.transform.parent = parentObject;
            multiPlayer.name = "Player" + (i + 1);
            increament++;

            player[i] = GameObject.Find(multiPlayer.name);
            player[i].GetComponent<PlayersMove>().enabled = false;
            player[i].GetComponent<RunAnimation>().enabled = false;
        }

        player[0].GetComponent<PlayersMove>().enabled = true;
        player[0].GetComponent<RunAnimation>().enabled = true;

    }

    public Color hairColor;
    public Color clothColor;

    //void switchCharacterColor(int playerNumber, GameObject player)
    //{
    //    switch (playerNumber)
    //    {
    //        case 1:
    //            player.GetComponentInChildren<Shader>().

    //            break;


    //    }
    //}

}
