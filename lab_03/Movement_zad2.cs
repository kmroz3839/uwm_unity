using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;

    private bool moveXPlus = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3((moveXPlus ? 1 : -1) * speed * Time.deltaTime, 0));
        if (transform.position.x >= 10 || transform.position.x <= -10)
        {
            moveXPlus = transform.position.x < 0;
        }
    }
}
