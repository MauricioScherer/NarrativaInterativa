using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerTV : MonoBehaviour
{
    [SerializeField]
    public VideoPlayer telaTv;
    [SerializeField]
    private AudioSource audioTv;
    [SerializeField]
    public MeshRenderer meshTV;
    [SerializeField]
    private GameObject feedbackArrow;

    public float volumeMax;
    public float volumeMin;

    private bool _stayTrigger;

    [Header("Dia 1")]
    public VideoClip[] propaganda;
    public VideoClip[] jornal;
    public VideoClip[] programacaoNormal;

    private bool tvOn;
    //dia1
    private int dia;
    private bool startDia;
    private int statusDia;
    private bool finishDia;

    private void Start()
    {
        dia = GameManager.Instance.GetDiaCurrent();
    }

    private void Update()
    {
        StartTvDia1();
        StayTrigger();
    }

    public void StartTvDia1()
    {
        if (startDia && !finishDia)
        {
            float currentFrame = telaTv.frame;

            if (statusDia == 0)
            {
                if (currentFrame >= propaganda[dia].frameCount - 15)
                {
                    print("troca para o jornal");
                    telaTv.clip = jornal[dia];
                    telaTv.Play();
                    statusDia++;
                }
            }
            else if(statusDia == 1)
            {
                if (currentFrame >= jornal[dia].frameCount - 10)
                {
                    GameManager.Instance.SetMissionResume(2);
                    telaTv.clip = programacaoNormal[dia];
                    telaTv.Play();
                    statusDia++;

                    if(dia == 1)
                    {
                        GameManager.Instance.StartDia2Parte2();
                    }
                }
            }
            else if(statusDia == 2)
            {
                if (currentFrame >= programacaoNormal[dia].frameCount - 5)
                {
                    finishDia = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stayTrigger = true;
            GameManager.Instance.ViewBtn("Aperte E para ligar ou desligar a TV.");
            //feedbackArrow.SetActive(false);
        }
    }

    private void StayTrigger()
    {
        if(_stayTrigger)
        {
            if (Input.GetKeyDown("e"))
            {
                if (!tvOn)
                {
                    GameManager.Instance.ViewBtn("");
                    //feedbackArrow.SetActive(false);
                    audioTv.volume = volumeMax;
                    tvOn = true;

                    if (dia == 0)
                    {
                        if (!startDia)
                        {
                            telaTv.clip = propaganda[dia];
                            telaTv.frame = 700;
                            telaTv.Play();
                            startDia = true;
                        }
                    }
                    else if(dia == 1)
                    {
                        if (!startDia)
                        {
                            telaTv.clip = propaganda[dia];
                            telaTv.frame = 900;
                            telaTv.Play();
                            startDia = true;
                        }
                    }
                    else if (dia == 2)
                    {
                        if (!startDia)
                        {
                            telaTv.clip = propaganda[dia];
                            telaTv.frame = 600;
                            telaTv.Play();
                            startDia = true;
                        }
                    }
                }
                else
                {
                    DesligarTv();
                }
                meshTV.enabled = tvOn;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stayTrigger = false;
            GameManager.Instance.ViewBtn("");
            //feedbackArrow.SetActive(true);
        }
    }

    public void DesligarTv()
    {
        if(tvOn)
        {
            GameManager.Instance.ViewBtn("");
            //feedbackArrow.SetActive(true);
            audioTv.volume = volumeMin;
            tvOn = false;
            meshTV.enabled = tvOn;
        }
    }

    public void ResetDia()
    {
        startDia = false;
        finishDia = false;
        statusDia = 0;
        dia = GameManager.Instance.GetDiaCurrent();
    }

    public void SetVolumeBinoculo()
    {
        if(tvOn)
        {
            if (audioTv.volume == volumeMax)
                audioTv.volume = 0.1f;
            else
                audioTv.volume = volumeMax;
        }
        
    }
}
