using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : MonoBehaviour
{
    void Update()
    {
        if (GetComponentInParent<Player>())
        { 
            GetComponentInParent<Catch>().SetObjectInHand(Catch.ObjectInHand.Mop);
        }
    }
}
