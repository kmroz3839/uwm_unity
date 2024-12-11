using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public FPPMvmtGameMovement player;

    public Text statusText;
    public Slider timeSlider;
    public Image timeStopOverlay;
    public Text timerText;
    public GameObject[] crosshairs;
    public Image wallJumpIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeStopOverlay.color = player.timeStopped ? new Color32(0x1D, 0x00, 0x1B, 40) : new Color32(0x0F, 0x0F, 0x0F, 80);
        timeStopOverlay.enabled = player.timeStopped || player.stageStopped;
        timeSlider.interactable = player.timeStopped;
        statusText.text = player.timeStopped ? "\u23f9Stop" : "\u25b6Play";

        if (player.timeManager != null){
            timerText.text = player.timeManager.currentTime.ToString("00.000", System.Globalization.CultureInfo.InvariantCulture) + "s";
        } else {
            timerText.text = "---";
        }

        crosshairs[0].SetActive(!player.canWallJump.Value);
        crosshairs[1].SetActive(player.canWallJump.Value);
        wallJumpIcon.color = player.canWallJump.Value ? Color.cyan : Color.black;

        if (player.timeStopped)
        {
            timeSlider.value = Mathf.Clamp(timeSlider.value + Input.GetAxis("Horizontal") * 0.5f * Time.deltaTime, 0, 1);
        }
    }
}
