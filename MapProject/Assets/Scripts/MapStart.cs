using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class MapStart : MonoBehaviour
{
    public GameObject _direction;
    public List<GameObject> createObjects = new List<GameObject>();
    public GameObject schedule;
    public GameObject wayPoint;
    public GameObject instance;
    public int InputNum;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        schedule = GameObject.Find("ScheduleManager");
        if (schedule)
        {
            ScheduleManager data = schedule.GetComponent<ScheduleManager>();
            CanvasManager canvasManager = canvas.GetComponent<CanvasManager>();
            if(data.classSchedule.Count != 0)
            {
                canvasManager.updateClassScheduled(data.classSchedule);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
