using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRain : MonoBehaviour
{
    public GameObject thunder;

    public void StartThunder()
    {
        thunder.SetActive(true);
        Invoke("ResetThunder", 15.0f);
    }

    public void ResetThunder()
    {
        thunder.SetActive(false);
    }

}
