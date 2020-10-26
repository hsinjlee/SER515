using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;

public class WebScraper : MonoBehaviour
{
    public InputField username;
    public InputField password;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("start test on selenium");   
    }

    public void Scraper() {
        IWebDriver driver = new ChromeDriver("/Users/Marcushsu/Documents/GitHub/ss/bin/Debug/netcoreapp3.0/");
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
        String[] classSchedules = new String[className.Count];
        int i = 0;
        foreach(IWebElement name in className) {
            classSchedules[i++] = name.Text;
            Debug.Log(name.Text);
        }

        IList<IWebElement> classLocation = wait1.Until(e => e.FindElements(By.XPath("//td[@data-label='Location']")));
        i = 0;
        foreach(IWebElement loc in classLocation) {
            classSchedules[i++] = loc.Text;
            Debug.Log(loc.Text);
        }
        
        driver.Quit();
        Debug.Log("End test on selenium");
        SceneManager.LoadScene("MapDemo");
    }
}
