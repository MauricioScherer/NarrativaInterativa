using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCafe : MonoBehaviour
{
    private bool _stayTrigger;
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
        if (!progressoFinish && progresso.activeSelf)
        {
            progressBar.fillAmount -= 0.01f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                progressBar.fillAmount += 0.1f;
            }

            if (progressBar.fillAmount >= 0.95f)
            {
                FinishingProgress();
            }
        }

        StayTrigger();
    }

    private void FinishingProgress()
    {
        progressoFinish = true;
        progresso.SetActive(false);
        GameManager.Instance.ViewBtn("");
        feedbackArrow.SetActive(true);
        player.GetComponent<Player>().SetCanMoving();
        GameManager.Instance.SetMissionResume(0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            if (!progressoFinish)
            {
                _stayTrigger = true;
                GameManager.Instance.ViewBtn("Aperte E para tomar café");
                feedbackArrow.SetActive(false);
            }
        }
    }

    private void StayTrigger()
    {
        if(_stayTrigger)
        {
            if (Input.GetKeyDown("e"))
            {
                if (!progressoFinish)
                {
                    if (player.GetComponent<Player>().GetCanMoving())
                    {
                        progresso.SetActive(true);
                        GameManager.Instance.ViewBtn("");
                        feedbackArrow.SetActive(false);
                        player.GetComponent<Player>().SetCanMoving();
                    }
                    else
                    {
                        progressBar.fillAmount = 0.0f;
                        progresso.SetActive(false);
                        GameManager.Instance.ViewBtn("");
                        feedbackArrow.SetActive(true);
                        player.GetComponent<Player>().SetCanMoving();
                    }
                }
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!progressoFinish)
            {
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(true);
            }
            _stayTrigger = false;
        }
    }

    public void SetProgressFinish(bool p_status)
    {
        progressoFinish = p_status;
    }
}
