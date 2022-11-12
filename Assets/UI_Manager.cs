using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] portraits;

    void Start()
    {
        GameObject[] allDogs = GameObject.FindGameObjectsWithTag("Dog");
        if (allDogs.Length == 1)
        {
            portraits[3].SetActive(false);
            portraits[5].SetActive(false);
        }
        else if (allDogs.Length == 2)
        {
            portraits[3].SetActive(true);
            portraits[5].SetActive(false);
        }
        else if (allDogs.Length == 3)
        {
            portraits[3].SetActive(true);
            portraits[5].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TogglePortrait(int index)
    {
        if (portraits[index].activeSelf)
        {
            portraits[index].SetActive(false);
            portraits[index + 1].SetActive(true);
        }
        else
        {
            portraits[index].SetActive(true);
            portraits[index + 1].SetActive(false);
        }
    }

    public void DisplayNextLevelStuff()
    {
        //
    }
}