using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwaying : MonoBehaviour
{
    Vector2 deltaMovement = new Vector2(0, 0);
    private Vector2 dampDeltaMovement = new Vector2(0, 0);
    RectTransform ttr;
    public Vector2 originPoint;

    public Vector2 swayStrength = new Vector2(8,8);

    // Start is called before the first frame update
    void Start()
    {
        ttr = GetComponent<RectTransform>();
        originPoint = ttr.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        deltaMovement += new Vector2(Input.GetAxis("Mouse X") + Input.GetAxis("Gamepad RSX"), Input.GetAxis("Mouse Y") + Input.GetAxis("Gamepad RSY")) * 0.25f;
        deltaMovement = Util.ClampVec(deltaMovement, -1, 1);

        deltaMovement -= deltaMovement * Time.deltaTime*4;

        dampDeltaMovement += (deltaMovement - dampDeltaMovement) * Time.deltaTime * 4;


        ttr.anchoredPosition = originPoint + dampDeltaMovement * swayStrength;
    }
}
