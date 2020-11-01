using JetBrains.Annotations;
using Mapbox.Directions;
using Mapbox.Unity.MeshGeneration.Factories;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SearchButton()
    {
        Vector3 position = new Vector3(0, 0, 0);

        if (instance == null)
        {
            instance = Instantiate(_direction, position, Quaternion.identity);
        }
        else
        {
            DirectionsFactory theDirect = instance.GetComponent<DirectionsFactory>();
            List<Transform> thelist = theDirect._waypoints.ToList();
            thelist.Add(Instantiate(wayPoint, position, Quaternion.identity).transform);

            theDirect._waypoints = thelist.ToArray();
            theDirect.Refresh();
        }
        
        
        String start = GameObject.Find("StartPointText").GetComponent<Text>().text;
        String end = GameObject.Find("EndPointText").GetComponent<Text>().text;
        Debug.Log(start);
        Debug.Log(end);

    }
}
