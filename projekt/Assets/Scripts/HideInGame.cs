using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInGame : MonoBehaviour
{
    public bool hide = true;

    public void Start()
    {
        this.GetComponent<Renderer>().enabled = !hide;
    }
}
