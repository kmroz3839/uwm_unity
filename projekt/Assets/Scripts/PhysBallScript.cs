using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysBallScript : MonoBehaviour
{
    private Rigidbody rb;
    public float raycastDistance = 2f;

    KeyValuePair<bool, RaycastHit> RTAndDebug(Vector3 from, Vector3 dir, float distance)
    {
        bool ret = Physics.Raycast(from, dir, out RaycastHit hit, distance, LayerMask.GetMask("Default"));
        Debug.DrawRay(from, dir*distance, ret ? Color.green : Color.red);
        return new KeyValuePair<bool, RaycastHit>(ret, hit);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var groundedToV = RTAndDebug(transform.position, -Vector3.up, raycastDistance);
        MovableOverTime groundedTo = null;
        bool grounded = groundedToV.Key;
        if (grounded) {
            groundedToV.Value.transform.TryGetComponent<MovableOverTime>(out groundedTo);
        }
        if (grounded && groundedTo != null) {
            //this.transform.position += groundedTo.deltaMovement;
            rb.AddForce(groundedTo.deltaMovement * 1.5f, ForceMode.Impulse);
        }
    }
}
