using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableButton : Clickable
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Click()
    {
        base.Click();
        anim.Play("animButtonActivated", -1, 0);
    }
}
