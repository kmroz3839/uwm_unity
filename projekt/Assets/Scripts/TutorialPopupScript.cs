using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopupScript : MonoBehaviour
{
    Animator anim;
    Text text;

    private void Start()
    {
        anim = GetComponent<Animator>();
        text = GetComponentInChildren<Text>();
    }
    public void ShowTutorial(string tutorialText)
    {
        text.text = tutorialText;
        anim.Play("animTutorialPopup", -1, 0);
    }
}
