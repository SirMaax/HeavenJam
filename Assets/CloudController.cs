using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CloudController : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int amountOfClouds;
    [SerializeField] private float spawnPositionX;
    [SerializeField] private float spawnPositionY;
    [SerializeField] private float yRandom;
    [SerializeField] private float baseCloudSpeed;
    [SerializeField] private float maxTimeBetweenClouds;
    [SerializeField] private float minTimeBetweenClouds;
    [SerializeField] private float minusScale;
    [SerializeField] private float baseScale;
    private int leftIndex, rightIndex;
    private GameObject[] leftCloud, rightCloud;
    private float[] rightCloudSpeed, leftCloudSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Init();
        InitalCloudRelease();
        StartCoroutine(ReleaseClouds());
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
            if (Mathf.Abs(leftCloud[i].transform.position.x) > spawnPositionX)
            {
                Vector2 currentPos = leftCloud[i].transform.position;
                currentPos.x = -spawnPositionX;
                leftCloud[i].transform.position = currentPos;
            }

            leftCloud[i].transform.Translate(new Vector3(leftCloudSpeed[i] * Time.deltaTime, 0, 0));

            if (!rightCloud[i].activeSelf) continue;
            if (Mathf.Abs(rightCloud[i].transform.position.x) > spawnPositionX)
            {
                Vector2 currentPos = rightCloud[i].transform.position;
                currentPos.x = spawnPositionX;
                rightCloud[i].transform.position = currentPos;
            }

            rightCloud[i].transform.Translate(new Vector3(rightCloudSpeed[i] * Time.deltaTime * -1, 0, 0));
        }
    }


    private void Init()
    {
        leftCloud = new GameObject[amountOfClouds / 2];
        rightCloud = new GameObject[amountOfClouds / 2];
        leftCloudSpeed = new float[amountOfClouds / 2];
        rightCloudSpeed = new float[amountOfClouds / 2];

        for (int i = 0; i < amountOfClouds / 2; i++)
        {
            Vector2 pos = new Vector2(spawnPositionX, Random.Range(spawnPositionY - yRandom, spawnPositionY + yRandom));
            GameObject newCloud = Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], pos, Quaternion.identity);
            float newScale = Random.Range(baseScale - minusScale, baseScale + minusScale);
            newCloud.transform.localScale = new Vector3(newScale, newScale, 0);
            if (Random.value < 0.5)
            {
                Vector3 scale = newCloud.transform.localScale;
                scale.x *= -1;
                newCloud.transform.localScale = scale;
            }

            newCloud.SetActive(false);
            leftCloud[i] = newCloud;
            leftCloudSpeed[i] = Random.Range(baseCloudSpeed / 3, baseCloudSpeed * 3);
            pos = new Vector2(spawnPositionX, Random.Range(spawnPositionY - yRandom, spawnPositionY + yRandom));
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
        for (int i = 0; i < amountOfClouds / 3; i++)
        {
            Vector2 newPos = leftCloud[i].transform.position;
            newPos.x = Random.Range(-spawnPositionX, spawnPositionX);
            leftCloud[i].transform.position = newPos;
            leftCloud[i].SetActive(true);
            newPos = rightCloud[i].transform.position;
            newPos.x = Random.Range(-spawnPositionX, spawnPositionX);
            rightCloud[i].transform.position = newPos;
            rightCloud[i].SetActive(true);
            leftIndex++;
            rightIndex++;
        }
    }
}