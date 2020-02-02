using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_particleSystem;
    public bool m_isOnfire = false;
    


    public void SetIsOnFire(bool isOnFire)
    {
        m_isOnfire = isOnFire;
        if (m_isOnfire)
        {
            m_particleSystem.Play();
        }
        else
        {
            m_particleSystem.Stop();
            GetComponent<GeneratorRandomBroke>().SetGeneratorOnFire();
            AudioManager.instance.SetIsOnFire(false);
        }
        
    }

    public bool GetIsOnFire()
    {
        return m_isOnfire;
    }
}
