using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPOrta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.Instance.GetStatusDia() == 2)
                GameManager.Instance.ViewBtn("Aperte E para ir trabalhar.");
            else
                GameManager.Instance.ViewBtn("Você precisa fazer as outras tarefas antes de ir trabalhar.");
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
