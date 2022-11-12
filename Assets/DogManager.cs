using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
    private DogScript[] allDogs;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Dog");
        GameObject[] madDogs = GameObject.FindGameObjectsWithTag("MadDog");
        allDogs = new DogScript[list.Length + madDogs.Length];
        for (int i = 0; i < list.Length; i++)
        {
            allDogs[i] = list[i].GetComponent<DogScript>();
        } 
        for (int i = 0; i <  + madDogs.Length; i++ )
        {
            allDogs[i + list.Length] = madDogs[i].GetComponent<DogScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DogScript[] GetAllDogs()
    {
        return allDogs;
    }
}
