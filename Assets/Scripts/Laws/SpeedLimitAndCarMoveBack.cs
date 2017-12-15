using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpeedLimitAndCarMoveBack : MonoBehaviour {
    public float maxSpeed = 80, speedScale = 1, limiteRe = 5;
    //Artigo 192 define quanto as restrições de marcha ré de forma subjetiva. Definimos 5 segundos.
    public Text speedText;
    private float timerRe;

    [System.Serializable]
    public class TriggerEvent : UnityEvent { }
    public TriggerEvent MultaRe = new TriggerEvent();
    public TriggerEvent MultaVelocidade = new TriggerEvent();

    public void MaxSpeed(float v)
    {
        maxSpeed = v;
    }

	void FixedUpdate () {
        float speed = VelocidadeKPH(GetComponent<Rigidbody>()) * speedScale;
        bool re = Re(GetComponent<Rigidbody>());
        if (re) timerRe += Time.deltaTime;
        else timerRe = 0;
        if(timerRe > limiteRe)
        {
            Debug.Log("Excedeu o tempo de ré" + timerRe);
            MultaRe.Invoke();
        }
        speedText.text = Mathf.RoundToInt(speed) + "Km/H" + ((re == true) ? "(Ré)":"");
        if (speed > maxSpeed)
        {
            Debug.Log("Excedeu o limite de velocidade" + speed);
            MultaVelocidade.Invoke();
        }
	}

    public static float VelocidadeKPH(Rigidbody body)
    {
        return body.velocity.magnitude * 3.6f;
    }

    public static bool Re(Rigidbody body)
    {
        return Mathf.RoundToInt(body.GetComponent<Transform>().InverseTransformVector(body.velocity).z) < 0;
    }
}
