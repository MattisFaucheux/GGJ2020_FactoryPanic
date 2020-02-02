using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leakeable : MonoBehaviour
{
    private bool m_isLeaking = false;

    public void SetIsLeaking(bool isLeaking)
    {
        m_isLeaking = isLeaking;
    }
}
