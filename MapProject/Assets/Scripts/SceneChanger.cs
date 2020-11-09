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
        new SpawnObject();
        SceneManager.LoadScene("ARNavigation");
    }

    public void SubmitBtn()
    {
        SceneManager.LoadScene("MapDemo");
    }

}