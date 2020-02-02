using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public float timeBeforeClose = 5.0f;
    private bool m_isOpen = false;

    private Vector3 m_rotate;



    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0, 3.375f);
        //m_rotate = GetComponent<Transform>().eulerAngles;
    }
    void Update()
    {
        //if(!m_isOpen && GetComponent<Transform>().position.z != m_rotate.z)
        //{
        //    m_isOpen = true;
        //    StartCoroutine(DoorClose());
        //}
    }



    //IEnumerator DoorClose()
    //{
    //    yield return new WaitForSeconds(5.0f);
    //    GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
    //    GetComponent<Transform>().eulerAngles = m_rotate;
    //    m_isOpen = false;
    //}
}
