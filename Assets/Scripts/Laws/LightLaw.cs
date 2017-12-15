using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DayTime
{
    Day,
    Night
};

public class LightLaw : MonoBehaviour {
    public GameObject lights;
    public float timeToChangeLights = 5;
    public DayTime dayTime; 
    bool timeOk = false;

    [System.Serializable]
    public class TriggerEvent : UnityEvent { }
    public TriggerEvent MultaFarol = new TriggerEvent();
    public TriggerEvent AlertaFarol = new TriggerEvent();

    private void Awake()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        timeOk = false;
        yield return new WaitForSecondsRealtime(timeToChangeLights);
        timeOk = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.transform.parent;
        if (player)
        {
            player = player.parent;
            if (player)
            {
                StartCoroutine(Delay());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.transform.parent;
        if (player)
        {
            player = player.parent;
            if (player && !StatusOk(lights.activeInHierarchy) && timeOk && player.gameObject.name == "Jogador(Carro)")
            {
                if (dayTime == DayTime.Day)
                {
                    Debug.Log("Recomenda-se desligar o farol");
                    AlertaFarol.Invoke();
                }
                else
                {
                    Debug.Log("Ligue o farol");
                    MultaFarol.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.transform.parent;
        if (player)
        {
            player = player.parent;
            if (player && !lights.activeInHierarchy && player.gameObject.name == "Jogador(Carro)")
            {
                if (dayTime == DayTime.Night)
                {
                    Debug.Log("Desligue o farol");
                    AlertaFarol.Invoke();
                }
                else
                {
                    Debug.Log("Ligue o farol");
                    AlertaFarol.Invoke();
                }
            }
        }
    }

    private bool StatusOk(bool lightOn)
    {
        return (dayTime == DayTime.Night && lightOn) || (dayTime == DayTime.Day && !lightOn);
    }
}
