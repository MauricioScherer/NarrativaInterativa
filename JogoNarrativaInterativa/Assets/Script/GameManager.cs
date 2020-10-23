using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int dia = 0;

    [SerializeField]
    private AudioSource alarm;
    [SerializeField]
    private GameObject btnAcordar;
    [SerializeField]
    private Light lampadaQuarto;
    [SerializeField]
    private Text feedText;

    [Header("Player")]
    public Player player;
    [Header("TriggetCama")]
    public TriggerCama triggerCama;

    [Header("Missoes")]
    public GameObject[] missoes;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //iniciar o ciclo para o dia 1
        Invoke("StartDia1", 10.0f);
    }

    void Update()
    {
        switch(dia)
        { 
            case 0:
                if(Input.GetKeyDown("e") && triggerCama.GetEstaDormindo())
                {
                    alarm.Stop();
                    btnAcordar.SetActive(false);
                    triggerCama.Acordar();
                    player.SetCanMoving();
                    player.SetViewPlayer(true);
                    lampadaQuarto.enabled = true;
                }
                break;
            default:
                break;
        }
    }

    private void StartDia1()
    {
        alarm.Play();
        btnAcordar.SetActive(true);
        missoes[dia].SetActive(true);

    }

    public void ViewBtn(string p_text)
    {
        feedText.text = p_text;

        if(p_text != "")        
            btnAcordar.SetActive(true);
        else
            btnAcordar.SetActive(false);

    }
}
