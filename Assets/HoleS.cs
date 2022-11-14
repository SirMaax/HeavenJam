using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleS : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Soul"))
        {
            Destroy(collision.gameObject);
            GameController.amountOfFallenSouls++;
            _audioManager.PlaySound(3);
        }
    }
}
