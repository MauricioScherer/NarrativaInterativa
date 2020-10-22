using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnvet : MonoBehaviour
{
    [SerializeField]
    private GameObject feedbackTarget;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            feedbackTarget.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            feedbackTarget.SetActive(false);
        }
    }
}
