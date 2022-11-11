using System;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [Header("Variabl")] [SerializeField] private float movementSpeed;
    [SerializeField] private float distanceTillSnapToTarget;
    [Header("Status")] 
    public bool selected;
    public bool canMove;
    public bool moving;
    private Vector2 targetPosition;

    [Header("Refs")] // Start is called before the first frame update
    [SerializeField] private LayerMask floor;
    private SelectManager _selectManager;
    void Start()
    {
        _selectManager = GameObject.FindWithTag("SelectManager").GetComponent<SelectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPos();
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            selected = true;
            _selectManager.NewSelectedObject(this);
            print("selected");
        }
    }

    private void MoveToPos()
    {
        if (moving)
        {
            //Check distance
            Vector2 direction = targetPosition - (Vector2)transform.position;
            if (direction.magnitude < distanceTillSnapToTarget)
            {
                transform.position = targetPosition;
                moving = false;
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
        this.targetPosition = newTargetPosition;
    }
}
