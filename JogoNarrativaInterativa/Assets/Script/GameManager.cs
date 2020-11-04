using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int dia = 0;
    private int statusDia;
    private bool startDia;

    private bool quarentena;
    private bool podeLevantarSofa;

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
    private GameObject luzBinoculo;
    [SerializeField]
    private GameObject finalDia;
    [SerializeField]
    private GameObject sonoSofa;

    [Header("Player")]
    public Player player;
    [Header("TriggerCama")]
    public TriggerCama triggerCama;
    [Header("TriggerCafe")]
    public TriggerCafe triggerCafe;
    [Header("TriggerBanheiro")]
    public TriggerPia triggerBanheiro;
    [Header("TriggerTv")]
    public TriggerTV triggerTv;
    [Header("TriggerSofa")]
    public TriggerSofa triggerSofa;

    [Header("Missoes")]
    public GameObject[] missoes;

    [Header("LuzesCasa")]
    public Light[] lights;

    [Header("AssassinoDia1")]
    public GameObject assassinoDia1;
    [Header("AssassinoDia3")]
    public GameObject assassinoDia3;

    [Header("Assassinato")]
    public GameObject assassino;
    public GameObject planeJanela;
    public GameObject vitima;
    public BoxCollider triggerJanela;

    [Header("Vizinhança")]
    public GameObject[] moradoresDia;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //iniciar o ciclo para o dia 1
        Invoke("StartDia1", 5.0f);
        moradoresDia[dia].SetActive(true);
    }

    void Update()
    {
        switch(dia)
        { 
            case 0:
                if(Input.GetKeyDown("e") && triggerCama.GetEstaDormindo() && startDia)
                {
                    StandardStartDia();
                }
                break;
            case 1:
                if (Input.GetKeyDown("e") && triggerCama.GetEstaDormindo() && startDia)
                {
                    StandardStartDia();
                }
                break;
            case 2:
                if (Input.GetKeyDown("e") && triggerCama.GetEstaDormindo() && startDia)
                {
                    StandardStartDia();
                }
                break;
            default:
                break;
        }
    }

    private void StandardStartDia()
    {
        alarm.Stop();
        btnAcordar.SetActive(false);
        triggerCama.Acordar();
        player.SetCanMoving();
        player.SetViewPlayer(true);
        lampadaQuarto.enabled = true;
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
    public void FinishDia()
    {
        if(startDia)
        {
            triggerTv.DesligarTv();
            lampadaQuarto.enabled = false;
            startDia = false;
            missoes[dia].SetActive(false);
            finalDia.SetActive(true);
        }
    }

    public void StartDia2()
    {
        alarm.Play();
        finalDia.SetActive(false);
        dia = 1;
        btnAcordar.SetActive(true);
        missoes[dia].SetActive(true);
        startDia = true;
        triggerCama.SetPodeDormir(false);
        triggerCafe.SetProgressFinish(false);
        triggerBanheiro.SetProgressFinish(false);
        triggerTv.ResetDia();
        ViewBtn("Aperte 'E' para acordar.");
        statusDia = 0;

        //troca os como o assassino é visto
        assassinoDia1.SetActive(false);
        assassino.SetActive(true);
        vitima.SetActive(true);

        //toca vizinhança
        moradoresDia[0].SetActive(false);
        moradoresDia[1].SetActive(true);
    }
    public void StartDia2Parte2()
    {
        missoes[dia].GetComponent<MissaoDia>().MissionDia2Parte2();
        quarentena = true;

        if (triggerSofa.GetStaySofa())
        {
            StartSonoSofa();
        }
    }
    public void StartDia2Parte3()
    {
        triggerCama.SetPodeDormir(true);
        missoes[dia].GetComponent<MissaoDia>().MissionDia2Parte3();
    }

    public void FinishDia3()
    {
        SetStatusLights(true);
        lights[0].enabled = false;
        sonoSofa.GetComponent<DormirSofa>().StopRain();
        sonoSofa.SetActive(false);
        triggerTv.GetComponent<BoxCollider>().enabled = true;
    }

    public void StartDia3()
    {
        alarm.Play();
        finalDia.SetActive(false);
        dia = 2;
        btnAcordar.SetActive(true);
        missoes[dia].SetActive(true);
        startDia = true;
        triggerCama.SetPodeDormir(false);
        triggerCafe.SetProgressFinish(false);
        triggerBanheiro.SetProgressFinish(false);
        
        triggerTv.ResetDia();
        ViewBtn("Aperte 'E' para acordar.");
        statusDia = 0;

        //troca os como o assassino é visto
        assassino.GetComponent<Assassino>().ResetDia3();
        assassino.SetActive(false);
        assassinoDia3.SetActive(true);
        planeJanela.SetActive(false);


        //toca vizinhança
        moradoresDia[1].SetActive(false);
        moradoresDia[2].SetActive(true);
    }

    public void StartSonoSofa()
    {
        triggerSofa.SetPodeLevantar(false);
        sonoSofa.SetActive(true);
        triggerJanela.enabled = true;
    }

    public void StartAssassinato()
    {
        assassino.GetComponent<Animator>().SetTrigger("Killer");
        vitima.SetActive(false);
        planeJanela.SetActive(true);
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

    public int GetDiaCurrent()
    {
        return dia;
    }

    public bool GetQuarentena()
    {
        return quarentena;
    }

    public void ViewLenteBinoculo(bool p_status)
    {
        lenteBinoculo.SetActive(p_status);
        luzBinoculo.SetActive(p_status);
        triggerTv.SetVolumeBinoculo();
    }

    public void DesligarTvDireto()
    {
        triggerTv.DesligarTv();
    }

    public void SetStatusLights(bool p_status)
    {
        foreach (Light obj in lights)
            obj.enabled = p_status;

        if(!p_status)
        {
            triggerTv.DesligarTv();
            triggerSofa.SetPodeLevantar(true);
            triggerTv.GetComponent<BoxCollider>().enabled = false;
            missoes[dia].GetComponent<MissaoDia>().SetMissionFinish(0);
        }
    }
}
