using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 pointA, pointB;
    private bool moveToPointB = true;
    private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementSpeed = 8 * Time.fixedDeltaTime;
        bool playerInRange = Vector2.Distance(transform.position, player.transform.position) < 6;
        Vector2 targetRoamMovementPoint = moveToPointB ? pointB : pointA;
        Vector2 target = playerInRange 
                           && (player.transform.position.x > Mathf.Min(pointA.x, pointB.x)) 
                           && (player.transform.position.x < Mathf.Max(pointA.x, pointB.x)) ? player.transform.position
                         : targetRoamMovementPoint;

        if (Vector2.Distance(transform.position, targetRoamMovementPoint) < 1)
        {
            moveToPointB = !moveToPointB;
        }

        movementSpeed *= target.x > this.transform.position.x ? 1 : -1;

        controller.Move(movementSpeed, false);
    }
}
