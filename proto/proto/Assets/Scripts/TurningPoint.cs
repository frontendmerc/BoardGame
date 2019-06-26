using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPoint : MonoBehaviour
{
    //private Waypointer waypoint;

    public Waypointer waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypointer>();
    }

    //disable animation script and turnningpoint script to current player
    // Update is called once per frame
    void Update()
    {

        //turn UP
        for (int i = 0; i < waypoints.up.Length; i++)
        {
            if (transform.position == waypoints.up[i].transform.position)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Debug.Log("trigger up");
            }
        }

        //turn RIght
        for (int i = 0; i < waypoints.right.Length; i++)
        {
            if (transform.position == waypoints.right[i].transform.position)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                Debug.Log("trigger right");
            }
        }

        //turn Down
        for (int i = 0; i < waypoints.down.Length; i++)
        {
            if (transform.position == waypoints.down[i].transform.position)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Debug.Log("trigger down");
            }
        }

        //turn Left
        for (int i = 0; i < waypoints.left.Length; i++)
        {
            if (transform.position == waypoints.left[i].transform.position)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                Debug.Log("trigger left");
            }
        }


    }
}
