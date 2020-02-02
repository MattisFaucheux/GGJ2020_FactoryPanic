using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    void Update()
    {
        if (GetComponentInParent<Player>())
        {
            GetComponentInParent<Catch>().SetObjectInHand(Catch.ObjectInHand.Plank);
        }
    }
}
