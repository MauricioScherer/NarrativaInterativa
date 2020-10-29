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

    public void FinalKiller()
    {
        plane.material = planeBlack;
        sangue.SetActive(true);
    }

    public void AudioAssassinato()
    {
        audioAssassino.Play();
    }
}
