using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase_classSchedules : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Firebase write data at a reference");
        GameObject location = GameObject.Find("Scraper");
        WebScraper webScraper = location.GetComponent<WebScraper>();
        //location.GetComponentInChildren<WebScraper>();
    }
    //public List<List<string>> weekSchedule = new List<List<string>>(5);
    //public Dictionary<string, HashSet<string>> classDetails = new Dictionary<string, HashSet<string>>();
    //public List<Dictionary<int, List<string>>> tempSchedule = new List<Dictionary<int, List<string>>>(5);
   

    //public string className;
    //public string classLocation;
    //Firebase_classSchedules schedules = new Firebase_classSchedules();
    //WebScraper wScript = 

    //public Firebase_classSchedules()
    //{

    //}

    //public Firebase_classSchedules(string className, string classLocation)
    //{
    //    this.className = className;
    //    this.classLocation = classLocation;
    //}

    //private void writeClassSchedules(string locationID, string className, string classLocation)
    //{
    //    Firebase_classSchedules classSchedules = new Firebase_classSchedules(className, classLocation);
    //    string json = JsonUtility.ToJson(classSchedules);

    //    Debug.Log("End of write data at a reference");

    //}

    //Push();
    
}
