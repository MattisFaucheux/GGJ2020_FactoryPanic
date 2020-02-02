using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ConveyorBelt : MonoBehaviour
{
    [FormerlySerializedAs("path")] [SerializeField]
    private List<Transform> m_path;

    [FormerlySerializedAs("speed")] [SerializeField]
    private float m_speed = 2;

    private bool m_canMove = true;

    private int m_currentIndex;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable") && m_canMove)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, m_path[m_currentIndex].position,
                m_speed * Time.deltaTime);
            if (Vector3.Distance(other.transform.position, m_path[m_currentIndex].position) < 0.5f)
            {
                if (m_currentIndex + 1 < m_path.Count)
                {
                    m_currentIndex++;
                }
                else
                {
                    m_canMove = false;
                }
            }
        }
    }

    public void ResetConveyor()
    {
        m_currentIndex = 0;
        m_canMove = true;
    }

}
