using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    [SerializeField] public float dogRadius;
    
    // Start is called before the first frame update
    private LineRenderer lr;

    void Start()
    {
        // LineRenderInit();
    }



    // Update is called once per frame
    void Update()
    {
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    // private void DisplayRange()
    // {
    //     float x;
    //     float y;
    //
    //     float angle = 20f;
    //     int segments = 50;
    //     for (int i = 0; i < (segments + 1); i++)
    //     {
    //         x = Mathf.Sin (Mathf.Deg2Rad * angle) * dogRadius;
    //         y = Mathf.Cos (Mathf.Deg2Rad * angle) * dogRadius;
    //
    //         lr.SetPosition (i,new Vector3(x,y,-2) );
    //
    //         angle += (360f / segments);
    //     }
    // }
    // private void LineRenderInit()
    // {
    //     lr = GetComponent<LineRenderer>();
    //     lr.positionCount = (50 + 1);
    //     lr.useWorldSpace = false;
    //     lr.loop = true;
    //     lr.widthCurve = AnimationCurve.Constant(1, 1, .2f);
    //     DisplayRange();
    // }
}