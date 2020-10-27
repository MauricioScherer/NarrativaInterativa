using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int dia = 0;
    private int statusDia;
    private bool startDia;

    [SerializeField]
    private AudioSource alarm;
    [SerializeField]
    private GameObject btnAcordar;
    [SerializeField]
    private Light lampadaQuarto;
    [SerializeField]
    private Text feedText;
    [SerializeField]
    private GameObject lenteBinoculo;
    [SerializeField]
    private GameObject finalDia;

    [Header("Player")]
    public Player player;
    [Header("TriggerCama")]
    public TriggerCama triggerCama;
    [Header("TriggerCama")]
    public TriggerCafe triggerCafe;
    [Header("TriggerBanheiro")]
    public TriggerPia triggerBanheiro;
    [Header("TriggerTv")]
    public TriggerTV triggerTv;

    [Header("Missoes")]
    public GameObject[] missoes;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //iniciar o ciclo para o dia 1
        Invoke("StartDia1", 5.0f);
    }

    void Update()
    {
        switch(dia)
        { 
            case 0:
                if(Input.GetKeyDown("e") && triggerCama.GetEstaDormindo() && startDia)
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
        startDia = true;
    }
    public void StartDia1parte2()
    {
        triggerCama.SetPodeDormir(true);
        triggerCafe.SetProgressFinish(false);
        triggerBanheiro.SetProgressFinish(false);
        missoes[0].GetComponent<MissaoDia>().MissionDia1Parte2();
    }
    public void FinishDia1()
    {
        if(startDia)
        {
            triggerTv.DesligarTv();
            lampadaQuarto.enabled = false;
            startDia = false;
            missoes[0].SetActive(false);
            finalDia.SetActive(true);
        }
    }

    public void ViewBtn(string p_text)
    {
        feedText.text = p_text;

        if(p_text != "")        
            btnAcordar.SetActive(true);
        else
            btnAcordar.SetActive(false);
    }

    public void SetMissionResume(int p_status)
    {
        missoes[dia].GetComponent<MissaoDia>().SetMissionFinish(p_status);
        statusDia++;
    }

    public int GetStatusDia()
    {
        return statusDia;
    }

    public void ViewLenteBinoculo(bool p_status)
    {
        lenteBinoculo.SetActive(p_status);
    }

    public void DesligarTvDireto()
    {
        triggerTv.DesligarTv();
    }
}
