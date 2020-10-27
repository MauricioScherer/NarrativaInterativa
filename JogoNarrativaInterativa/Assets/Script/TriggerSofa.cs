using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSofa : MonoBehaviour
{
    private bool _staytrigger;
    private GameObject player;

    [SerializeField]
    private GameObject playerSofa;
    [SerializeField]
    private GameObject feedbackArrow;

    private void Update()
    {
        StayTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _staytrigger = true;
            player = other.gameObject;
            GameManager.Instance.ViewBtn("Aperte E para sentar no sofá.");
            feedbackArrow.SetActive(false);
        }
    }

    private void StayTrigger()
    {
        if(_staytrigger)
        {
            if (Input.GetKeyDown("e"))
            {
                if (player.GetComponent<Player>().GetCanMoving())
                {
                    //feedbackTarget.SetActive(false);
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(false);
                    player.GetComponent<Player>().SetCanMoving();
                    player.GetComponent<Player>().SetViewPlayer(false);
                    playerSofa.SetActive(true);
                }
                else
                {
                    //feedbackTarget.SetActive(false);
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(true);
                    player.GetComponent<Player>().SetCanMoving();
                    player.GetComponent<Player>().SetViewPlayer(true);
                    playerSofa.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _staytrigger = false;

            GameManager.Instance.ViewBtn("");
            feedbackArrow.SetActive(true);
        }
    }
}
