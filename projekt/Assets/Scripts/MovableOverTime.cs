using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovableOverTime : MonoBehaviour
{
    [Header("Assign this in inspector")]
    public Vector3[] Positions;

    public TimeManager timer;

    public Vector3 deltaMovement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentLoc = Positions[timer.currentTimeSegment % Positions.Length];
        Vector3 nextLoc = Positions[(timer.currentTimeSegment + 1) % Positions.Length];

        Vector3 movementBefore = this.transform.position;
        Vector3 diff = nextLoc - currentLoc;

        this.transform.position = currentLoc + diff * (timer.segmentTime / timer.timePerSegment);
        deltaMovement = this.transform.position - movementBefore;
        
    }
}
