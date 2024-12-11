using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTreadmill : MovableOverTime
{
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentLoc = Positions[timer.currentTimeSegment % Positions.Length];
        Vector3 nextLoc = Positions[(timer.currentTimeSegment + 1) % Positions.Length];

        Vector3 movementBefore = this.transform.position;
        Vector3 diff = nextLoc - currentLoc;

        this.transform.position = currentLoc + diff * (timer.segmentTime / timer.timePerSegment);
        Vector3 prevDeltaMovement = this.transform.position - movementBefore;
        deltaMovement = direction * Time.deltaTime + prevDeltaMovement;
    }
}
