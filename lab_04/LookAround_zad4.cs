using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    // ruch wokó³ osi Y bêdzie wykonywany na obiekcie gracza, wiêc
    // potrzebna nam referencja do niego (konkretnie jego komponentu Transform)
    public Transform player;

    public float sensitivity = 200f;

    void Start()
    {
        // zablokowanie kursora na œrodku ekranu, oraz ukrycie kursora
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // pobieramy wartoœci dla obu osi ruchu myszy
        float mouseXMove = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseYMove = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // wykonujemy rotacjê wokó³ osi Y
        player.Rotate(Vector3.up * mouseXMove);

        // a dla osi X obracamy kamerê dla lokalnych koordynatów
        // -mouseYMove aby unikn¹æ ofektu mouse inverse
        transform.Rotate(new Vector3(-mouseYMove, 0f, 0f), Space.Self);

        float xRot = transform.localEulerAngles.x;
        xRot = xRot > 180 ? xRot - 360 : xRot;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localEulerAngles = new Vector3(xRot, 0f, 0f);

    }
}