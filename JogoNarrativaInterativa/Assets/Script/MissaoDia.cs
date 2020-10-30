using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissaoDia : MonoBehaviour
{
    [SerializeField]
    private Text[] descricaoMissao;

    public GameObject[] missionFinish;

    public void SetMissionFinish(int p_status)
    {
        missionFinish[p_status].SetActive(true);
    }

    public void MissionDia1Parte2()
    {
        foreach(GameObject _obj in missionFinish)
            _obj.SetActive(false);
        descricaoMissao[3].gameObject.SetActive(false);

        descricaoMissao[0].GetComponent<Text>().text = "-Jantar";
        descricaoMissao[1].GetComponent<Text>().text = "-Escovar os dentes";
        descricaoMissao[2].GetComponent<Text>().text = "-Dormir";
    }

    public void MissionDia2Parte2()
    {
        foreach (GameObject _obj in missionFinish)
            _obj.SetActive(false);

        descricaoMissao[1].gameObject.SetActive(false);
        descricaoMissao[2].gameObject.SetActive(false);
        descricaoMissao[3].gameObject.SetActive(false);

        descricaoMissao[0].GetComponent<Text>().text = "-Descansar no Sofá";
    }

    public void MissionDia2Parte3()
    {
        foreach (GameObject _obj in missionFinish)
            _obj.SetActive(false);

        descricaoMissao[1].gameObject.SetActive(false);
        descricaoMissao[2].gameObject.SetActive(false);
        descricaoMissao[3].gameObject.SetActive(false);

        descricaoMissao[0].GetComponent<Text>().text = "-Dormir";
    }
}
