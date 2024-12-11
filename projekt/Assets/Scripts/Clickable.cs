using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public Actionable target;

    public virtual void Click() {
        print("Clickable clicked");
        if (target != null) {
            target.Action();
        }
    }
}
