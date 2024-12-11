using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOOB : MonoBehaviour
{
    public GameObject respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("OnTriggerEnter");
        if (other.gameObject.TryGetComponent<FPPMvmtGameMovement>(out FPPMvmtGameMovement movement))
        {
            movement.SetPosition(respawnPosition.transform.position);
            movement.PlayRetryAnimation();
            print("Retry");
        }
    }
}
