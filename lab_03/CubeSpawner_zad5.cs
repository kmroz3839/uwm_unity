using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;

    // Start is called before the first frame update
    void Start()
    {
        List<int> placedPositions = new List<int>();
        for (int x = 0; x < 10; x++)
        {
            int randomX = Random.Range(0, 10);
            int randomY = Random.Range(0, 10);
            int c = randomX + randomY << 8;
            while (placedPositions.Contains(c))
            {
                randomX = Random.Range(0, 10);
                randomY = Random.Range(0, 10);
                c = randomX + randomY << 8;
            }
            placedPositions.Add(c);
            Instantiate(cubePrefab, new Vector3(randomX, 0, randomY) - new Vector3(5,0,5), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
