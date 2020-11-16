using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

public class WebScraper : MonoBehaviour
{
    public List<List<string>> weekSchedule = new List<List<string>>(5);
    public Dictionary<string,HashSet<string>> classDetails = new Dictionary<string,HashSet<string>>(); 
    public List<Dictionary<int, List<string>>> tempSchedule = new List<Dictionary<int, List<string>>>(5);
    public InputField username;
    public InputField password;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("start test on selenium");
    }

    public void Scraper() {
        IWebDriver driver = new ChromeDriver(Environment.CurrentDirectory);
        driver.Url = "https://weblogin.asu.edu/cas/login?service=https%3A%2F%2Fweblogin.asu.edu%2Fcgi-bin%2Fcas-login%3Fcallapp%3Dhttps%253A%252F%252Fwebapp4.asu.edu%252Fmyasu%252F%253Finit%253Dfalse";
        
        //init
        for (int i = 0 ; i < 5 ; i++) {
            weekSchedule.Add(new List<string>());
            tempSchedule.Add(new Dictionary<int, List<string>>());
        }

        IWebElement usernameTextbox = driver.FindElement(By.Id("username"));
        usernameTextbox.SendKeys(username.text);

        IWebElement passwordTextbox = driver.FindElement(By.Name("password"));
        passwordTextbox.SendKeys(password.text);

        IWebElement submitTextBox = driver.FindElement(By.Name("submit"));
        submitTextBox.Submit();
        //new webpage
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3600));
        IWebElement scheduleTextbox = wait.Until(e => e.FindElement(By.XPath("//div[@id='2207_tab']/div[@class='box-padding']/div[@id='my_schedule_line']/a")));
        Debug.Log(scheduleTextbox.Text);
        scheduleTextbox.Click();

        WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        IList<IWebElement> className = wait1.Until(e => e.FindElements(By.XPath("//td[@data-label='Course']")));
        IList<IWebElement> classDays = wait1.Until(e => e.FindElements(By.XPath("//td[@data-label='Days']")));
        IList<IWebElement> classLocation = wait1.Until(e => e.FindElements(By.XPath("//td[@data-label='Location']")));
        IList<IWebElement> classTimes = wait1.Until(e => e.FindElements(By.XPath("//td[@data-label='Times']")));

        for(int i = 0, j = 0; i < className.Count ; i++) {
            //parsing classTimes
            string[] parseTimeFormat = classTimes[i].Text.Split(' ');
            string[] parseHourMin = parseTimeFormat[0].Split(':');
            int hour = Int32.Parse(parseHourMin[0]);
            int minute = Int32.Parse(parseHourMin[1]);
            string timeFormat = parseTimeFormat[1];
            if(timeFormat == "PM") {
                hour += 12;
            }
            int timekey = hour*100 + minute;
            //parsing classDays
            string[] days = classDays[i].Text.Split(' ');
            foreach(string day in days) {
                switch (day) {
                    case "M":
                        j = 0;
                        break;                        
                    case "T":
                        j = 1;
                        break;
                    case "W":
                        j = 2;
                        break;
                    case "Th":
                        j = 3;
                        break;
                    case "F":
                        j = 4;
                        break;
                    default:
                        break;
                }
                if (tempSchedule[j].ContainsKey(timekey)) {
                    tempSchedule[j][timekey].Add(className[i].Text);
                } else {
                    tempSchedule[j].Add(timekey, new List<string>{className[i].Text});
                }
                //class details
                HashSet<string> existing;
                if (!classDetails.TryGetValue(className[i].Text, out existing)) {
                    existing = new HashSet<string>();
                    classDetails[className[i].Text] = existing;
                }
                classDetails[className[i].Text].Add(classLocation[i].Text);
            }
        }

        //sort each dictionary and save into weekSchedule
        for (int i = 0 ; i < tempSchedule.Count ; i++) {
            var list = tempSchedule[i].Keys.ToList();
            list.Sort();
            foreach (var key in list) {
                foreach(string course in tempSchedule[i][key])
                    weekSchedule[i].Add(course);
            }
        }
        
        
        driver.Quit();
        Debug.Log("End test on selenium");
        SceneManager.LoadScene("MapDemo");
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.transform.root);
    }
}