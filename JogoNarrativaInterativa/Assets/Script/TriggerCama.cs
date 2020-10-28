using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCama : MonoBehaviour
{
    private bool _stayTrigger;
    private GameObject player;

    [SerializeField]
    private GameObject playerCama;
    [SerializeField]
    private GameObject feedbackArrow;
    [SerializeField]
    private AudioSource somAcordar;

    private bool podeDormir = false;
    private bool podeAcordar;
    private bool estaDormindo = true;

    private void Update()
    {
        StayTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            if (podeDormir)
            {
                _stayTrigger = true;
                GameManager.Instance.ViewBtn("Aperte E para dormir.");
                //feedbackArrow.SetActive(false);
            }
        }
    }

    private void StayTrigger()
    {
        if(_stayTrigger)
        {
            if (Input.GetKeyDown("e"))
            {
                if (player.GetComponent<Player>().GetCanMoving())
                {
                    if (podeDormir)
                    {
                        GameManager.Instance.ViewBtn("");
                        GameManager.Instance.FinishDia1();
                        //feedbackArrow.SetActive(false);
                        player.GetComponent<Player>().SetCanMoving();
                        player.GetComponent<Player>().SetViewPlayer(false);
                        playerCama.SetActive(true);
                        estaDormindo = true;
                    }
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
                GameManager.Instance.ViewBtn("");
                //feedbackArrow.SetActive(true);
            }
            _stayTrigger = false;
        }
    }

    public void Acordar()
    {
        playerCama.SetActive(false);
        estaDormindo = false;
        somAcordar.Play();
    }

    public void SetPodeDormir(bool p_status)
    {
        podeDormir = p_status;
    }

    public bool GetEstaDormindo()
    {
        return estaDormindo;
    }
}
