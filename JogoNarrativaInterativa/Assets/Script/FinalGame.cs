using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGame : MonoBehaviour
{
    public FinalRain finalRain;
    public GameObject[] assassino;
    public AudioSource audioFinal;
    public AudioSource musicaFinal;

    public Transform cameraMain;
    public Transform posCamSala;
    public GameObject btnReturn;

    public void Assassino1()
    {
        assassino[0].SetActive(true);
    }
    public void Assassino2()
    {
        assassino[0].SetActive(false);
        assassino[1].SetActive(true);
    }
    public void Assassino3()
    {
        assassino[1].SetActive(false);
        assassino[2].SetActive(true);
    }

    public void Thunder()
    {
        finalRain.StartThunder();
    }

    public void Assassinato()
    {
        audioFinal.Play();
    }

    public void MusicaFinal()
    {
        musicaFinal.Play();
    }

    public void CutSceneFinal()
    {
        assassino[2].SetActive(false);
        assassino[3].SetActive(true);

        cameraMain.transform.position = posCamSala.position;
        cameraMain.transform.rotation = posCamSala.rotation;
    }

    public void btnFinal()
    {
        btnReturn.SetActive(true);
    }
}
