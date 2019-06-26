using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject selectionPanel;
    [SerializeField]
    private Text playerSizeInputText;
    [SerializeField]
    private Text traprSizeInputText;
    [SerializeField]
    private Text gameTurnInputText;

    //default value
    public static int playerSize = 1;
    public static int trapSize = 3;
    public static int gameTurn = 10;
    public static string questionSet = "/Questions/english1.json";
    private int mapIndex = 1;

    [SerializeField]
    private GameObject[] map1;
    [SerializeField]
    private GameObject[] set;
    [SerializeField]
    private Color selectColor;
    [SerializeField]
    private Color diselectColor;

    private void Start()
    {
        map1 = GameObject.FindGameObjectsWithTag("Map");
        set = GameObject.FindGameObjectsWithTag("QuestionSet");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void NewGame()
    {
        selectionPanel.SetActive(true);

    }

    public void PlayerSizeIncreament()
    {
        if(playerSize < 4)
        {
            playerSize++;
            playerSizeInputText.text = playerSize.ToString();
        }
    }

    public void PlayerSizeDecreament()
    {
        if (playerSize > 1)
        {
            playerSize--;
            playerSizeInputText.text = playerSize.ToString();
        }
    }
    public void TrapSizeIncreament()
    {
        if (trapSize < 10)
        {
            trapSize += 3;
            traprSizeInputText.text = trapSize.ToString();
        }
    }

    public void TraprSizeDecreament()
    {
        if (trapSize > 3)
        {
            trapSize -=  3;
            traprSizeInputText.text = trapSize.ToString();
        }
    }

    public void GameTurnIncreament()
    {
        if (gameTurn < 30)
        {
            gameTurn += 10;
            gameTurnInputText.text = gameTurn.ToString();
        }
    }

    public void GameTurneDecreament()
    {
        if (gameTurn > 10)
        {
            gameTurn -= 10;
            gameTurnInputText.text = gameTurn.ToString();
        }
    }

    public void ChooseMap1()    
    {
        for (int i = 0; i < map1.Length; i++)
        {
            ColorBlock colors = map1[i].GetComponent<Button>().colors;
            colors.normalColor = diselectColor;
            map1[i].GetComponent<Button>().colors = colors;
        }

        ColorBlock color = map1[0].GetComponent<Button>().colors;
        color.normalColor = selectColor;
        color.selectedColor = selectColor;
        map1[0].GetComponent<Button>().colors = color;

        mapIndex = 1;
    }

    public void ChooseMap2()
    {
        for (int i = 0; i < map1.Length; i++)
        {
            ColorBlock colors = map1[i].GetComponent<Button>().colors;
            colors.normalColor = diselectColor;
            map1[i].GetComponent<Button>().colors = colors;
        }

        ColorBlock color = map1[1].GetComponent<Button>().colors;
        color.normalColor = selectColor;
        color.selectedColor = selectColor;
        map1[1].GetComponent<Button>().colors = color;

        mapIndex = 2;
    }

    public void ChooseMap3()
    {
        for (int i = 0; i < map1.Length; i++)
        {
            ColorBlock colors = map1[i].GetComponent<Button>().colors;
            colors.normalColor = diselectColor;
            map1[i].GetComponent<Button>().colors = colors;
        }

        ColorBlock color = map1[2].GetComponent<Button>().colors;
        color.normalColor = selectColor;
        color.selectedColor = selectColor;
        map1[2].GetComponent<Button>().colors = color;

        mapIndex = 3;
    }

    public void QuestionSet1()
    {
        for (int i = 0; i < set.Length; i++)
        {
            ColorBlock colors = set[i].GetComponent<Button>().colors;
            colors.normalColor = diselectColor;
            set[i].GetComponent<Button>().colors = colors;
        }

        ColorBlock color = set[0].GetComponent<Button>().colors;
        color.normalColor = selectColor;
        color.selectedColor = selectColor;
        set[0].GetComponent<Button>().colors = color;

        questionSet = "/Questions/english1.json";
    }

    public void QuestionSet2()
    {
        for (int i = 0; i < set.Length; i++)
        {
            ColorBlock colors = set[i].GetComponent<Button>().colors;
            colors.normalColor = diselectColor;
            set[i].GetComponent<Button>().colors = colors;
        }

        ColorBlock color = set[1].GetComponent<Button>().colors;
        color.normalColor = selectColor;
        color.selectedColor = selectColor;
        set[1].GetComponent<Button>().colors = color;

        questionSet = "/Questions/english2.json";
    }

    public void QuestionSet3()
    {
        for (int i = 0; i < set.Length; i++)
        {
            ColorBlock colors = set[i].GetComponent<Button>().colors;
            colors.normalColor = diselectColor;
            set[i].GetComponent<Button>().colors = colors;
        }

        ColorBlock color = set[2].GetComponent<Button>().colors;
        color.normalColor = selectColor;
        color.selectedColor = selectColor;
        set[2].GetComponent<Button>().colors = color;

        questionSet = "/Questions/english3.json";
    }

    public void Proceed()
    {
        SceneManager.LoadScene(mapIndex);
    }

}
