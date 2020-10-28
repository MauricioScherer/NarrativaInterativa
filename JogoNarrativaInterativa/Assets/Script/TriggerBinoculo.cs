using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBinoculo : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject player;
    private bool _stayTrigger;
    [SerializeField]
    private GameObject feedbackArrow;

    [SerializeField]
    private Transform posCameraBinoculo;
    [SerializeField]
    private Transform posCameraSala;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        Staytrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            _stayTrigger = true;
            GameManager.Instance.ViewBtn("Aperte E para usar o binóculo.");
            //feedbackArrow.SetActive(false);
        }
    }

    private void Staytrigger()
    {
        if(_stayTrigger)
        {
            if (Input.GetKeyDown("e"))
            {
                if (player.GetComponent<Player>().GetCanMoving())
                {
                    GameManager.Instance.ViewBtn("");
                    GameManager.Instance.ViewLenteBinoculo(true);
                    //feedbackArrow.SetActive(false);
                    player.GetComponent<Player>().SetCanMoving();
                    mainCamera.transform.position = posCameraBinoculo.position;
                    mainCamera.transform.localRotation = posCameraBinoculo.localRotation;

                    mainCamera.GetComponent<Camera>().fieldOfView = 20.0f;
                    mainCamera.GetComponent<Binoculo>().enabled = true;
                }
                else
                {
                    GameManager.Instance.ViewBtn("");
                    GameManager.Instance.ViewLenteBinoculo(false);
                    //feedbackArrow.SetActive(true);
                    player.GetComponent<Player>().SetCanMoving();
                    mainCamera.transform.position = posCameraSala.position;
                    mainCamera.transform.rotation = posCameraSala.rotation;

                    mainCamera.GetComponent<Camera>().fieldOfView = 60.0f;
                    mainCamera.GetComponent<Binoculo>().enabled = false;
                }
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
}
