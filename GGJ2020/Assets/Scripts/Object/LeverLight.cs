using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLight : MonoBehaviour
{

    public Light ourLight;
    public Light emergencyLight;
    public Transform lever;
    public bool m_isActivated = false;

    private bool m_activatePlayer1 = false;
    private float m_timeActivePlayer1;
    private bool m_activatePlayer2 = false;
    private float m_timeActivePlayer2;

    public float timeToActive = 4.0f;

    public int minSecondsBeforeNextBreak = 10;
    public int maxSecondsBeforeNextBreak = 30;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartNextDefect(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
    }

    // Update is called once per frame
    void Update()
    {
        if(m_activatePlayer1 && m_activatePlayer2 && !m_isActivated)
        {
            m_isActivated = true;
            emergencyLight.GetComponent<Light>().enabled = false;
            ourLight.GetComponent<Light>().enabled = true;
            lever.eulerAngles = new Vector3(lever.eulerAngles.x, lever.eulerAngles.y, 100);
            StartCoroutine(StartNextDefect(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
        }

        if(m_timeActivePlayer1 > 0.0f)
        {
            m_timeActivePlayer1 -= Time.deltaTime;
        }
        else
        {
            m_activatePlayer1 = false;
        }

        if (m_timeActivePlayer2 > 0.0f)
        {
            m_timeActivePlayer2 -= Time.deltaTime;
        }
        else
        {
            m_activatePlayer2 = false;
        }
    }

    public void PlayerActivate(float playerNumber)
    {
        if(playerNumber == 1)
        {
            m_activatePlayer1 = true;
            m_timeActivePlayer1 = timeToActive;
        }
        else if(playerNumber == 2)
        {
            m_activatePlayer2 = true;
            m_timeActivePlayer2 = timeToActive;
        }
    }

    void Disable()
    {
        ourLight.GetComponent<Light>().enabled = false;
        emergencyLight.GetComponent<Light>().enabled = true;
        m_isActivated = false;
        lever.eulerAngles = new Vector3(lever.eulerAngles.x, lever.eulerAngles.y, 0);
    }

    IEnumerator StartNextDefect(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Disable();
        
    }

}
