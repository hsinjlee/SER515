using JetBrains.Annotations;
using Mapbox.Directions;
using Mapbox.Examples;
using Mapbox.Geocoding;
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
    public ForwardGeocodeUserInput _searchLocation;
    public GameObject _direction;
    public List<GameObject> createObjects = new List<GameObject>();
    public GameObject wayPoint;
    public GameObject instance;
    public int InputNum;
    public GameObject _user;
    public InputField StartPoint;
    public InputField EndPoint;
    private Vector2d searchStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void DrawDirection(String type)
    {
        
       
            DirectionsFactory theDirect = instance.GetComponent<DirectionsFactory>();
            if (theDirect._routeType != type)
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
        
    }
    public void SearchButton()
    {
        _searchLocation.OnGeocoderResponse += p;   
    }
    
    public void WalkingButton()
    {
        DrawDirection("Walking");
    }

    public void DrivingButton()
    {
        DrawDirection("Driving");
    }
    void p(ForwardGeocodeResponse response)
    {
        searchStart.x = _searchLocation.Coordinate.x;
        searchStart.y = _searchLocation.Coordinate.y;
        Debug.Log("ddddd"+searchStart);
        string startBuilding = StartPoint.text;
        if (instance == null)
        {
            DirectionsFactory theDirect = _direction.GetComponent<DirectionsFactory>();
            List<Vector2d> theList = theDirect._waypointsGeo.ToList();
            theList.Clear();
            if (startBuilding != "user")
            {
                theDirect.userOrNot = false;
                Debug.Log("++" + searchStart);
                theList.Add(new Vector2d(searchStart.x, searchStart.y));
            }
            else
            {
                theList.Add(new Vector2d(0, 0));
                Vector3 temp = _user.transform.position;
                theDirect._userPosition = temp;
                theDirect.userOrNot = true;
            }
            theList.Add(new Vector2d(33.4162891, -111.9379518));
            theDirect._waypointsGeo = theList.ToArray();
            instance = Instantiate(_direction, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
