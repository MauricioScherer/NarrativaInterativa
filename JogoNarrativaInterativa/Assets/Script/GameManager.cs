using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int dia = 0;
    private int statusDia;
    private bool startDia;

    private bool quarentena;
    private bool podeLevantarSofa;

    private bool _estaBisbilhotando;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Relogio relogio;
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
    [SerializeField]
    public GameObject papel1;
    [SerializeField]
    public GameObject papel2;
    [SerializeField]
    public GameObject papel3;
    [SerializeField]
    public GameObject papel4;

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
    [Header("TriggerPC")]
    public TrriggerHomeoffice triggerPC;


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

    [Header("Final")]
    public GameObject finalRain;
    public GameObject rainFinalGame;

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
        if(Input.GetKeyDown(KeyCode.Escape) && !rainFinalGame.activeSelf && startDia)
        {
            if(!pauseMenu.activeSelf)
                PauseMenu(true);
            else
                PauseMenu(false);
        }

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
            case 3:
                if (Input.GetKeyDown("e") && triggerCama.GetEstaDormindo() && startDia)
                {
                    StandardStartDia();
                }
                break;
            case 4:
                if (Input.GetKeyDown("e") && triggerCama.GetEstaDormindo() && startDia)
                {
                    if(!rainFinalGame.activeSelf)
                    {
                        btnAcordar.SetActive(false);
                        triggerCama.Acordar();
                        player.SetCanMoving();
                        player.SetViewPlayer(true);
                    }
                }
                break;
            default:
                break;
        }
    }

    public void PauseMenu(bool p_status)
    {
        if (p_status)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
        }

        Cursor.visible = p_status;
        pauseMenu.SetActive(p_status);
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
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
        triggerCafe.ViewFeedCafe(false);
        triggerCama.SetPodeDormir(true);
        triggerCafe.SetProgressFinish(false);
        triggerBanheiro.SetProgressFinish(false);
        missoes[0].GetComponent<MissaoDia>().MissionDia1Parte2();
        SetRelogio(23);
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
        ResetRelogio();
        alarm.Play();
        finalDia.SetActive(false);
        dia = 1;
        btnAcordar.SetActive(true);
        missoes[dia].SetActive(true);
        startDia = true;
        triggerCama.SetPodeDormir(false);
        triggerCafe.SetProgressFinish(false);
        triggerCafe.ViewFeedCafe(true);
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
        triggerPC.SetPodeTrabalhar(false);
        ResetRelogio();
        alarm.Play();
        finalDia.SetActive(false);
        dia = 2;
        btnAcordar.SetActive(true);
        missoes[dia].SetActive(true);
        startDia = true;
        triggerCama.SetPodeDormir(false);
        triggerCafe.SetProgressFinish(false);
        triggerCafe.ViewFeedCafe(true);
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

        triggerPC.gameObject.SetActive(true);
    }

    public void FinishjobDia3()
    {
        missoes[dia].GetComponent<MissaoDia>().MissionDia1Parte2();
        triggerCama.SetPodeDormir(true);
        triggerCafe.SetProgressFinish(false);
        triggerCafe.ViewFeedCafe(false);
        triggerBanheiro.SetProgressFinish(false);
    }

    public void StartDia4()
    {
        triggerPC.SetPodeTrabalhar(false);
        ResetRelogio();
        alarm.Play();
        finalDia.SetActive(false);
        dia = 3;
        btnAcordar.SetActive(true);
        missoes[dia].SetActive(true);
        startDia = true;
        triggerCama.SetPodeDormir(false);
        triggerCafe.SetProgressFinish(false);
        triggerCafe.ViewFeedCafe(true);
        triggerBanheiro.SetProgressFinish(false);

        triggerTv.ResetDia();
        ViewBtn("Aperte 'E' para acordar.");
        statusDia = 0;

        triggerPC.ResetJob();

        moradoresDia[3].SetActive(false);
        moradoresDia[4].SetActive(true);
    }

    public void StartFinalGame()
    {
        dia = 4;
        startDia = true;

        print("acorda durante a noite");
        triggerCama.SetPodeDormir(false);
        triggerCafe.SetProgressFinish(true);
        triggerBanheiro.SetProgressFinish(true);
        moradoresDia[4].SetActive(false);
        SetRelogio(3);
        triggerPC.gameObject.SetActive(false);
        ViewPapel4(true);

        foreach (Light obj in lights)
            obj.enabled = false;

        finalRain.SetActive(true);
        finalRain.GetComponent<FinalRain>().StartThunder();
    }

    public void Final()
    {
        rainFinalGame.SetActive(true);
    }

    public void StartSonoSofa()
    {
        if(!triggerJanela.enabled)
        {
            print("2");
            triggerSofa.SetPodeLevantar(false);
            sonoSofa.SetActive(true);
            triggerJanela.enabled = true;
            SetRelogio(19);
        }
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

        if(dia == 2 && !_estaBisbilhotando && relogio.GetHour() >= 10)
        {
            _estaBisbilhotando = true;
        }
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

    public void SetRelogio(int p_hora)
    {
        relogio.SetNewHour(p_hora);
    }

    public void ResetRelogio()
    {
        relogio.ResetHour();
    }

    public void MoreHour()
    {
        relogio.MoreHour();

        if(dia == 2)
        {
            if(relogio.GetHour() == 10)
            {
                ViewPapel1(true);
            }
            
            if(relogio.GetHour() == 16)
            {
                moradoresDia[2].SetActive(false);
                assassinoDia3.SetActive(false);
                moradoresDia[3].SetActive(true);
            }
        }
        else if(dia == 3)
        {
            if (relogio.GetHour() == 13)
            {
                if(_estaBisbilhotando)
                    ViewPapel2(true); // papel caso o jogador tenha bisbilhotado
                else
                    ViewPapel3(true); // papel caso o jogador não tenha bisbilhotado
            }
        }
    }

    public void ViewPapel1(bool p_status)
    {
        papel1.SetActive(p_status);
    }
    public void ViewPapel2(bool p_status)
    {
        papel2.SetActive(p_status);
    }
    public void ViewPapel3(bool p_status)
    {
        papel3.SetActive(p_status);
    }
    public void ViewPapel4(bool p_status)
    {
        papel4.SetActive(p_status);

        if(dia == 4 && !p_status)
        {
            triggerCama.SetPodeDormir(true);
        }
    }
}
