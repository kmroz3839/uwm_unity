using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCubesGenerator : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();
    public float delay = 3.0f;
    int objectCounter = 0;
    // obiekt do generowania
    public GameObject block;

    public int numGenerateObjects = 10;
    public Material[] objMaterials;

    void Start()
    {
        int planeSizeX = (int)(this.transform.localScale.x * 5);
        int planeSizeZ = (int)(this.transform.localScale.z * 5);
        print("scale:" + this.transform.localScale);

        // w momecie uruchomienia generuje 10 kostek w losowych miejscach
        List<int> pozycje_x = new List<int>(Enumerable.Range(0, numGenerateObjects).OrderBy(x => Guid.NewGuid()).Take(numGenerateObjects));
        List<int> pozycje_z = new List<int>(Enumerable.Range(0, numGenerateObjects).OrderBy(x => Guid.NewGuid()).Take(numGenerateObjects));

        for (int i = 0; i < numGenerateObjects; i++)
        {
            this.positions.Add(new Vector3(pozycje_x[i]%planeSizeX - planeSizeX/2, 5, pozycje_z[i]%planeSizeZ - planeSizeZ/2) + this.transform.position);
        }
        foreach (Vector3 elem in positions)
        {
            Debug.Log(elem);
        }
        // uruchamiamy coroutine
        StartCoroutine(GenerujObiekt());
    }

    void Update()
    {

    }

    IEnumerator GenerujObiekt()
    {
        Debug.Log("wywo�ano coroutine");
        System.Random rnd = new System.Random();
        foreach (Vector3 pos in positions)
        {
            GameObject newObj = Instantiate(this.block, this.positions.ElementAt(this.objectCounter++), Quaternion.identity);
            newObj.GetComponent<Renderer>().material = objMaterials[rnd.Next(objMaterials.Length)];
            yield return new WaitForSeconds(this.delay);
        }
        // zatrzymujemy coroutine
        StopCoroutine(GenerujObiekt());
    }
}