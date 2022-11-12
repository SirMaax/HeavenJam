using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("LevelINfo")] 
    [SerializeField] private String currentLevelName;
    [SerializeField] private String nextLevelName;
    
    [Header("SoulsInfo")]
    public static int amountSouls;
    public static int amountOfEscapedSouls;
 
    public static int amountOfFallenSouls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckIfGameIsOver()
    {
        if (amountOfEscapedSouls + amountOfFallenSouls >= amountSouls)
        {
            //LevelCompelete
            // SceneManager.LoadScene(nextLevelName);
            //Pause game and load ui
            
        }
        else if (amountOfFallenSouls >= amountSouls)
        {
            //Relaod Level 1
            SceneManager.LoadScene(currentLevelName);
        }
    }

    public void TriggerNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
