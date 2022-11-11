using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class SoulMovement : MonoBehaviour
{
    [Header("Status")] 
    public bool isInRangeFromDog;
    [SerializeField] private float movementSpeed;
    [SerializeField] float timeBetweenRandomMovementMin;
    [SerializeField] float timeBetweenRandomMovementMax;
    private bool canMoveRandomly;
    private Vector2 fleeDirection;
    // Start is called before the first frame update

    [Header("Refs")] private DogManager _dogManager;
    void Start()
    {
        _dogManager = GameObject.FindGameObjectWithTag("DogManager").GetComponent<DogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        FleeFromDogs();
        Move();
    }

    private void RandomMovement()
    {
        if (!isInRangeFromDog && canMoveRandomly)
        {
            //Moverandomly
            canMoveRandomly = false;
            
            //Get new Target for walking
        }
    }

    private IEnumerator CooldownMoveRandomly()
    {
        float randomTime = Random.Range(timeBetweenRandomMovementMin, timeBetweenRandomMovementMax);
        yield return new WaitForSeconds(randomTime);
        canMoveRandomly = true;
    }

    private void FleeFromDogs()
    {
        DogScript[] dogs = _dogManager.GetAllDogs();
        ArrayList dogsInRadius = new ArrayList();
        Vector2 newDirection = Vector2.zero;
        Vector2 toCirlce = Vector2.zero;
        foreach (var dog in dogs)
        {
            
            if ((dog.GetPosition() - (Vector2)transform.position).magnitude < dog.dogRadius)
            {
                // newDirection += (Vector2)transform.position - dog.GetPosition() ;
                toCirlce = (((Vector2)transform.position - dog.GetPosition()).normalized) * (dog.dogRadius + dog.dogRadius/2) ;
                // toCirlce = (((Vector2)transform.position - dog.GetPosition())) * dog.dogRadius;

                Debug.DrawRay(dog.GetPosition(),toCirlce,Color.yellow);
                // dogsInRadius.Add(dog.GetPosition());
                newDirection += toCirlce;
            }
        
        }
            Debug.DrawRay(transform.position, newDirection,Color.green);
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
        // if (dogsInRadius.Count != 0)
        // {
        //     
        // }
    }

    private void Move()
    {
        Debug.DrawLine(transform.position,fleeDirection +(Vector2) transform.position,Color.cyan);
        if (isInRangeFromDog)
        {
            fleeDirection = fleeDirection.normalized;
            fleeDirection *= movementSpeed * Time.deltaTime;
            transform.Translate(fleeDirection);
        }
    }
}
