using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    public enum ObjectInHand
    {
        None = 0,
        Tape,
        Plank,
        Mop,
        Extinguisher
    }

    private GameObject m_pickable = null;
    private ObjectInHand m_objectInHand;
    [SerializeField]
    private Transform m_handPlaceHolder;
    private float m_objectTimer = 0.0f;

    void Update()
    {
        if (m_pickable != null)
        {
            m_objectTimer -= Time.deltaTime;
            if (Input.GetButtonDown("InteractPlayer" + gameObject.GetComponent<Player>().playerNumber) && m_objectTimer <= 0.0f)
            {
                m_pickable.GetComponent<Transform>().SetParent(null);
                m_pickable = null;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            if (other.gameObject.GetComponentInParent<Player>() && other.gameObject.GetComponentInParent<Player>().playerNumber != gameObject.GetComponentInParent<Player>().playerNumber)
            {
                if (Input.GetButtonDown("InteractPlayer" + gameObject.GetComponent<Player>().playerNumber))
                {
                    other.gameObject.GetComponentInParent<Catch>().m_pickable = null;
                    m_objectTimer = 1.0f;
                    other.gameObject.GetComponent<Transform>().SetParent(m_handPlaceHolder);
                    m_pickable = other.gameObject;
                }

            }

            if (Input.GetButtonDown("InteractPlayer" + gameObject.GetComponent<Player>().playerNumber))
            {
                if (m_pickable == null)
                {
                    m_objectTimer = 1.0f;
                    other.gameObject.GetComponent<Transform>().SetParent(m_handPlaceHolder);
                    //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    m_pickable = other.gameObject;

                }
            }
        }
    }

    public void SetObjectInHand(ObjectInHand objectInHand)
    {
        m_objectInHand = objectInHand;
    }
    public ObjectInHand GetObjectInHand()
    {
        return m_objectInHand;
    }

    public void DestroyPickable()
    {
        Destroy(m_pickable);
        m_objectInHand = ObjectInHand.None; 
        m_pickable = null;
        
        
    }
}
