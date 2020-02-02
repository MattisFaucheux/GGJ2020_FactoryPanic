using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveConvoyer : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;
    [SerializeField]
    private List<GameObject> m_objects;

    [SerializeField]
    private ConveyorBelt m_ref;
    [SerializeField]
    private Material m_onButtonColor;
    [SerializeField]
    private Material m_offButtonColor;

    public float m_cooldown = 5.0f;
    private float m_timer = 0.0f;
    private bool m_ready = true;

    public RepairConvoyer repairConvoyer;

    private void Update()
    {
        if (m_timer >= 0.0f)
        {
            m_timer -= Time.deltaTime;
        }
        else if (!m_ready)
        {
            m_ready = true;
            GetComponent<Renderer>().materials[1].CopyPropertiesFromMaterial(m_offButtonColor);
        }

    }

    public void SpawnItem()
    {
        if (!m_ready || !repairConvoyer.m_isActivated)
        {
            return;
        }

        m_ref.ResetConveyor();
        for (int nbObject = 0 ; nbObject < m_objects.Count; nbObject++)
        {
            Instantiate(m_objects[nbObject], m_transform.position, Quaternion.identity);
        }
        m_ready = false;
        m_timer = m_cooldown;
        GetComponent<Renderer>().materials[1].CopyPropertiesFromMaterial(m_onButtonColor);
    }
}
