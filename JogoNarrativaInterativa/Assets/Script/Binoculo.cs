using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binoculo : MonoBehaviour
{
    public float xSensitivity = 100.0f;
    public float ySensitivity = 100.0f;

    public float yMinLimit = -45.0f;
    public float yMaxLimit = 45.0f;

    public float xMinLimit = -360.0f;
    public float xMaxLimit = 360.0f;

    float yRot = 0.0f;
    float xRot = 0.0f;

    // Update is called once per frame
    void Update()
    {
        xRot += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yMinLimit, yMaxLimit);
        xRot = Mathf.Clamp(xRot, xMinLimit, xMaxLimit);
        Camera.main.transform.localEulerAngles = new Vector3(-yRot, xRot, 0);
    }
}
