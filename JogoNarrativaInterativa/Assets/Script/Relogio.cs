using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Relogio : MonoBehaviour
{
    private int hour;
    private int seconds;
    private int minuto;

    [SerializeField]
    private TextMeshPro textHour;

    void FixedUpdate()
    {
        if(seconds >= 3600)
        {
            if (minuto < 60)
            {
                minuto++;
            }
            else
            {
                minuto = 0;
            }
            seconds = 0;
        }

        seconds++;
    }

    public void SetNewHour(int p_hora)
    {
        hour = p_hora;
        SetHour();
    }

    public void MoreHour()
    {
        hour++;
        SetHour();
    }

    private void SetHour()
    {
        string _hour = hour < 10 ? "0" + hour.ToString() : hour.ToString();
        string _minutos = minuto < 10 ? "0" + minuto.ToString() : minuto.ToString();
        textHour.text = _hour + ":" + _minutos;
    }
}
