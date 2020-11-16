using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Directions;
using Mapbox.Unity.MeshGeneration.Factories;

public class Instantiate_Object : MonoBehaviour
{
    public GameObject myPrefab;
    public List<Vector3> theList;
    // Start is called before the first frame update
    void Start()
    {
        //real-world coordinates
        theList = CanvasManager.waypts;
        foreach (var item in theList)
        {
            Debug.Log(" x = " + item.x + " y = " + item.y + " z = " + item.z);
            Instantiate(myPrefab, new Vector3(item.x, item.y, item.z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
