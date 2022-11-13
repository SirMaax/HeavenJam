using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private float startTime;
    public TMP_Text timer;
    public TMP_Text soulsSaved;

    public float goneTime;
    // Start is called before the first frame update
    void Start()
    {
        amountSouls = GameObject.FindGameObjectsWithTag("Soul").Length;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        TrackTime();
        UpdateSousl();
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

    public void TrackTime()
    {
        //UPdate Time 
        goneTime =- Time.time - startTime ;
        int temp = (int)goneTime *-1;
        timer.SetText( temp.ToString());
    }

    private void UpdateSousl()
    {
        soulsSaved.SetText(amountOfEscapedSouls.ToString() + "/" + amountSouls.ToString());
    }
}
