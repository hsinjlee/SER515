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
        
        //DesiredCapabilities Usercapabilities = new DesiredCapabilities();
        //Usercapabilities.SetCapability("deviceName", "iPhone 8");
        //Usercapabilities.SetCapability("platformVersion", "10.3");
        //Usercapabilities.SetCapability("platformName", "iOS");
        //Usercapabilities.SetCapability("automationName", "XCUITest");
        //Usercapabilities.SetCapability("app", "/path/to/my.app");
        //Usercapabilities.SetCapability("MobileCapabilityType.APP", "System.getProperty('user.dir') + '/build/SampleiOS.app'");
        //using the Appium_dot_net_driver (version 1.3.0.1) 
        
        //Initialise appium which throwing some error saying to add <IWebelement>
        //AppiumDriver<AppiumWebElement> driver1;
        //VVVV this line has problem
        //driver1 = new IOSDriver<IWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), Usercapabilities);
        //driver1.Navigate().GoToUrl("https://weblogin.asu.edu/cas/login?service=https%3A%2F%2Fweblogin.asu.edu%2Fcgi-bin%2Fcas-login%3Fcallapp%3Dhttps%253A%252F%252Fwebapp4.asu.edu%252Fmyasu%252F%253Finit%253Dfalse"); //launch URL

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

        //@test
        // for(int i = 0 ; i < weekSchedule.Count ; i++) {
        //     Debug.Log("weekday = " + i);
        //     for(int j = 0 ; j < weekSchedule[i].Count ; j++) {
        //         Debug.Log("class: " + weekSchedule[i][j]);
        //     }
        // }

        //@test
        // foreach (KeyValuePair<string, HashSet<string>> ins in classDetails)
        // {
        //     Debug.Log("Details of class: " + ins.Key);       
        //     foreach(string s in ins.Value) {
        //         Debug.Log(s);
        //     }
        // }
        
        driver.Quit();
        Debug.Log("End test on selenium");
        SceneManager.LoadScene("MapDemo");
    }
}
