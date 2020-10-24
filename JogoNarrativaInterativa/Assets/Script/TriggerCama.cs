using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCama : MonoBehaviour
{
    //[SerializeField]
    //private GameObject feedbackTarget;
    [SerializeField]
    private GameObject playerCama;
    [SerializeField]
    private GameObject feedbackArrow;
    [SerializeField]
    private AudioSource somAcordar;

    private bool podeDormir = false;
    private bool podeAcordar;
    private bool estaDormindo = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(podeDormir)
            {
                //feedbackTarget.SetActive(true);
                GameManager.Instance.ViewBtn("Aperte E para dormir.");
                feedbackArrow.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
            if (other.GetComponent<Player>().GetCanMoving())
            {
                if(podeDormir)
                {
                    //feedbackTarget.SetActive(false);
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(false);
                    other.GetComponent<Player>().SetCanMoving();
                    other.GetComponent<Player>().SetViewPlayer(false);
                    playerCama.SetActive(true);
                    estaDormindo = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(podeDormir)
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(true);
            }
        }
    }

    public void Acordar()
    {
        playerCama.SetActive(false);
        estaDormindo = false;
        somAcordar.Play();
    }

    public bool GetEstaDormindo()
    {
        return estaDormindo;
    }
}
