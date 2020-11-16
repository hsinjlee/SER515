using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Directions;
using Mapbox.Unity.MeshGeneration.Factories;

public class Instantiate_Object : MonoBehaviour
{
    public GameObject box;
    public GameObject dot;
    public List<Vector3> theList;
    // Start is called before the first frame update
    void Start()
    {
        //real-world coordinates
        theList = CanvasManager.waypts;
        if (theList.Count == 0) return;
        Instantiate(box, new Vector3(theList[0].x, theList[0].y, theList[0].z), Quaternion.identity);
        for (int i = 1; i < theList.Count; i++)
        {
            Instantiate(box, new Vector3(theList[i].x, theList[i].y, theList[i].z), Quaternion.identity);
            ConnectTwoPoints(i-1, i);
        }
    }

    //build up the connecting line between two points
    void ConnectTwoPoints(int startIndex, int endIndex)
    {
        float startX = theList[startIndex].x;
        float startZ = theList[startIndex].z;
        float endX = theList[endIndex].x;
        float endZ = theList[endIndex].z;
        float coordinatY = theList[startIndex].y;
        if (startX != endX)
        {
            if(startX > endX)
            {
                for(float j = endX + 0.01f ; j < startX ; j+= 0.01f)
                {
                    Instantiate(dot, new Vector3(j, coordinatY, endZ), Quaternion.identity);
                }
            }else
            {
                for (float j = startX + 0.01f ; j < endX ; j += 0.01f)
                {
                    Instantiate(dot, new Vector3(j, coordinatY, endZ), Quaternion.identity);
                }
            }
        }else
        {
            if (startZ > endZ)
            {
                for (float j = endZ + 0.01f ; j < startZ; j += 0.01f)
                {
                    Instantiate(dot, new Vector3(startX, coordinatY, j), Quaternion.identity);
                }
            }
            else
            {
                for (float j = startZ + 0.01f ; j < endZ; j += 0.01f)
                {
                    Instantiate(dot, new Vector3(startX, coordinatY, j), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
