using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [FormerlySerializedAs("pauseMenu")]
    [SerializeField]
    private GameObject m_pauseMenu;
    [FormerlySerializedAs("soundMenu")]
    [SerializeField]
    private GameObject m_soundMenu;

    [SerializeField] private GameObject ResumeButtonPause;
    [SerializeField] private GameObject CloseButtonSound;

    [SerializeField]
    private EventSystem m_es;
    private float m_timer;

    public GameObject gameOverUI;

    void Start()
    {
        m_pauseMenu.SetActive(false);
        m_soundMenu.SetActive(false);
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {

        if (!m_pauseMenu.activeSelf)
        {
            m_timer += Time.deltaTime;
            string minutes = Mathf.Floor(m_timer / 60).ToString("00");
            string seconds = (m_timer % 60).ToString("00");
            FindObjectOfType<Text>().text = string.Format("{0}:{1}", minutes, seconds);
        }

        if (GetComponentInChildren<Health>())
        {

        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !m_pauseMenu.activeSelf)
        {
            OpenPause();
        }
    }

    private void OpenPause()
    {
        Time.timeScale = 0;
        m_pauseMenu.SetActive(true);
        m_es.SetSelectedGameObject(ResumeButtonPause);
        m_soundMenu.SetActive(false);
    }

    public void Quit()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        m_pauseMenu.SetActive(false);
        Time.timeScale = 1;
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

    private void ResetTimer()
    {
        m_timer = 0.0f;
    }

    public void EndGame()
    {
        Debug.Log("ouiouioui222222");
        gameOverUI.SetActive(true);
        //SceneManager.LoadScene("EndScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");           
    }
}
