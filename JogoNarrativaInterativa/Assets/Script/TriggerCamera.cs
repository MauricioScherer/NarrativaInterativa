using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    private GameObject mainCamera;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private int positionMove;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            mainCamera.transform.position = target.position;
            mainCamera.transform.rotation = target.rotation;

            other.GetComponent<Player>().direction = positionMove;
        }
    }
}
