using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void Student()
    {
        SceneManager.LoadScene("LoginPage");
    }
    public void Faculty()
    {
        SceneManager.LoadScene("LoginPage");
    }
    public void Visitor()
    {
        SceneManager.LoadScene("MapDemo");
    }

    public void AR()
    {
        SceneManager.LoadScene("ARNavigation");
    }

    public void SubmitBtn()
    {
        SceneManager.LoadScene("MapDemo");
    }

}