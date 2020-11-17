using JetBrains.Annotations;
using Mapbox.Directions;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using Mapbox.Json.Linq;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject _direction;
    public List<GameObject> createObjects = new List<GameObject>();
    public GameObject wayPoint;
    public GameObject instance;
    public int InputNum;
    public GameObject _user;
    public InputField StartPoint;
    public InputField EndPoint;
    private HttpClient client;
    private string searchApi;
    public static List<Vector3> waypts;
    // Start is called before the first frame update
    void Start()
    {
       client  = new HttpClient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DrawDirection(String type)
    {
        string startBuilding = StartPoint.text;
        string endBuilding = EndPoint.text;
        if (instance == null)
        {
            DirectionsFactory theDirect = _direction.GetComponent<DirectionsFactory>();
            if (type != "Search")
            {
               theDirect._routeType = type;
            }
            List<Vector2d> theList = theDirect._waypointsGeo.ToList();
            theList.Clear();
            if(startBuilding != "user")
            {
                string stemp = startBuilding.Replace(" ", "%20");
                searchApi = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input="+ stemp + "&inputtype=textquery&fields=geometry&key=AIzaSyCCaNjplKt-tq3Nvxq0Hb28Etu7KZUaqE0"; 
                
                var startcontent = JObject.Parse(client.GetStringAsync(searchApi).Result);
                
                double lat = Double.Parse(startcontent["candidates"][0]["geometry"]["location"]["lat"].ToString());
                double lng = Double.Parse(startcontent["candidates"][0]["geometry"]["location"]["lng"].ToString());
                
                
                theDirect.userOrNot = false;
                theList.Add(new Vector2d(lat, lng));
            }
            else
            {
                theList.Add(new Vector2d(0, 0));
                Vector3 temp = _user.transform.position;
                theDirect._userPosition = temp;
                theDirect.userOrNot = true;
            }
            string etemp = endBuilding.Replace(" ", "%20");
            searchApi = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=" + etemp + "&inputtype=textquery&fields=geometry&key=AIzaSyCCaNjplKt-tq3Nvxq0Hb28Etu7KZUaqE0";
            var endcontent = JObject.Parse(client.GetStringAsync(searchApi).Result);

            double elat = Double.Parse(endcontent["candidates"][0]["geometry"]["location"]["lat"].ToString());
            double elng = Double.Parse(endcontent["candidates"][0]["geometry"]["location"]["lng"].ToString());
            theList.Add(new Vector2d(elat, elng));
            theDirect._waypointsGeo = theList.ToArray();
            instance = Instantiate(_direction, new Vector3(0, 0, 0), Quaternion.identity);
            waypts = theDirect._waypointsOnMap.ToList();
            foreach(Vector3 item in waypts)
            {
                Debug.Log("cm1: x = " + item.x + " y = " + item.y + " z= " + item.z);
            }
        }
        else
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
            waypts = theDirect._waypointsOnMap.ToList();
            foreach (Vector3 item in waypts)
            {
                Debug.Log("cm2: x = " + item.x + " y = " + item.y + " z= " + item.z);
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

    public void ShowSchedule() {
        GameObject.Find("Panel").SetActive(false);
        SceneManager.LoadScene("MySchedule");
    }
}
