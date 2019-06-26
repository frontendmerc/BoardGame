using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimation : MonoBehaviour
{

    public Animator run;

    // Update is called once per frame
    void Update()
    {
        if (PlayersMove.runTrigger)
        {
            run.SetTrigger("RunTriger");
        }
        else
        {
            run.SetTrigger("IdleTriger");
        }
    }
}
