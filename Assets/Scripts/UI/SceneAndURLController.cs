using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAndURLController : MonoBehaviour {
    private PauseMenu m_PauseMenu;

    private void Awake()
    {
        m_PauseMenu = GetComponentInChildren<PauseMenu>();
    }

    public void SceneLoad(string sceneName)
    {
        //PauseMenu pauseMenu = (PauseMenu)FindObjectOfType(typeof(PauseMenu));
        m_PauseMenu.MenuOff();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Exit()
    {
        Debug.Log("O jogo irá fechar (Não funciona no editor -- https://docs.unity3d.com/ScriptReference/Application.Quit.html)");
        Application.Quit();
    }
}
