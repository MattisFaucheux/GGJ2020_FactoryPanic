using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    public int valveNumber = 0;
    public float activDuration = 1.0f;
    private bool m_isActivated = false;
    private float m_timer = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timer <= 0.0f)
        {
            m_timer -= Time.deltaTime;
        }
        else if (m_isActivated)
        {
            m_isActivated = false;
        }
    }
}
