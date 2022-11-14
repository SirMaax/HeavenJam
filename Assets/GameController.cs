using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool roundOver;
    [SerializeField] private GameObject endRoundUi;
    [SerializeField] public GameObject[] deactive;
    
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
    public TMP_Text endTIme;
    public TMP_Text soulsSaved_afterGame;

    public float goneTime;
    private AudioManager _audioManager;

    private void Awake()
    {
        // amountSouls = GameObject.FindGameObjectsWithTag("Soul").Length;
        // goneTime = 0;
        // startTime = Time.time;
        // _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        // roundOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        amountSouls = GameObject.FindGameObjectsWithTag("Soul").Length;
        goneTime = 0;
        startTime = Time.time;
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        roundOver = false;
        // foreach (var ele in deactive)
        // {
        //     ele.SetActive(true);
        // }
        // endRoundUi.SetActive(false);
        // amountSouls = GameObject.FindGameObjectsWithTag("Soul").Length;
        // _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        roundOver = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.roundOver) return;
        TrackTime();
        UpdateSousl();
        CheckIfGameIsOver();
    }

    private void CheckIfGameIsOver()
    {
        if (roundOver) return;
        if (amountOfEscapedSouls + amountOfFallenSouls >= amountSouls)
        {
            //LevelCompelete
            //Pause game and load ui
            _audioManager.PlaySound(4);
            roundOver = true;
            endRoundUi.SetActive(true);
            goneTime =- Time.time - startTime ;
            int temp = (int)goneTime *-1;
            if (temp > 60)
            {
                endTIme.SetText((temp/60).ToString() + " minutes!") ;
            }
            else
            {
                endTIme.SetText((temp).ToString() + " seconds!") ;
            }
            soulsSaved_afterGame.SetText(amountOfEscapedSouls.ToString() + " from " + amountSouls.ToString());
            foreach (var ele in deactive)
            {
                ele.SetActive(false);
            }
            //Disable otherObjects
        }
        else if (amountOfFallenSouls >= amountSouls)
        {
            //Relaod Level 1
            SceneManager.LoadScene(currentLevelName);
        }
    }

    public void TriggerNextLevel()
    {
        amountSouls = GameObject.FindGameObjectsWithTag("Soul").Length;
        startTime = Time.time;
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        roundOver = false;
        amountOfEscapedSouls = 0;
        goneTime = 0;
        SceneManager.LoadScene(nextLevelName);
    }

    public void TrackTime()
    {
        //UPdate Time 
        goneTime = startTime - Time.time;
        // goneTime =- Time.time - startTime ;
        int temp = (int)goneTime *-1;
        timer.SetText( temp.ToString());
    }

    private void UpdateSousl()
    {
        soulsSaved.SetText(amountOfEscapedSouls.ToString() + "/" + amountSouls.ToString());
    }

    public void RetryLevel()
    {
        amountSouls = GameObject.FindGameObjectsWithTag("Soul").Length;
        startTime = Time.time;
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        roundOver = false;
        amountOfEscapedSouls = 0;
        goneTime = 0;
        // foreach (var ele in deactive)
        // {
        //     ele.SetActive(true);
        // }
        endRoundUi.SetActive(false);
        SceneManager.LoadScene(currentLevelName);
    }

    public void Menu()
    {
        SceneManager.LoadScene("0.Level");

    }

    
    
    
}
