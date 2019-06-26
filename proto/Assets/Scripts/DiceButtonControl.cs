using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceButtonControl : MonoBehaviour
{
    public static bool rollDice = false;
    [SerializeField]
    private Button diceButton;

    private void Update()
    {
        if (!rollDice)
        {
            diceButton.interactable = true;
        }
    }

    public void trigger2()
    {
        rollDice = true;
        diceButton.interactable = false;
    }
}
