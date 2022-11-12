using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    [Tooltip("0 is the normal dog, 1 is the mad dog. Important to set!")]
    [SerializeField] public int typeOfDog;
    [SerializeField] public float dogRadius;
    [SerializeField] private bool showRadius; 
    [Header("RaysDectection")] [SerializeField]
    private LayerMask soulMask;
    [SerializeField] private float rayCastDistance;
    [SerializeField] private float angleStep;
    [SerializeField] private int amountOfRays;
    
    
    
    private Vector2 direction;
    private Vector2 lastFramePosition;
    // Start is called before the first frame update
    private LineRenderer lr2;
    [SerializeField] private GameObject LineRendererObject;

    void Start()
    {
        
        if(typeOfDog==0 ) LineRenderInit();
        
    }



    // Update is called once per frame
    void Update()
    {
        if (typeOfDog == 1) return;
        direction = (Vector2)transform.position - lastFramePosition;
        lastFramePosition = transform.position;
        CastRays();
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void DisplayRange()
    {
        float x;
        float y;
    
        float angle = 20f;
        int segments = 50;
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * dogRadius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * dogRadius;
    
            lr2.SetPosition (i,new Vector3(x,y,-2) );
    
            angle += (360f / segments);
        }
    }
    private void LineRenderInit()
    {
        lr2 = LineRendererObject.GetComponent<LineRenderer>();
        lr2.positionCount = (50 + 1);
        lr2.useWorldSpace = false;
        lr2.loop = true;
        lr2.widthCurve = AnimationCurve.Constant(1, 1, .2f);
        DisplayRange();
        if(!showRadius)lr2.gameObject.SetActive(false);
    }

    private void CastRays()
    {
        for (int i = 0; i < amountOfRays; i++)
        {
            // Quaternion angle = Quaternion.AngleAxis(angleStep * i,Vector3.up);
            
            Vector2 newDir = Rotate(direction,i * angleStep);
            Vector2 newDir2 = Rotate(direction,i * -angleStep);
            RaycastHit2D[] hit1 = Physics2D.RaycastAll(transform.position, newDir,
                rayCastDistance, soulMask); 
            foreach (var contact in hit1)
            {
                contact.collider.GetComponent<SoulMovement>().IsInBarkingRange(transform.position);
            }
            hit1 = Physics2D.RaycastAll(transform.position, newDir2, rayCastDistance, soulMask);
            
            foreach (var contact in hit1)
            {
                contact.collider.GetComponent<SoulMovement>().IsInBarkingRange(transform.position);
            }
            Debug.DrawRay(transform.position,newDir.normalized * rayCastDistance, Color.green);
            Debug.DrawRay(transform.position,newDir2.normalized * rayCastDistance, Color.magenta);

        }

    }
    
    public Vector2 Rotate(Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}