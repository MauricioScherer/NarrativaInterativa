using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binoculo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float mouseInput = Input.GetAxis("Mouse X");
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);
    }
}
