﻿using System.Collections;
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
    [SerializeField]
    private AudioSource inputSound;
    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    private GameObject feedCafe;
    [SerializeField]
    private GameObject feedJanta;

    private GameObject player;
    private bool progressoFinish;

    private void Update()
    {
        if (!progressoFinish && progresso.activeSelf)
        {
            progressBar.fillAmount -= 0.01f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputSound.Play();
                progressBar.fillAmount += 0.12f;
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
        inputSound.clip = clips[1];
        inputSound.Play();
        progressoFinish = true;
        progresso.SetActive(false);
        GameManager.Instance.ViewBtn("");
        player.GetComponent<Player>().SetCanMoving();
        GameManager.Instance.SetMissionResume(0);
        progressBar.fillAmount = 0.0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            if (!progressoFinish)
            {
                _stayTrigger = true;
                if (feedCafe.activeSelf)
                    GameManager.Instance.ViewBtn("Aperte E para tomar café");
                else
                    GameManager.Instance.ViewBtn("Aperte E para jantar");
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
                        if(feedCafe.activeSelf)
                            inputSound.clip = clips[0];
                        else
                            inputSound.clip = clips[2];
                        player.GetComponent<Player>().SetCanMoving();
                    }
                    else
                    {
                        progressBar.fillAmount = 0.0f;
                        progresso.SetActive(false);
                        GameManager.Instance.ViewBtn("");
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
                //feedbackArrow.SetActive(true);
            }
            _stayTrigger = false;
        }
    }

    public void SetProgressFinish(bool p_status)
    {
        progressoFinish = p_status;
    }

    public void ViewFeedCafe(bool p_status)
    {
        feedCafe.SetActive(p_status);
        feedJanta.SetActive(!p_status);
    }
}
