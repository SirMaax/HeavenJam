using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("GameInfo")] [SerializeField] public float soulsInGate;
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
            soulsInGate++;
            Destroy(collision.gameObject);
            GameController.amountOfEscapedSouls++;
            _audioManager.PlaySound(6);

        }
    }
}
