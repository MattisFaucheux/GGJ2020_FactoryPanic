using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    [FormerlySerializedAs("pauseMenu")] [SerializeField]
    private GameObject m_pauseMenu;
    [FormerlySerializedAs("soundMenu")] [SerializeField]
    private GameObject m_soundMenu;

    [SerializeField] private GameObject ResumeButtonPause;
    [SerializeField] private GameObject CloseButtonSound;
    
    [SerializeField]
    private EventSystem m_es;


    void Start()
    {
        m_pauseMenu.SetActive(false);
        m_soundMenu.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !m_pauseMenu.activeSelf)
            OpenPause();
        
    }

    private void OpenPause()
    {
        m_pauseMenu.SetActive(true);
        m_es.SetSelectedGameObject(ResumeButtonPause);
        m_soundMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        m_pauseMenu.SetActive(false);
    }

    public void OpenSound()
    {
        m_soundMenu.SetActive(true);
        m_pauseMenu.SetActive(false);
        m_es.SetSelectedGameObject(CloseButtonSound);
    }
    
    public void CloseSound()
    {
        m_soundMenu.SetActive(false);
        m_pauseMenu.SetActive(true);
    }
}
