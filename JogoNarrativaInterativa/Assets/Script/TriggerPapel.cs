using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPapel : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject bilhete;

    private void Update()
    {
        StayTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            GameManager.Instance.ViewBtn("Aperte 'E' para pegar o bilhete.");
        }
    }

    private void StayTrigger()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!bilhete.activeSelf)
            {
                GameManager.Instance.ViewBtn("");
                player.GetComponent<Player>().SetCanMoving();
                bilhete.SetActive(true);
            }
            else
            {
                GameManager.Instance.ViewPapel1(false);
                player.GetComponent<Player>().SetCanMoving();
                bilhete.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ViewBtn("");
        }
    }


}
