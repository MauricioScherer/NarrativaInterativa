using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnvet : MonoBehaviour
{
    //[SerializeField]
    //private GameObject feedbackTarget;
    [SerializeField]
    private GameObject playerPC;
    [SerializeField]
    private GameObject feedbackArrow;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //feedbackTarget.SetActive(true);
            GameManager.Instance.ViewBtn("Aperte E para trabalhar no notebook.");
            feedbackArrow.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown("e"))
        {
            if(other.GetComponent<Player>().GetCanMoving())
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(false);
                other.GetComponent<Player>().SetCanMoving();
                other.GetComponent<Player>().SetViewPlayer(false);
                playerPC.SetActive(true);
            }
            else
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(true);
                other.GetComponent<Player>().SetCanMoving();
                other.GetComponent<Player>().SetViewPlayer(true);
                playerPC.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //feedbackTarget.SetActive(false);
            GameManager.Instance.ViewBtn("");
            feedbackArrow.SetActive(true);
        }
    }
}
