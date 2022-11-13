using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MadDog : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")] 
    [SerializeField] private float speed;
    private Vector2 target;
    private Vector2 direction;
    private Vector2 speedVector;
    private Vector2 lastPos;
    void Start()
    {
        //SetInitalVector]
        SetNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        speedVector =(Vector2) transform.position - lastPos;
        lastPos = transform.position;
        Movement();
        //CHeck for wall
        ClampMovement();
        FlipSprite();
    }

    private void ClampMovement()
    {
        Vector2 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, -MoveScript.XClampStatic, MoveScript.XClampStatic);
        currentPos.y = Mathf.Clamp(currentPos.y, -MoveScript.YClampStatic, MoveScript.YClampStatic);
        if (MoveScript.XClampStatic - Mathf.Abs(currentPos.x) < 0.1 ||
            MoveScript.YClampStatic - Mathf.Abs(currentPos.y) < 0.1)
        {
            //Reached edge
            SetNewTarget();
        }
        transform.position = new Vector3(currentPos.x, currentPos.y, -1);
    }
    
    private void Movement()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private void SetNewTarget()
    {
        GameObject[] sheeps = GameObject.FindGameObjectsWithTag("Soul");
        Vector2 medium = Vector2.zero;
        foreach (var sheep in sheeps)
        {
            medium +=(Vector2) sheep.transform.position;
        }

        medium /= sheeps.Length;
        target = medium;
        direction = target - (Vector2)transform.position;
        direction = direction.normalized;
        direction *= speed;
    }
    
    private void FlipSprite()
    {
        if (speedVector.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -1;
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        
    }
    
    
}
