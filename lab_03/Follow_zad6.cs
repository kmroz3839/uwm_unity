using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float vel = 1;
        //x = Mathf.SmoothDamp(x, target.transform.position.x, ref vel, 0.05f);
        x = Mathf.Lerp(x, target.transform.position.x, 1f * Time.deltaTime);
        this.transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
