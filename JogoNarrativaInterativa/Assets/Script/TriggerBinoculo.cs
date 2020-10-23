using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBinoculo : MonoBehaviour
{
    //[SerializeField]
    //private GameObject feedbackTarget;
    [SerializeField]
    private GameObject feedbackArrow;

    [SerializeField]
    private GameObject cameraBinoculo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //feedbackTarget.SetActive(true);
            GameManager.Instance.ViewBtn("Aperte E para usar o binóculo.");
            feedbackArrow.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
            if (other.GetComponent<Player>().GetCanMoving())
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(false);
                other.GetComponent<Player>().SetCanMoving();
                cameraBinoculo.SetActive(true);
            }
            else
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(true);
                other.GetComponent<Player>().SetCanMoving();
                cameraBinoculo.SetActive(false);
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
