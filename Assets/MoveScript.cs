using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [Header("Variabl")] [SerializeField] private float movementSpeed;
    [SerializeField] private float distanceTillSnapToTarget;
    [SerializeField] private float xClampValue;
    [SerializeField] private float yClampValue;
    public static float XClampStatic;
    public static float YClampStatic;
    [Header("Status")] 
    public bool selected;
    public bool moving;
    private Queue<Vector2> targetPosition;
     LineRenderer lr;
     private Animator _animator;
    [Header("Refs")] // Start is called before the first frame update
    [SerializeField] private LayerMask floor;
    private SelectManager _selectManager;
    private Vector2 direction;
    private Vector2 lastPos;
    void Start()
    {
        targetPosition = new Queue<Vector2>();
        _selectManager = GameObject.FindWithTag("SelectManager").GetComponent<SelectManager>();
        lr = GetComponent<LineRenderer>();
        lr.startColor = Color.red;
        lr.positionCount = 1;
        lr.useWorldSpace = true;
        lr.widthCurve = AnimationCurve.Constant(1, 1, .2f);
        XClampStatic = xClampValue;
        YClampStatic = yClampValue;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        direction = (Vector2)transform.position - lastPos;
        lastPos = transform.position;
        FlipSprite();
        
        if(moving)_animator.SetFloat("Speed", 1);
        else
        {
            _animator.SetFloat("Speed", 0);
        }
        MoveToPos();
        LineRendererUpdate();
        ClampMovement();
    }

    private void ClampMovement()
    {
        Vector2 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, -xClampValue, xClampValue);
        currentPos.y = Mathf.Clamp(currentPos.y, -yClampValue, yClampValue);
        transform.position = new Vector3(currentPos.x, currentPos.y, -1);

    }
    
    private void OnMouseDown()
    {
        if (!selected)
        {
            selected = true;
            _selectManager.NewSelectedObject(this);
        }
    }

    private void MoveToPos()
    {
        if (moving)
        {
            //Check distance
            Vector2 direction = targetPosition.Peek() - (Vector2)transform.position;
            if (direction.magnitude < distanceTillSnapToTarget)
            {
                transform.position = targetPosition.Peek();
                targetPosition.Dequeue();
                UpdatePositionsInLineRenderer();
                if (targetPosition.Count == 0)
                {
                moving = false;
                }
            }
            else
            {
            direction = direction.normalized;
            direction =  movementSpeed * Time.deltaTime * direction;
            transform.Translate(direction);
            
            }
            
        }
        
        
        
    }

   

    public void DeSelect()
    {
        selected = false;
    }

    public void SetNewPosition(Vector2 newTargetPosition)
    {
        moving = true;
        if (targetPosition.Count == 100) return;
        targetPosition.Enqueue(newTargetPosition);
        UpdatePositionsInLineRenderer();
    }

    public void SetNewPositionWithClear(Vector2 newTargetPosition)
    {
        targetPosition.Clear();
        lr.SetPositions(new Vector3[100]);
        SetNewPosition(newTargetPosition);
    }

    private void LineRendererUpdate()
    {
        lr.SetPosition(0,transform.position);
        if (selected)
        {
            // lr.enabled = true;
        }
        else
        {
            // lr.enabled = false;
        }
    }

    private void UpdatePositionsInLineRenderer()
    {
        Vector3[] tempArray = new Vector3[targetPosition.Count + 1];
        int index = 1;
        tempArray[0] = transform.position;
        foreach (var ele in targetPosition)
        {
            
            tempArray[index] = new Vector3(ele.x, ele.y, -1);
            index++;
        }
        lr.positionCount = targetPosition.Count + 1;
        lr.SetPositions(tempArray);
    }

    private void FlipSprite()
    {
        if (direction.x < 0)
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
