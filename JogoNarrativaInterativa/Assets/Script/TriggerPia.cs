using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPia : MonoBehaviour
{
    //[SerializeField]
    //private GameObject feedbackTarget;
    [SerializeField]
    private GameObject feedbackArrow;
    [SerializeField]
    private GameObject progresso;
    [SerializeField]
    private Image progressBar;

    private GameObject player;
    private bool progressoFinish;

    private void Update()
    {
        if(!progressoFinish && progresso.activeSelf)
        {
            progressBar.fillAmount -= 0.01f;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                progressBar.fillAmount += 0.1f;
            }

            if(progressBar.fillAmount >= 0.95f)
            {
                FinishingProgress();
            }
        }
    }

    private void FinishingProgress()
    {
        progressoFinish = true;
        progresso.SetActive(false);
        //feedbackTarget.SetActive(false);
        GameManager.Instance.ViewBtn("");
        feedbackArrow.SetActive(true);
        player.GetComponent<Player>().SetCanMoving();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!progressoFinish)
            {
                //feedbackTarget.SetActive(true);
                GameManager.Instance.ViewBtn("Aperte E para escovar os dentes.");
                feedbackArrow.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
            if(!progressoFinish)
            {
                if (other.GetComponent<Player>().GetCanMoving())
                {
                    progresso.SetActive(true);
                    //feedbackTarget.SetActive(false);
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(false);
                    player = other.gameObject;
                    other.GetComponent<Player>().SetCanMoving();
                }
                else
                {
                    progressBar.fillAmount = 0.0f;
                    progresso.SetActive(false);
                    //feedbackTarget.SetActive(false);
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(true);
                    player = null;
                    other.GetComponent<Player>().SetCanMoving();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!progressoFinish)
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(true);
            }
        }
    }

    public void SetProgressFinish(bool p_status)
    {
        progressoFinish = p_status;
    }
}
