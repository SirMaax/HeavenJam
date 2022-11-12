using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CloudController : MonoBehaviour
{
    [SerializeField] private bool isUpCloud;
    [SerializeField] private bool allClouds;
    [SerializeField] private bool reverseSpeed;

    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int amountOfClouds;
    [SerializeField] private float spawnPositionXLeft;
    [SerializeField] private float spawnPositionXRight;

    [SerializeField] private float spawnPositionYDown;
    [SerializeField] private float spawnPositionYUp;

    [SerializeField] private float yRandom;
    [SerializeField] private float baseCloudSpeed;
    [SerializeField] private float maxTimeBetweenClouds;
    [SerializeField] private float minTimeBetweenClouds;
    [SerializeField] private float minusScale;
    [SerializeField] private float baseScale;

    [SerializeField] private bool useTransformsForUpAndDown;
    [SerializeField] private Transform[] all;
    
    
    private int leftIndex, rightIndex;
    private GameObject[] leftCloud, rightCloud;
    private float[] rightCloudSpeed, leftCloudSpeed;
    private bool[] rightReverse,leftReverse;

    // Start is called before the first frame update
    void Start()
    {
        if (useTransformsForUpAndDown)
        {
            spawnPositionXLeft = all[0].position.x;
            spawnPositionXRight = all[1].position.x;
            spawnPositionYUp = all[2].position.y;
            spawnPositionYDown = all[3].position.y;
            // BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
            // spawnPositionXLeft = transform.position.x - collider2D.size.x / 2 ;
            // spawnPositionXRight = +transform.position.x + collider2D.size.x / 2;
            // spawnPositionYUp = transform.position.y + collider2D.size.y / 2;
            // spawnPositionYDown = transform.position.y - collider2D.size.y / 2;
            GetComponent<SpriteRenderer>().enabled = false;


        }
        Init();
        InitalCloudRelease();
        if(!allClouds) StartCoroutine(ReleaseClouds());
    }

    // Update is called once per frame
    void Update()
    {
        MoveSprites();
    }

    private void MoveSprites()
    {
        for (int i = 0; i < amountOfClouds / 2; i++)
        {
            if (!leftCloud[i].activeSelf) continue;
            if (leftCloud[i].transform.position.x < spawnPositionXLeft ||
                leftCloud[i].transform.position.x > spawnPositionXRight)

    {
                
                if (!reverseSpeed)
                {
                    Vector2 currentPos = leftCloud[i].transform.position;
                    currentPos.x = spawnPositionXLeft;
                    leftCloud[i].transform.position = currentPos;
                }
                else
                {
                    leftReverse[i] = !leftReverse[i];
                }
                
                
            }
            if(leftReverse[i]) leftCloud[i].transform.Translate(new Vector3(leftCloudSpeed[i] * Time.deltaTime *-1, 0, 0));
            else leftCloud[i].transform.Translate(new Vector3(leftCloudSpeed[i] * Time.deltaTime, 0, 0));

            if (!rightCloud[i].activeSelf) continue;
            if (rightCloud[i].transform.position.x < spawnPositionXLeft ||
                rightCloud[i].transform.position.x > spawnPositionXRight)
            {
                if (!reverseSpeed)
                {
                Vector2 currentPos = rightCloud[i].transform.position;
                currentPos.x = spawnPositionXRight;
                rightCloud[i].transform.position = currentPos;
                }
                else
                {
                    rightReverse[i] = !rightReverse[i];
                }
            }
            if(rightReverse[i])rightCloud[i].transform.Translate(new Vector3(rightCloudSpeed[i] * Time.deltaTime, 0, 0));
            else rightCloud[i].transform.Translate(new Vector3(rightCloudSpeed[i] * Time.deltaTime * -1, 0, 0));
        }
    }


    private void Init()
    {
        leftCloud = new GameObject[amountOfClouds / 2];
        rightCloud = new GameObject[amountOfClouds / 2];
        leftCloudSpeed = new float[amountOfClouds / 2];
        rightCloudSpeed = new float[amountOfClouds / 2];
        rightReverse = new bool[amountOfClouds / 2];
        leftReverse = new bool[amountOfClouds / 2];

        for (int i = 0; i < amountOfClouds / 2; i++)
        {
            Vector2 pos = new Vector2(spawnPositionXLeft, Random.Range(spawnPositionYDown - yRandom, spawnPositionYUp + yRandom));
            GameObject newCloud = Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], pos, Quaternion.identity);
            float newScale = Random.Range(baseScale - minusScale, baseScale + minusScale);
            newCloud.transform.localScale = new Vector3(newScale, newScale, 0);
            if (Random.value < 0.5)
            {
                Vector3 scale = newCloud.transform.localScale;
                scale.x *= -1;
                newCloud.transform.localScale = scale;
            }
            Vector3 scale2 = newCloud.transform.localScale;
            if (isUpCloud) scale2.y *= -1;
            newCloud.transform.localScale = scale2;

            newCloud.SetActive(false);
            leftCloud[i] = newCloud;
            leftCloudSpeed[i] = Random.Range(baseCloudSpeed / 3, baseCloudSpeed * 3);
            pos = new Vector2(spawnPositionXRight, Random.Range(spawnPositionYDown - yRandom, spawnPositionYUp + yRandom));
            pos.x *= -1;
            newCloud = Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], pos, Quaternion.identity);
            newScale = Random.Range(baseScale - minusScale, baseScale + minusScale);
            newCloud.transform.localScale = new Vector3(newScale, newScale, 0);
            if (Random.value < 0.5)
            {
                Vector3 scale = newCloud.transform.localScale;
                scale.x *= -1;
                newCloud.transform.localScale = scale;
            }
            scale2 = newCloud.transform.localScale;
            if (isUpCloud) scale2.y *= -1;
            newCloud.transform.localScale = scale2;

            newCloud.SetActive(false);

            rightCloud[i] = newCloud;
            rightCloudSpeed[i] = Random.Range(baseCloudSpeed / 3, baseCloudSpeed * 3);
        }
    }

    private IEnumerator ReleaseClouds()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetweenClouds, maxTimeBetweenClouds));
        if (Random.value < 0.5 && leftIndex < leftCloud.Length)
        {
            leftCloud[leftIndex].SetActive(true);
            leftIndex++;
        }
        else if (rightIndex < rightCloud.Length)
        {
            rightCloud[rightIndex].SetActive(true);
            rightIndex++;
        }

        if (leftIndex >= leftCloud.Length && rightIndex >= rightCloud.Length)
        {
        }
        else
        {
            StartCoroutine(ReleaseClouds());
        }
    }

    private void InitalCloudRelease()
    {
        if (allClouds)
        {
            for (int i = 0; i < amountOfClouds; i++)
            {
                Vector2 newPos = leftCloud[i].transform.position;
                newPos.x = Random.Range(spawnPositionXLeft, spawnPositionXRight);
                leftCloud[i].transform.position = newPos;
                leftCloud[i].SetActive(true);
                newPos = rightCloud[i].transform.position;
                newPos.x = Random.Range(spawnPositionXLeft, spawnPositionXRight);
                rightCloud[i].transform.position = newPos;
                rightCloud[i].SetActive(true);
                leftIndex++;
                rightIndex++;
            }
        }
        else
        {
            for (int i = 0; i < amountOfClouds / 3; i++)
            {
                Vector2 newPos = leftCloud[i].transform.position;
                newPos.x = Random.Range(spawnPositionXLeft, spawnPositionXRight);
                leftCloud[i].transform.position = newPos;
                leftCloud[i].SetActive(true);
                newPos = rightCloud[i].transform.position;
                newPos.x = Random.Range(spawnPositionXLeft, spawnPositionXRight);
                rightCloud[i].transform.position = newPos;
                rightCloud[i].SetActive(true);
                leftIndex++;
                rightIndex++;
            } 
        }
        
    }
}