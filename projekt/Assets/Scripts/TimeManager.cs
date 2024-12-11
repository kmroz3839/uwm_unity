using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public float maxTime;
    public int timeSegments;

    [Header("Automatically calculated :: Do not modify")]
    public float segmentTime = 0;
    public float currentTime = 0;
    public int currentTimeSegment = 0;
    public float timePerSegment = 1;    

    public FPPMvmtGameMovement player;


    public void Start()
    {
        timePerSegment = maxTime / timeSegments;
    }

    public void Update()
    {
        if (player != null)
        {
            currentTime = player.ui.timeSlider.value * maxTime;
            
        }

        if (player == null || !player.stageStopped)
        {
            currentTime += Time.deltaTime;
            if (currentTime / maxTime > 0)
            {
                currentTime -= maxTime * ((int)(currentTime / maxTime));
            }
        }

        currentTimeSegment = 0;
        float ctime = timePerSegment;
        while (ctime < currentTime)
        {
            ctime += timePerSegment;
            currentTimeSegment++;
        }
        if (player != null && !player.stageStopped)
        {
            player.ui.timeSlider.value = currentTime / maxTime;
        }

        segmentTime = currentTime - currentTimeSegment * timePerSegment;
    }

    
}
