using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayAnimation : Actionable
{
    private Animator anim;
    public string animationName;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Action()
    {
        anim.Play(animationName, -1, 0);
    }
}
