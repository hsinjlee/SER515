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
    private HttpClient client;
    private string searchApi;
    // Start is called before the first frame update
    void Start()
    {
        client = new HttpClient();
        schedule = GameObject.Find("ScheduleManager");
        if (schedule)
        {
            ScheduleManager data = schedule.GetComponent<ScheduleManager>();
            CanvasManager canvasManager = canvas.GetComponent<CanvasManager>();
            canvasManager.updateClassScheduled(data.classSchedule);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
