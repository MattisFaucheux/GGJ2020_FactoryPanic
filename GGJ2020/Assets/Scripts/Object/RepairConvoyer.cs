using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairConvoyer : MonoBehaviour
{

    public bool m_isActivated = false;

    private bool m_activatePlayer1 = false;
    private float m_timeActivePlayer1;
    private bool m_activatePlayer2 = false;
    private float m_timeActivePlayer2;

    public float timeToActive = 4.0f;

    public Material on;
    public Material off;

    [SerializeField]
    private Material m_breakButtonColor;
    [SerializeField]
    private GameObject m_buttonToRepare;

    // Start is called before the first frame update
    void Start()
    {
        Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_activatePlayer1 && m_activatePlayer2 && !m_isActivated)
        {
            m_isActivated = true;
            GetComponent<MeshRenderer>().materials[1].CopyPropertiesFromMaterial(on);
            m_buttonToRepare.GetComponent<Renderer>().materials[1].CopyPropertiesFromMaterial(off);
        }

        if (m_timeActivePlayer1 > 0.0f)
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
        if (playerNumber == 1)
        {
            m_activatePlayer1 = true;
            m_timeActivePlayer1 = timeToActive;
        }
        else if (playerNumber == 2)
        {
            m_activatePlayer2 = true;
            m_timeActivePlayer2 = timeToActive;
        }
    }

    void Disable()
    {
        m_isActivated = false;
        GetComponent<MeshRenderer>().materials[1].CopyPropertiesFromMaterial(off);
    }

}
