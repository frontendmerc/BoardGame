using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGenerate : MonoBehaviour
{
    private GameObject cubeObject;
    [SerializeField]
    private GameObject cubePrefabs;
    [SerializeField]
    private Transform tilesParentFolder;
    [SerializeField]
    private Material questionTileMaterial;
    [SerializeField]
    private Material trapTileMaterial;
    [SerializeField]
    private Material startTileMaterial;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Waypointer.points.Length; i++)
        {
            cubeObject = Instantiate(cubePrefabs, Waypointer.points[i].transform.position, Quaternion.identity);

            if(i == 0)
            {
                cubeObject.GetComponent<Renderer>().material = startTileMaterial;
            }
            else
            {
                if (GameplayController.trapTileList.Contains(i))
                {
                    cubeObject.GetComponent<Renderer>().material = trapTileMaterial;
                }
                else
                {
                    cubeObject.GetComponent<Renderer>().material = questionTileMaterial;
                }
            }

            cubeObject.transform.parent = tilesParentFolder;
        }
    }

}
