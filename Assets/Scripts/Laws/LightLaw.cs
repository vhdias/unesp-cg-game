using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                }
                else
                {
                    Debug.Log("Ligue o farol");
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
                }
                else
                {
                    Debug.Log("Ligue o farol");
                }
            }
        }
    }

    private bool StatusOk(bool lightOn)
    {
        return (dayTime == DayTime.Night && lightOn) || (dayTime == DayTime.Day && !lightOn);
    }
}
