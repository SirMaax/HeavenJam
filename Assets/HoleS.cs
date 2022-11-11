using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("yes");
        if (collision.gameObject.tag.Equals("Soul"))
        {
            Destroy(collision.gameObject);
            GameController.amountOfFallenSouls++;
        }
    }
}
