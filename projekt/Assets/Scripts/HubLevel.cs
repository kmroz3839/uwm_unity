using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubLevel : MonoBehaviour
{
    public Actionable section01Door;
    public Light lightSection0;
    public Light lightSection1;

    // Start is called before the first frame update
    void Start()
    {
        if (CrossScene.finishedArea0)
        {
            lightSection0.color = Color.green;
        }
        if (CrossScene.finishedArea1)
        {
            lightSection1.color = Color.green;
        }

        if (true || (CrossScene.finishedArea0 && CrossScene.finishedArea1))
        {
            section01Door.Action();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
