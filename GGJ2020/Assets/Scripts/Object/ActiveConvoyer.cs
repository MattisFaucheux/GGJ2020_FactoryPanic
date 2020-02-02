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

    public void SpawnItem()
    {
        m_ref.ResetConveyor();
        for (int nbObject = 0 ; nbObject < m_objects.Count; nbObject++)
        {
            Debug.Log("Spawn");
            Instantiate(m_objects[nbObject], m_transform.position, Quaternion.identity);
        }
        
    }
}
