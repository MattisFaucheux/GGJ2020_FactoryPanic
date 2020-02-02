using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLight : MonoBehaviour
{

    public Light ourLight;
    public Transform lever;
    private bool m_isActivated = false;

    private bool m_activatePlayer1 = false;
    private float m_timeActivePlayer1;
    private bool m_activatePlayer2 = false;
    private float m_timeActivePlayer2;

    public float timeToActive = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_activatePlayer1 && m_activatePlayer2 && !m_isActivated)
        {
            m_isActivated = true;
            ourLight.GetComponent<Light>().enabled = true;
            lever.eulerAngles = new Vector3(lever.eulerAngles.x, lever.eulerAngles.y, 100);
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
        m_isActivated = false;
        lever.eulerAngles = new Vector3(lever.eulerAngles.x, lever.eulerAngles.y, 0);
    }
}
