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
    }
    public string classNeme;
    public string classLocation;

    public Firebase_classSchedules()
    {

    }

    public Firebase_classSchedules(string className, string classLocation)
    {
        this.classNeme = className;
        this.classLocation = classLocation;
    }

    private void writeWebScrapper(string className, string classLocation)
    {

    }
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
