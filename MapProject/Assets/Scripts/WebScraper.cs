using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start test on selenium");   
    }

    public void Scraper() {
        IWebDriver driver = new ChromeDriver("/Users/Marcushsu/Documents/GitHub/ss/bin/Debug/netcoreapp3.0/");
        driver.Url = "https://weblogin.asu.edu/cas/login?service=https%3A%2F%2Fweblogin.asu.edu%2Fcgi-bin%2Fcas-login%3Fcallapp%3Dhttps%253A%252F%252Fwebapp4.asu.edu%252Fmyasu%252F%253Finit%253Dfalse";
        
        DesiredCapabilities Usercapabilities = new DesiredCapabilities();
        Usercapabilities.SetCapability("deviceName", "iPhone 8");
        Usercapabilities.SetCapability("platformVersion", "10.3");
        Usercapabilities.SetCapability("platformName", "iOS");
        Usercapabilities.SetCapability("automationName", "XCUITest");
        Usercapabilities.SetCapability("app", "/path/to/my.app");
        Usercapabilities.SetCapability("MobileCapabilityType.APP", "System.getProperty('user.dir') + '/build/SampleiOS.app'");
         //using the Appium_dot_net_driver (version 1.3.0.1) 
        
        //Initialise appium which throwing some error saying to add <IWebelement>
        AppiumDriver<AppiumWebElement> driver1;
        //VVVV this line has problem
        driver1 = new IOSDriver<IWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), Usercapabilities);
        //driver1.Navigate().GoToUrl("https://weblogin.asu.edu/cas/login?service=https%3A%2F%2Fweblogin.asu.edu%2Fcgi-bin%2Fcas-login%3Fcallapp%3Dhttps%253A%252F%252Fwebapp4.asu.edu%252Fmyasu%252F%253Finit%253Dfalse"); //launch URL

        IWebElement textbox1 = driver.FindElement(By.Id("username"));
        textbox1.SendKeys("chsu65");

        IWebElement textbox2 = driver.FindElement(By.Name("password"));
        textbox2.SendKeys("F127906052bad748ad111");

        IWebElement textbox3 = driver.FindElement(By.Name("submit"));
        textbox3.Submit();
        //new webpage
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement textbox4 = wait.Until(e => e.FindElement(By.XPath("/html/body/div/main/div/div[@id='myasu-main-container']/div[@class='myasu-content-container']/div[@class='column-container']/div[@class='column-container-row']/div[@class='content-column']/div[@class='splitcontentleft']/section[@class='box' and @data-box-order ='30']/div[@id='classes_content']/div[@class='tab-pane']/div[@id='2207_tab']/div[@class='box-padding']/div[@id='my_schedule_line']/a")));
        //IWebElement textbox4 = wait.Until(e => e.FindElement(By.XPath("//div[@id='2207_tab']/div[@class='box-padding']/[@id='my_schedule_line']/a")));
        Debug.Log(textbox4.Text);
        textbox4.Click();
        driver.Quit();
        Debug.Log("End test on selenium");   
        //
        SceneManager.LoadScene("MapDemo"); 
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
