using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public BrokentObjectManager manager;

    public float limit;

    float scaleX;
    void Start()
    {
        scaleX = GetComponent<RectTransform>().localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.damage >= limit)
        {
            GetComponent<RectTransform>().localScale = new Vector3(0, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
            GetComponentInParent<PauseMenu>().EndGame();   
        }
        else
        {
            GetComponent<RectTransform>().localScale = new Vector3(scaleX - (manager.damage / scaleX), GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
        }

    }
}
