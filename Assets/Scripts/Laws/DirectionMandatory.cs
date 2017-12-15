using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirectionMandatory : MonoBehaviour {
    public Vector3 mandatoryDirection;
    float signalX, signalY, signalZ;
    bool isX, isY, isZ, timeOk = false;

    [System.Serializable]
    public class TriggerEvent : UnityEvent { }
    public TriggerEvent wrongDirection = new TriggerEvent();

    private void Start()
    {
        signalX = Mathf.Sign(mandatoryDirection.x);
        signalY = Mathf.Sign(mandatoryDirection.y);
        signalZ = Mathf.Sign(mandatoryDirection.z);
        Debug.Log(signalX + "   " + signalY + "   " + signalZ);
        isX = Mathf.Abs(mandatoryDirection.x) == 1;
        isY = Mathf.Abs(mandatoryDirection.y) == 1;
        isZ = Mathf.Abs(mandatoryDirection.z) == 1;
        if ((Mathf.Abs(mandatoryDirection.magnitude) != 1) || (!isX && !isY && !isZ))
        {
            Debug.Log(mandatoryDirection.magnitude + "    " + Mathf.Abs(mandatoryDirection.magnitude));
            Debug.Log(isX.ToString() + isY.ToString() + isZ.ToString());
            Destroy(this);
            throw new UnityException("Direção inválida");
        }
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        timeOk = false;
        yield return new WaitForSecondsRealtime(5);
        timeOk = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (timeOk)
        {
            var player = other.transform.parent;
            if (player)
            {
                player = player.parent;
                if (!player) return;
                Vector3 currentDirection = GetLocalDirection(player);
                //Debug.Log(currentDirection);
                //Debug.Log(signalX + "   " + signalY + "   " + signalZ);
                //Debug.Log(Round(currentDirection.x) + "   " + Round(currentDirection.y) + "   " + Round(currentDirection.z));
                if ((isX && Round(currentDirection.x) * signalX < 0) || 
                    (isY && Round(currentDirection.y) * signalY < 0) ||
                    (isZ && Round(currentDirection.z) * signalZ < 0))
                {
                    WrongDirection();
                }
            }
        }
    }

    float Round(float f)
    {
        var i = f * 100;
        //Debug.Log(Mathf.Round(i) / 100);
        return Mathf.Round(i) / 100;
    }

    void WrongDirection()
    {
        Debug.Log("Você está na direção contrária da via");
        wrongDirection.Invoke();
    }

    Vector3 GetLocalDirection(Transform body)
    {
        return transform.InverseTransformDirection(body.GetComponent<Rigidbody>().velocity);
    }
}
