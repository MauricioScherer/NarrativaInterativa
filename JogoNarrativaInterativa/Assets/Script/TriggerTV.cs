using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerTV : MonoBehaviour
{
    [SerializeField]
    public VideoPlayer telaTv;
    [SerializeField]
    public MeshRenderer meshTV;
    [SerializeField]
    private GameObject feedbackTarget;
    [SerializeField]
    private GameObject feedbackArrow;

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
            feedbackTarget.SetActive(true);
            feedbackArrow.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
            if (!tvOn)
            {
                feedbackTarget.SetActive(false);
                feedbackArrow.SetActive(false);
                tvOn = true;

                if(dia == 1)
                {
                    if(!startDia1)
                    {
                        telaTv.clip = propaganda;
                        telaTv.Play();
                        startDia1 = true;
                    }
                }
            }
            else
            {
                feedbackTarget.SetActive(false);
                feedbackArrow.SetActive(true);
                tvOn = false;
            }
            meshTV.enabled = tvOn;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            feedbackTarget.SetActive(false);
            feedbackArrow.SetActive(true);
        }
    }
}
