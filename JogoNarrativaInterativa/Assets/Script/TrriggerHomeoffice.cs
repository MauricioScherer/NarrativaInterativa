using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrriggerHomeoffice : MonoBehaviour
{
    private bool _staytrigger;
    private bool _stayJob;
    private bool _finishJob;
    private bool _podeTrabalhar;
    private GameObject player;
    private KeyCode clickEvent;
    private Event e;

    private KeyCode[] keyCodeSort = new KeyCode[4] {KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A };
    private KeyCode keyCurrent;
    private int countJob;
    private int sortNewKey;
    [SerializeField]
    private TextMeshPro count;
    [SerializeField]
    private TextMeshPro inputCurrent;
    [SerializeField]
    private Image progressBar;

    [SerializeField]
    private GameObject playerTrabalhando;
    [SerializeField]
    private GameObject feedProgresso;
    private int numColor;
    [SerializeField]
    private Color[] mainColor;
    [SerializeField]
    private AudioSource feedAudio;
    [SerializeField]
    private AudioClip[] clips;

    private void Start()
    {
        keyCurrent = keyCodeSort[UnityEngine.Random.Range(0, keyCodeSort.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        StayTrigger();
        Job();
    }

    void OnGUI()
    {
        e = Event.current;
        if (e.isKey)
        {
            clickEvent = e.keyCode;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _staytrigger = true;
            player = other.gameObject;
            if(!_finishJob)
            {
                if(_podeTrabalhar)
                    GameManager.Instance.ViewBtn("Aperte E para trabalhar.");
                else
                    GameManager.Instance.ViewBtn("Melhor ver as notícias antes de trabalhar.");
            }
        }
    }

    private void StayTrigger()
    {
        if (_staytrigger && !_finishJob)
        {
            if (Input.GetKeyDown("e") && _podeTrabalhar)
            {
                if (player.GetComponent<Player>().GetCanMoving())
                {
                    GameManager.Instance.ViewBtn("");
                    player.GetComponent<Player>().SetCanMoving();
                    player.GetComponent<Player>().SetViewPlayer(false);
                    playerTrabalhando.SetActive(true);
                    feedProgresso.SetActive(true);
                    _stayJob = true;
                }
                else
                {
                    GameManager.Instance.ViewBtn("");
                    player.GetComponent<Player>().SetCanMoving();
                    player.GetComponent<Player>().SetViewPlayer(true);
                    playerTrabalhando.SetActive(false);
                    feedProgresso.SetActive(false);
                    _stayJob = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _staytrigger = false;
            GameManager.Instance.ViewBtn("");
        }
    }

    private void Job()
    {
        if (_stayJob && !_finishJob)
        {
            inputCurrent.text = keyCurrent.ToString();

            if (Input.GetKeyUp(clickEvent))
            {
                if (keyCurrent == clickEvent)
                {
                    progressBar.fillAmount += 0.3f;
                    keyCurrent = keyCodeSort[UnityEngine.Random.Range(0, keyCodeSort.Length)];

                    if (progressBar.fillAmount > 0.9f)
                    {
                        progressBar.fillAmount = 0.0f;
                        countJob++;
                        if(countJob < 10)
                        {
                            count.text = countJob.ToString() + "/10";
                            GameManager.Instance.MoreHour();
                        }
                        else
                        {
                            count.text = countJob.ToString() + "/10";
                            GameManager.Instance.MoreHour();
                            _finishJob = true;
                            player.GetComponent<Player>().SetCanMoving();
                            player.GetComponent<Player>().SetViewPlayer(true);
                            playerTrabalhando.SetActive(false);
                            feedProgresso.SetActive(false);
                            GameManager.Instance.FinishjobDia3();
                            _stayJob = false;
                        }
                    }

                    sortNewKey = 0;

                    numColor = numColor == 0 ? numColor = 1 : numColor = 0;
                    inputCurrent.color = mainColor[numColor];

                    feedAudio.clip = clips[0];
                    feedAudio.Play();
                }
                else
                {
                    NewSortKey();
                    feedAudio.clip = clips[1];
                    feedAudio.Play();
                }
            }

            sortNewKey++;

            if(sortNewKey >= 60)
            {
                NewSortKey();
            }
        }
    }

    private void NewSortKey()
    {
        progressBar.fillAmount -= 0.3f;
        keyCurrent = keyCodeSort[UnityEngine.Random.Range(0, keyCodeSort.Length)];
        sortNewKey = 0;

        numColor = numColor == 0 ? numColor = 1 : numColor = 0;
        inputCurrent.color = mainColor[numColor];            
    }

    public void ResetJob()
    {
        _finishJob = false;
        progressBar.fillAmount = 0.0f;
        keyCurrent = keyCodeSort[UnityEngine.Random.Range(0, keyCodeSort.Length)];
        sortNewKey = 0;
        numColor = numColor == 0 ? numColor = 1 : numColor = 0;
        inputCurrent.color = mainColor[numColor];
        countJob = 0;
        count.text = countJob.ToString() + "/10";
    }

    public bool PodeTrabalhar()
    {
        return _podeTrabalhar;
    }

    public void SetPodeTrabalhar(bool p_status)
    {
        _podeTrabalhar = p_status;
    }
}
