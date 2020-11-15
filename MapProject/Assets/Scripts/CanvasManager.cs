using JetBrains.Annotations;
using Mapbox.Directions;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject _direction;
    public List<GameObject> createObjects = new List<GameObject>();
    public GameObject wayPoint;
    public GameObject instance;
    public int InputNum;
    public static List<Vector3> waypts;
    public static List<Vector3> testpts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DrawDirection(String type)
    {
        if (instance == null)
        {
            DirectionsFactory theDirect = _direction.GetComponent<DirectionsFactory>();
            if(type != "Search")
            {
                theDirect._routeType = type;
            }
            List<Vector2d> theList = theDirect._waypointsGeo.ToList();
            theList.Clear();
            theList.Add(new Vector2d(33.404321, -111.939023));
            //theList.Add(new Vector2d(33.4191474, -111.9345634));
            //theList.Add(new Vector2d(33.4162891, -111.9379518));
            theList.Add(new Vector2d(33.404655, -111.939716));
            theDirect._waypointsGeo = theList.ToArray();
            instance = Instantiate(_direction, new Vector3(0, 0, 0), Quaternion.identity);
            //@test
            Debug.Log("cm here");
            waypts = theDirect._cachedWaypoints.ToList();
            //foreach(Vector3 v in waypts) {
			//	Debug.Log("waypts in cm = " + v.x + " , " + v.y + " , " + v.z);
			//}
            testpts = theDirect._testdata.ToList();
        }
        else
        {
            DirectionsFactory theDirect = instance.GetComponent<DirectionsFactory>();
            if(theDirect._routeType != type)
            {
                instance = null;
                GameObject.Find("Directions(Clone)").Destroy();
                GameObject.Find("direction waypoint  entity").Destroy();
                DrawDirection(type);
            }
            else
            {
                List<Transform> thelist = theDirect._waypoints.ToList();
                thelist.Add(Instantiate(wayPoint, new Vector2(0, 0), Quaternion.identity).transform);
                theDirect._waypoints = thelist.ToArray();
                theDirect.Refresh();
            }
            //@test
            waypts = theDirect._cachedWaypoints.ToList();
            testpts = theDirect._testdata.ToList();
            foreach(Vector3 v in testpts) {
				Debug.Log("testdata in cm = " + v.x + " , " + v.y + " , " + v.z);
			}
        }
    }
    public void SearchButton()
    {
        DrawDirection("Search");   
    }

    public void WalkingButton()
    {
        DrawDirection("Walking");
    }

    public void DrivingButton()
    {
        DrawDirection("Driving");
    }
}
