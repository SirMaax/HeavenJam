using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("1.Level");
    }
    public void StartLevel2()
    {
        SceneManager.LoadScene("2.Level");
    }
    public void StartLevel3()
    {
        SceneManager.LoadScene("3.Level");
    }
    public void StartLevel4()
    {
        SceneManager.LoadScene("4.Level");
    }
    public void StartLevel5()
    {
        SceneManager.LoadScene("5.Level");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevelLoading()
    {
        SceneManager.LoadScene("0.5Level");

    }
    
    public void Return()
    {
        SceneManager.LoadScene("0.Level");

    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Levels/HowToPlay");
    }
}
