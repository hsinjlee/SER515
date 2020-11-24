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
    public List<string> classSchedule;
    private Boolean scheduleOrNot;
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
            theDirect.scheduleOrNot = false;
            List<Transform> thelist1 = theDirect._waypoints.ToList();
            while(thelist1.Count > 2)
            {
                thelist1.RemoveAt(thelist1.Count - 1);
            }
            theDirect._waypoints = thelist1.ToArray();
            if (type != "Search")
            {
               theDirect._routeType = type;
            }
            List<Vector2d> theList = theDirect._waypointsGeo.ToList();
            theList.Clear();
            if(!startBuilding.ToString().Equals("user"))
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
        }
        else
        {
            DirectionsFactory theDirect = instance.GetComponent<DirectionsFactory>();
            if (theDirect._routeType != type)
            {
                instance = null;
                GameObject.Find("Directions(Clone)").Destroy();
                GameObject.Find("direction waypoint  entity").Destroy();
                GameObject[] g = GameObject.FindGameObjectsWithTag("waypointc");
                foreach(GameObject gg in g)
                {
                    Destroy(gg);
                }
                DrawDirection(type);
            }
        }
    }

    private void DrawDirectionBySchedule(String type)
    {
        if (instance == null)
        {
            DirectionsFactory theDirect = _direction.GetComponent<DirectionsFactory>();
            theDirect.scheduleOrNot = true;
            if (type != "Schedule")
            {
                theDirect._routeType = type;
            }
            List<Vector2d> theList = theDirect._waypointsGeo.ToList();
            theList.Clear();
            int index = 0;
            theDirect.userOrNot = false;
            List<Transform> thelist1 = theDirect._waypoints.ToList();
            while (thelist1.Count > 2)
            {
                thelist1.RemoveAt(thelist1.Count - 1);
            }
            foreach (string item in classSchedule)
            {
                string stemp = (item).Replace(" ", "%20");
                searchApi = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=" + stemp + "&inputtype=textquery&fields=geometry&key=AIzaSyCCaNjplKt-tq3Nvxq0Hb28Etu7KZUaqE0";
                var startcontent = JObject.Parse(client.GetStringAsync(searchApi).Result);
                double lat = Double.Parse(startcontent["candidates"][0]["geometry"]["location"]["lat"].ToString());
                double lng = Double.Parse(startcontent["candidates"][0]["geometry"]["location"]["lng"].ToString());
                if (index == 0)
                {
                    theList.Add(new Vector2d(lat, lng));
                    if(classSchedule.Count() == 1)
                    {
                        theList.Add(new Vector2d(lat, lng));
                    }
                }
                else if(index == 1)
                {
                    theList.Add(new Vector2d(lat, lng));
                }
                else
                {
                    thelist1.Add(Instantiate(wayPoint, new Vector2(0, 0), Quaternion.identity).transform);
                    theList.Add(new Vector2d(lat, lng));
                }
                index++;
            }
            theDirect._waypoints = thelist1.ToArray();
            theDirect._waypointsGeo = theList.ToArray();
            instance = Instantiate(_direction, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            DirectionsFactory theDirect = instance.GetComponent<DirectionsFactory>();
            if (theDirect._routeType != type)
            {
                instance = null;
                GameObject.Find("Directions(Clone)").Destroy();
                GameObject.Find("direction waypoint  entity").Destroy();
                GameObject[] g = GameObject.FindGameObjectsWithTag("waypointc");
                foreach (GameObject gg in g)
                {
                    Destroy(gg);
                }
                DrawDirectionBySchedule(type);
            }
        }
    }

    public void SearchButton()
    {
        scheduleOrNot = false;
        DrawDirection("Search");   
    }

    public void WalkingButton()
    {
        if (scheduleOrNot)
        {
            DrawDirectionBySchedule("Walking");
        }
        else
        {
            DrawDirection("Walking");
        }
    }

    public void DrivingButton()
    {
        if (scheduleOrNot)
        {
            DrawDirectionBySchedule("Driving");
        }
        else
        {
            DrawDirection("Driving");
        }
    }

    public void ShowSchedule() {
        GameObject.Find("Panel").SetActive(false);
        SceneManager.LoadScene("MySchedule");
    }

    public void updateClassScheduled()
    {
        GameObject schedule = GameObject.Find("ScheduleManager");
        if (schedule)
        {
            ScheduleManager data = schedule.GetComponent<ScheduleManager>();
            if (data.classSchedule.Count != 0)
            {
                classSchedule = data.classSchedule;
            }
        }
        scheduleOrNot = true;
        DrawDirectionBySchedule("Schedule");
    }

    public void route1()
    {
        if (instance)
        {
            DirectionsFactory theDirect = instance.GetComponent<DirectionsFactory>();
            theDirect.routeNum = 0;
            theDirect.Refresh();
        }
    }

    public void route2()
    {
        if (instance)
        {
            DirectionsFactory theDirect = instance.GetComponent<DirectionsFactory>();
            theDirect.routeNum = 1;
            theDirect.Refresh();
        }
    }
}
