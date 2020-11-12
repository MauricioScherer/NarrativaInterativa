using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSofa : MonoBehaviour
{
    private bool _podeLevantar = true;
    private bool _staySofa;
    private bool _staytrigger;
    private bool _sonoSofa;
    private GameObject player;

    [SerializeField]
    private GameObject playerSofa;
    [SerializeField]
    private GameObject feedbackArrow;

    private void Update()
    {
        StayTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _staytrigger = true;
            player = other.gameObject;
            GameManager.Instance.ViewBtn("Aperte E para sentar no sofá.");
        }
    }

    private void StayTrigger()
    {
        if(_staytrigger)
        {
            if (Input.GetKeyDown("e"))
            {
                if (player.GetComponent<Player>().GetCanMoving())
                {
                    //feedbackTarget.SetActive(false);
                    GameManager.Instance.ViewBtn("");
                    //feedbackArrow.SetActive(false);
                    player.GetComponent<Player>().SetCanMoving();
                    player.GetComponent<Player>().SetViewPlayer(false);
                    playerSofa.SetActive(true);
                    _staySofa = true;

                    print("0");

                    if (GameManager.Instance.GetDiaCurrent() == 1 && GameManager.Instance.GetQuarentena() && !_sonoSofa)
                    {
                        print("1");
                        _sonoSofa = true;
                        GameManager.Instance.StartSonoSofa();
                    }
                }
                else
                {
                    if(_podeLevantar)
                    {
                        GameManager.Instance.ViewBtn("");
                        player.GetComponent<Player>().SetCanMoving();
                        player.GetComponent<Player>().SetViewPlayer(true);
                        playerSofa.SetActive(false);
                        _staySofa = false;
                    }

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
            //feedbackArrow.SetActive(true);
        }
    }

    public bool GetStaySofa()
    {
        return _staySofa;
    }

    public void SetPodeLevantar(bool p_status)
    {
        _podeLevantar = p_status;
    }
}
