using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnvet : MonoBehaviour
{
    [SerializeField]
    private GameObject feedbackTarget;
    [SerializeField]
    private GameObject playerPC;
    [SerializeField]
    private GameObject feedbackArrow;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            feedbackTarget.SetActive(true);
            feedbackArrow.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown("e"))
        {
            if(other.GetComponent<Player>().GetCanMoving())
            {
                feedbackTarget.SetActive(false);
                feedbackArrow.SetActive(false);
                other.GetComponent<Player>().SetCanMoving();
                playerPC.SetActive(true);
            }
            else
            {
                feedbackTarget.SetActive(false);
                feedbackArrow.SetActive(true);
                other.GetComponent<Player>().SetCanMoving();
                playerPC.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            feedbackTarget.SetActive(false);
            feedbackArrow.SetActive(true);
        }
    }
}
