using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokentObjectManager : MonoBehaviour
{

    public RepairConvoyer repairConvoyer;
    public LeverLight leverLight;
    public Puddle puddle;
    public Flammable flammable;
    public PipeBreakManager PipeBreakManager;

    public float damage = 0.0f;

    public float diviseur = 10.0f;


    // Update is called once per frame
    void Update()
    {
        int num = 0;

        if (!repairConvoyer.m_isActivated)
        {
            num += 1;
        }

        if (!leverLight.m_isActivated)
        {
            num += 1;
        }

        if (puddle.isActivated)
        {
            num += 1;
        }

        if(flammable.m_isOnfire)
        {
            num += 1;
        }

        for (int i = 0; i < 3; i++)
        {
            if (PipeBreakManager.isBorked[i])
            {
                num += 1;
            }
        }

        damage += (num * Time.deltaTime) / diviseur;
    }
}
