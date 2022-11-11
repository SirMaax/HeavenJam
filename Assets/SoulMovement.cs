using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoulMovement : MonoBehaviour
{
    [Header("Status")] 
    public bool isInRangeFromDog;
    [SerializeField] float timeBetweenRandomMovementMin;
    [SerializeField] float timeBetweenRandomMovementMax;
    private bool canMoveRandomly;
    
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxSpeedMulitplier;

    [Header("RandomMovenetn")] private Vector2 nextRandomTarget;
    [SerializeField] private float xRandomMovement;
    [SerializeField] private float yRandomMovement;
    
    [Tooltip("Speed = random between Basespeed minus and plus 1/10 of Basespeed")]
    [SerializeField] private float percentageOfBaseMovementSpeedMultiplier;
    private Vector2 fleeDirection;

    private float smallestDistanceToDog;
    // Start is called before the first frame update

    [Header("Refs")] private DogManager _dogManager;
    void Start()
    {
        movementSpeed = Random.Range(movementSpeed - (movementSpeed / percentageOfBaseMovementSpeedMultiplier)
            , movementSpeed + (movementSpeed / percentageOfBaseMovementSpeedMultiplier));
        _dogManager = GameObject.FindGameObjectWithTag("DogManager").GetComponent<DogManager>();
        smallestDistanceToDog = Mathf.Infinity;
        canMoveRandomly = true;
    }

    // Update is called once per frame
    void Update()
    {
        FleeFromDogs();
        Move();
        MoveRandomly();
        SetNewRandomTarget();
    }

    private void SetNewRandomTarget()
    {
        if (!isInRangeFromDog && canMoveRandomly)
        {
            //Moverandomly
            canMoveRandomly = false;
            
            //Get new Target for walking
            Vector2 currentPos = transform.position;
            
            DogScript[] dogs = _dogManager.GetAllDogs();
            bool newPointIsInDogRadius = false;
            int runsThroughLoop = 0;
            do
            {
                newPointIsInDogRadius = false;
                float newX = Random.Range(currentPos.x - xRandomMovement, currentPos.x + xRandomMovement);
                float newY = Random.Range(currentPos.y - yRandomMovement, currentPos.y + yRandomMovement);
                nextRandomTarget = new Vector2(newX, newY);

                foreach (var dog in dogs)
                {
                    if ((nextRandomTarget - dog.GetPosition()).magnitude < dog.dogRadius)
                    {
                        newPointIsInDogRadius = true;
                        break;
                    }
                }

                runsThroughLoop++;
                if (runsThroughLoop >= 50) return;
            } while (newPointIsInDogRadius);
            
            
            
        }
    }

    private void MoveRandomly()
    {
        if (!isInRangeFromDog && nextRandomTarget != Vector2.zero)
        {
            Vector2 direction = nextRandomTarget -(Vector2) transform.position;
            if (direction.magnitude < 0.1f)
            {
                transform.position = nextRandomTarget;
                nextRandomTarget = Vector2.zero;
                StartCoroutine(CooldownMoveRandomly());
            }
            else if (direction.magnitude > 10f)
            {
                nextRandomTarget = Vector2.zero;
                StartCoroutine(CooldownMoveRandomly());
            }
            else
            {
            direction = direction.normalized;
            direction *= movementSpeed/2 * Time.deltaTime;
            transform.Translate(direction);
            }
        }
    }

    private IEnumerator CooldownMoveRandomly()
    {
        canMoveRandomly = false;
        float randomTime = Random.Range(timeBetweenRandomMovementMin, timeBetweenRandomMovementMax);
        yield return new WaitForSeconds(randomTime);
        canMoveRandomly = true;
    }

    private void FleeFromDogs()
    {
        DogScript[] dogs = _dogManager.GetAllDogs();
        Vector2 newDirection = Vector2.zero;
        Vector2 toCirlce = Vector2.zero;
        smallestDistanceToDog = Mathf.Infinity;
        foreach (var dog in dogs)
        {
            float distanceToDog = (dog.GetPosition() - (Vector2)transform.position).magnitude;
            if (distanceToDog < smallestDistanceToDog) smallestDistanceToDog = distanceToDog;
            if (distanceToDog < dog.dogRadius)
            {
                toCirlce = (((Vector2)transform.position - dog.GetPosition()).normalized) * (dog.dogRadius) ;
                newDirection += toCirlce;
            }
            // else if (distanceToDog < dog.dogRadius + dog.dogRadius/3)
            // {
            //     toCirlce = (((Vector2)transform.position - dog.GetPosition()).normalized) * 
            //                (Random.Range(dog.dogRadius/5,dog.dogRadius/2)) ;
            //     newDirection += toCirlce;
            // }
        
        }
        if (toCirlce != Vector2.zero)
        {
            isInRangeFromDog = true;
            fleeDirection = newDirection ;
        }
        else
        {
            isInRangeFromDog = false;
            fleeDirection = Vector2.zero;
        }
   
    }

    private void Move()
    {
        Debug.DrawLine(transform.position,fleeDirection +(Vector2) transform.position,Color.cyan);
        if (isInRangeFromDog)
        {
            fleeDirection = fleeDirection.normalized;
            
            float extraSpeedPercentage = ((maxSpeedMulitplier-100)/-5 * smallestDistanceToDog + maxSpeedMulitplier)/100;
            fleeDirection *= movementSpeed * extraSpeedPercentage * Time.deltaTime;
            transform.Translate(fleeDirection);
        }
    }

    public void IsInBarkingRange(Vector3 DogPosition)
    {
    }
}
