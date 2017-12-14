using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitchController : MonoBehaviour
{
    public GameObject[] cameras;
    public Text text;

    private int m_CurrentActiveObject = 0;


    private void OnEnable()
    {
        text.text = cameras[m_CurrentActiveObject].name;
    }


    public void NextCamera()
    {
        m_CurrentActiveObject = ++m_CurrentActiveObject % cameras.Length;
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(i == m_CurrentActiveObject);
        }
        text.text = cameras[m_CurrentActiveObject].name;
    }
}

