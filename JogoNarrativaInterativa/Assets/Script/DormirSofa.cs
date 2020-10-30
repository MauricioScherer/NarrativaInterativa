using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormirSofa : MonoBehaviour
{
    [SerializeField]
    private AudioSource thunder;
    [SerializeField]
    private AudioSource rain;
    [SerializeField]
    private GameObject objrain;
    [SerializeField]
    private AudioSource soundTv;

    private void FixedUpdate()
    {
        if(soundTv.volume > 0)
        {
            soundTv.volume -= 0.0005f;
        }
    }

    public void StartRain()
    {
        thunder.Play();
        rain.Play();
        GameManager.Instance.SetStatusLights(false);
        objrain.SetActive(true);
    }

    public void StopRain()
    {
        rain.Stop();
        objrain.SetActive(false);
    }
}
