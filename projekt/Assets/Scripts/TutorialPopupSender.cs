using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopupSender : MonoBehaviour
{
    public TutorialPopupScript tutorialCanvas;
    [TextArea(3, 10)]
    public string text;

    private void OnTriggerEnter(Collider other)
    {
        tutorialCanvas.ShowTutorial(text);
        GameObject.Destroy(gameObject);
    }
}
