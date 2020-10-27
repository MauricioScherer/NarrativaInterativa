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

    private bool _stayTrigger;

    [Header("Dia 1")]
    public VideoClip propaganda;
    public VideoClip jornal;
    public VideoClip programacaoNormal;

    private bool tvOn;
    //dia1
    private int dia = 1;
    private bool startDia1;
    private int statusDia1;
    private bool finishDia1;

    private void Update()
    {
        StartTvDia1();
        StayTrigger();
    }

    public void StartTvDia1()
    {
        if (startDia1 && !finishDia1)
        {
            float currentFrame = telaTv.frame;

            if (statusDia1 == 0)
            {
                if (currentFrame >= propaganda.frameCount - 5)
                {
                    telaTv.clip = jornal;
                    telaTv.Play();
                    statusDia1++;
                }
            }
            else if(statusDia1 == 1)
            {
                if (currentFrame >= jornal.frameCount - 5)
                {
                    GameManager.Instance.SetMissionResume(2);
                    telaTv.clip = programacaoNormal;
                    telaTv.Play();
                    statusDia1++;
                }
            }
            else if(statusDia1 == 2)
            {
                if (currentFrame >= programacaoNormal.frameCount - 5)
                {
                    finishDia1 = true;
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
            feedbackArrow.SetActive(false);
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
                    //feedbackTarget.SetActive(false);
                    GameManager.Instance.ViewBtn("");
                    feedbackArrow.SetActive(false);
                    audioTv.volume = 0.3f;
                    tvOn = true;

                    if (dia == 1)
                    {
                        if (!startDia1)
                        {
                            telaTv.clip = propaganda;
                            telaTv.frame = 700;
                            telaTv.Play();
                            startDia1 = true;
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
            feedbackArrow.SetActive(true);
        }
    }

    public void DesligarTv()
    {
        if(tvOn)
        {
            GameManager.Instance.ViewBtn("");
            feedbackArrow.SetActive(true);
            audioTv.volume = 0.0f;
            tvOn = false;
            meshTV.enabled = tvOn;
        }
    }
}
