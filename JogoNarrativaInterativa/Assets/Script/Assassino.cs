using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassino : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer plane;
    [SerializeField]
    private Material planeBlack;
    [SerializeField]
    private GameObject sangue;
    [SerializeField]
    private AudioSource audioAssassino;
    [SerializeField]
    private GameObject thunderFinal;

    public void FinalKiller()
    {
        plane.material = planeBlack;
        thunderFinal.SetActive(true);
        GameManager.Instance.StartDia2Parte3();
    }

    public void AudioAssassinato()
    {
        audioAssassino.Play();
        sangue.SetActive(true);
    }
}
