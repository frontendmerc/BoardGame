using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypointer : MonoBehaviour
{

    public static Transform[] points;

    public GameObject[] up;
    public GameObject[] right;
    public GameObject[] down;
    public GameObject[] left;

    private void Awake()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }

    }
}
