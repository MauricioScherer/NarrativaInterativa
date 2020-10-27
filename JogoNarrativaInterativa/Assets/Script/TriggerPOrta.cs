using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPOrta : MonoBehaviour
{
    private bool _stayTrigger;
    private bool _work;
    private GameObject player;

    [SerializeField]
    private GameObject feedWork;

    private void Update()
    {
        Staytrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_work)
        {
            player = other.gameObject;
            _stayTrigger = true;
            if (GameManager.Instance.GetStatusDia() == 3)
                GameManager.Instance.ViewBtn("Aperte E para ir trabalhar.");
            else
                GameManager.Instance.ViewBtn("Você precisa fazer as outras tarefas antes de ir trabalhar.");
        }
    }

    private void Staytrigger()
    {
        if(_stayTrigger)
        {
            if(Input.GetKeyDown("e") && !_work && GameManager.Instance.GetStatusDia() == 3)
            {
                GameManager.Instance.DesligarTvDireto();
                feedWork.SetActive(true);
                player.GetComponent<Player>().SetCanMoving();
                GameManager.Instance.ViewBtn("");
                _work = true;
                Invoke("ReturnHome", 6.0f);
            }
        }
    }

    private void ReturnHome()
    {
        feedWork.SetActive(false);
        player.GetComponent<Player>().SetCanMoving();
        GameManager.Instance.StartDia1parte2();
        Destroy(gameObject, 1.0f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_work)
        {
            _stayTrigger = false;
            GameManager.Instance.ViewBtn("");
        }
    }
}
