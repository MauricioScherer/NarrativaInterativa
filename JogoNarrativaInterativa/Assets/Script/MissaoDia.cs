using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoDia : MonoBehaviour
{
    public GameObject[] missionFinish;

    public void SetMissionFinish(int p_status)
    {
        missionFinish[p_status].SetActive(true);
    }
}
