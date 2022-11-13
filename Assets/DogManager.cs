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

        for (int i = 0; i < +madDogs.Length; i++)
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

    public Vector2 GetNearestMagDog(Vector2 position)
    {
        Vector2 nearest = Vector2.zero;
        float distance = Mathf.Infinity;
        foreach (var dog in allDogs)
        {
            
            if (dog.typeOfDog == 1)
            {
                float temp = ((Vector2)dog.transform.position - position).magnitude;
                if (temp < distance)
                {
                    nearest = dog.transform.position;
                    distance = temp;
                }
            }
        }
        return nearest;
    }
}