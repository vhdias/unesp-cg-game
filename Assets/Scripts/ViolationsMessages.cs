using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViolationsMessages : MonoBehaviour {
    public Text text;
    public int Pontos = 0, MaximoPontos = 3;
    public string mainScene;
    public enum Multas
    {
        ExcedeuVelocidade,
        ExcedeuRe,
        DirecaoErrada,
        FarolApagado
    };
    public enum Alertas
    {
        ApagarFarol
    }
    public Multas multas;
    Multas lastMulta;
    bool timeOk = true;

    IEnumerator Reseta(float t)
    {
        text.text = "Você perdeu";
        yield return new WaitForSecondsRealtime(t);
        SceneManager.LoadScene(mainScene); //Mudar para game
    }

    IEnumerator EsperaParaMultar(float t)
    {
        timeOk = false;
        yield return new WaitForSecondsRealtime(t);
        timeOk = true;
    }

    public void Multa(int i)
    {
        Pontos += i;
        if (Pontos > MaximoPontos)
        {
            StartCoroutine(Reseta(5));
        }
    }

    public void Multa(Multas multa)
    {
        text.color = Color.red;
        if (multa == Multas.DirecaoErrada)
        {
            text.text = "Você está na direção errada";
        }
        else if (multa == Multas.ExcedeuRe)
        {
            text.text = "Você está dando ré por muito tempo";
        }
        else if (multa == Multas.ExcedeuVelocidade)
        {
            text.text = "Você excedeu o limite de velocidade";
        }else if (multa == Multas.FarolApagado)
        {
            text.text = "Você está andando com o farol apagado durante a noite";
        }
        if (lastMulta != multa && timeOk)
        {
            Multa(1);
            StartCoroutine(EsperaParaMultar(5));
        }
        lastMulta = multa;
    }

    public void Alerta(Alertas alerta)
    {
        text.color = Color.yellow;
        if(alerta == Alertas.ApagarFarol)
        {
            text.text = "Verifique o periodo do dia para ligar ou desligar o farol";
        }
    }

}
