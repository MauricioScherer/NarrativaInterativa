using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDia : MonoBehaviour
{
    public void StartNewDia()
    {
        if(GameManager.Instance.GetDiaCurrent() == 0)
        {
            GameManager.Instance.StartDia2();
        }
        else if(GameManager.Instance.GetDiaCurrent() == 1)
        {
            GameManager.Instance.StartDia3();
        }
    }

    public void ResetInBlack()
    {
        if (GameManager.Instance.GetDiaCurrent() == 1)
        {
            GameManager.Instance.FinishDia3();
        }
    }
}
