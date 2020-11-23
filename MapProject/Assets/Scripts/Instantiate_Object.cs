using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Directions;
using Mapbox.Unity.MeshGeneration.Factories;

public class Instantiate_Object : MonoBehaviour
{
    public GameObject dot;
    public GameObject marker;
    public List<Vector3> theList;
    float offset = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //real-world coordinates
        theList = CanvasManager.waypts;
        if (theList.Count == 0) return;
        //adjust scale of real-world
        for(int i = 0; i < theList.Count; i++)
        {
            theList[i] *= 3f;
        }

        Instantiate(marker, new Vector3(theList[0].x, theList[0].y, theList[0].z), Quaternion.identity);
        for (int i = 1; i < theList.Count; i++)
        {
            if (i == theList.Count - 1)
            {
                Instantiate(marker, new Vector3(theList[i].x, theList[i].y, theList[i].z), Quaternion.identity);
            }
            else
            {
                Instantiate(dot, new Vector3(theList[i].x, theList[i].y, theList[i].z), Quaternion.identity);
            }
            ConnectTwoPoints(i - 1, i);
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
        if (startX != endX && startZ != endZ)
        {
            if (Mathf.Abs(startX - endX) > Mathf.Abs(startZ - endZ))
            {
                calculateX(startX, endX, coordinatY, endZ);
            }
            else
            {
                calculateZ(startZ, endZ, coordinatY, startX);
            }
        }
        else if (startX != endX)
        {
            calculateX(startX, endX, coordinatY, endZ);
        }
        else
        {
            calculateZ(startZ, endZ, coordinatY, startX);
        }
    }
    void calculateX(float start, float end, float y, float z)
    {
        if (start > end)
        {
            for (float j = end + offset ; j < start; j += offset)
            {
                Instantiate(dot, new Vector3(j, y, z), Quaternion.identity);
            }
        }
        else
        {
            for (float j = start + offset ; j < end; j += offset)
            {
                Instantiate(dot, new Vector3(j, y, z), Quaternion.identity);
            }
        }
        
    }
    void calculateZ(float start, float end, float y, float x)
    {
        if (start > end)
        {
            for (float j = end + offset ; j < start; j += offset)
            {
                Instantiate(dot, new Vector3(x, y, j), Quaternion.identity);
            }
        }
        else
        {
            for (float j = start + offset ; j < end; j += offset)
            {
                Instantiate(dot, new Vector3(x, y, j), Quaternion.identity);
            }
        }
    }
}
