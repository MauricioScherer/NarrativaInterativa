using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBinoculo : MonoBehaviour
{
    //[SerializeField]
    //private GameObject feedbackTarget;
    private GameObject mainCamera;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //feedbackTarget.SetActive(true);
            GameManager.Instance.ViewBtn("Aperte E para usar o binóculo.");
            feedbackArrow.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
            if (other.GetComponent<Player>().GetCanMoving())
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(false);
                other.GetComponent<Player>().SetCanMoving();
                mainCamera.transform.position = posCameraBinoculo.position;
                mainCamera.transform.rotation = posCameraBinoculo.rotation;
            }
            else
            {
                //feedbackTarget.SetActive(false);
                GameManager.Instance.ViewBtn("");
                feedbackArrow.SetActive(true);
                other.GetComponent<Player>().SetCanMoving();
                mainCamera.transform.position = posCameraSala.position;
                mainCamera.transform.rotation = posCameraSala.rotation;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //feedbackTarget.SetActive(false);
            GameManager.Instance.ViewBtn("");
            feedbackArrow.SetActive(true);
        }
    }
}
