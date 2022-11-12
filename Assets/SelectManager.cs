using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{
    [Header("Selcted")] private MoveScript currentSelected;
    [SerializeField] private float timeAfterBeingAbleToSelectPosition;

    [Header("Other Stuff")] private bool canSetPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SelectNewPosition();
    }

    public void NewSelectedObject(MoveScript newObject)
    {
        if (currentSelected != null)
        {
            currentSelected.DeSelect();
        }

        canSetPosition = false;
        StartCoroutine(WaitBeforeMoving());
        currentSelected = newObject;
    }

    public void SelectNewPosition()
    {
        if (currentSelected != null && canSetPosition)
        {
            canSetPosition = true;
            if (Mouse.current.rightButton.wasPressedThisFrame && Keyboard.current.ctrlKey.isPressed)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    currentSelected.SetNewPatrolPoint(hit.point);
                }
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame && Keyboard.current.shiftKey.isPressed)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    currentSelected.SetNewPosition(hit.point);
                }
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    currentSelected.SetNewPositionWithClear(hit.point);
                }
            }
        }
    }

    private IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(timeAfterBeingAbleToSelectPosition);
        canSetPosition = true;
    }

    public void ClearSelection()
    {
        if (currentSelected != null)
        {
            currentSelected.DeSelect();
            currentSelected = null;
        }
    }
}