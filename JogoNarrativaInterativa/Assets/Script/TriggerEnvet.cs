using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnvet : MonoBehaviour
{
    private bool _stayTrigger;
    private GameObject player;

    [SerializeField]
    private GameObject playerPC;
    [SerializeField]
    private GameObject feedbackArrow;

    private void Update()
    {
        StayTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _stayTrigger = true;
            player = other.gameObject;
            GameManager.Instance.ViewBtn("Aperte E para trabalhar no notebook.");
            feedbackArrow.SetActive(false);
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
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(false);
                    player.GetComponent<Player>().SetCanMoving();
                    player.GetComponent<Player>().SetViewPlayer(false);
                    playerPC.SetActive(true);
                }
                else
                {
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(true);
                    player.GetComponent<Player>().SetCanMoving();
                    player.GetComponent<Player>().SetViewPlayer(true);
                    playerPC.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stayTrigger = false;
            GameManager.Instance.ViewBtn("");
            feedbackArrow.SetActive(true);
        }
    }
}
