using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookViewcamera : MonoBehaviour
{
    private Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
            transform.LookAt(mainCamera);
    }
}
