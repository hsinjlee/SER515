using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Directions;
using Mapbox.Unity.MeshGeneration.Factories;

public class Instantiate_Object : MonoBehaviour
{
    public GameObject myPrefab;
    public List<Vector3> theList;
    public List<Vector3> testList;
    //[SerializeField]
	//AbstractMap _map;
    //[SerializeField]
	//public Transform[] _waypoints;
    //var wp = new Vector2d[9];
    //GameObject Box;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ins obj here");
        //Instantiate(Box, Instantiate_Position.transform.position,Instantiate_Position.transform.rotation);
        //real-world coordinates
        theList = CanvasManager.waypts;
        testList = CanvasManager.testpts;
        foreach (var item in testList)
        {
            Debug.Log("x= " + item.x + " y= " + item.y + " z= " + item.z);
            Instantiate(myPrefab, new Vector3(item.x, item.y, item.z), Quaternion.identity);
        }
        //testList.Clear();
        //on unity map
        //for(int i = 1 ; i < 10 ; i++) {
            //Instantiate(myPrefab, new Vector3(-24.97468f, 0, -38.49812f), Quaternion.identity);
            //Instantiate(myPrefab, new Vector3(i, 0, 0), Quaternion.identity);
            //change map poistion to real world scale position(lat, lon)
            //myPrefab.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        theList = CanvasManager.waypts;
        testList = CanvasManager.testpts;
    }
}
