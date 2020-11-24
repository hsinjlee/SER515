using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScheduleManager : MonoBehaviour
{
    public List<string> classSchedule;
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;

    private List<List<string>> weekSchedule;
    private Dictionary<string, HashSet<string>> classDetails;
    private bool updated = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject);
        GameObject scraper = GameObject.FindWithTag("Scraper");
        WebScraper webScraper;
        if (scraper != null)
        {
            Debug.Log("Scraper Found.");
            webScraper = scraper.GetComponent<WebScraper>();
            weekSchedule = webScraper.weekSchedule;
            classDetails = webScraper.classDetails;
        }
        else
        {
            Debug.Log("Scraper Not Found.");
        }

        // classSchedule = new List<string> { "Memorial Union", "Hayden Library", "ASU SDFC Field", "ASU ISTB 4" };
        if (!updated)
        {
            GenerateSchedule();
        }
        foreach (string s in classSchedule)
        {
            Debug.Log(s);
            GenerateItem(s);
        }
        scrollView.verticalNormalizedPosition = 1;
    }

    public void GenerateSchedule()
    {
        string weekday = DateTime.Today.DayOfWeek.ToString();
        int key;
        switch (weekday)
        {
            case "Monday":
                key = 0;
                break;
            case "Tuesday":
                key = 1;
                break;
            case "Wednesday":
                key = 2;
                break;
            case "Thursday":
                key = 3;
                break;
            case "Friday":
                key = 4;
                break;
            default:
                return;
        }
        List<string> dailyClass = weekSchedule[key];
        foreach (string course in dailyClass)
        {
            Debug.Log(course);
            HashSet<string> details = classDetails[course];

            string location = null;
            foreach (string l in details)
            {
                location = l;
            }
            if (!location.Equals("ASU Sync"))
            {
                classSchedule.Add(StringFilter(location));
            }

        }

    }

    public void BackToMainPage()
    {
        SceneManager.LoadScene("MapDemo");
    }

    void GenerateItem(string str)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        scrollItemObj.transform.SetParent(scrollContent.transform, false);
        scrollItemObj.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = str;
    }

    public List<string> getClassSchedule()
    {
        return classSchedule;
    }

    public void UpdateButton()
    {
        List<string> updatedList = new List<string>();
        int childCount = scrollContent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform scrollItem = scrollContent.transform.GetChild(i);
            string newLocation = scrollItem.transform.Find("Text").gameObject.GetComponent<Text>().text;
            if (newLocation.Length == 0)
            {
                newLocation = scrollItem.transform.Find("Placeholder").gameObject.GetComponent<Text>().text;
            }
            scrollItem.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = newLocation;
            updatedList.Add(StringFilter(newLocation));
        }
        classSchedule = updatedList;
        updated = true;
    }

    private string StringFilter(string s) {
        s = s.Replace("PRLTA", "PERALTA HALL");
        s = s.Replace("SANTN", "SANTAN HALL");
        s = s.Replace("AGBC", "AGRIBUSINESS CENTER");
        s = s.Replace("SANCA", "SANTA CATALINA HALL");
        s = s.Replace("TECH", "TECHNOLOGY CENTER");
        s = s.Replace("ARAVA", "ARAVAIPA AUDITORIUM");
        s = s.Replace("PICHO", "PICACHO HALL");
        s = s.Replace("SIM", "SIMULATOR BUILDING");
        s = s.Replace("ENGR", "ENGINEERING STUDIO");
        s = s.Replace("LNTAHL", "LANTANA HALL");
        s = s.Replace("CNTR", "ACADEMIC CENTER");
        s = s.Replace("SDFCP", "SUN DEVIL FITNESS COMPLEX");
        return s;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


}