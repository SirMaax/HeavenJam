using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    private SelectManager _selectManager;
    void Start()
    {
        _selectManager = GameObject.FindWithTag("SelectManager").GetComponent<SelectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _selectManager.ClearSelection();
        }
    }
}
