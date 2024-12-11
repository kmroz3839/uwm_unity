using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSpawnObject : Actionable
{
    public GameObject spawnObject;

    private GameObject currentSpawnedObject = null;

    public override void Action()
    {
        if (currentSpawnedObject != null) {
            Destroy(currentSpawnedObject);
        }
        currentSpawnedObject = Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
