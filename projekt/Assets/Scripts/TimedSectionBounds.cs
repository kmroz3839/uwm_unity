using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSectionBounds : MonoBehaviour
{

    public TimeManager timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        if (other.gameObject.TryGetComponent<FPPMvmtGameMovement>(out FPPMvmtGameMovement movement))
        {
            movement.ui.timeSlider.value = timer.currentTime / timer.maxTime;
            if (movement.timeManager != null)
            {
                movement.timeManager.player = null;
            }
            movement.timeManager = timer;
            timer.player = movement;
        }
    }
}
