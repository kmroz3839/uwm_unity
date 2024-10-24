using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.transform.forward * speed * Time.deltaTime, Space.World);
        print(this.transform.forward);
        if (Vector3.Distance(startPos, transform.position) >= 10)
        {
            startPos = transform.position;
            transform.Rotate(new Vector3(0,90,0));
        }
    }
}
