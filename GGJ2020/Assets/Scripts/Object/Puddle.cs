using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Puddle : MonoBehaviour
{

    public int minSecondsBeforeNextBreak = 10;
    public int maxSecondsBeforeNextBreak = 30;

    public bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        Disable();
        StartCoroutine(StartNextDefect(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator StartNextDefect(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Enable();
    }

    void Enable()    
    {
        AudioManager.instance.SetIsOilSpawning(true);
        isActivated = true;

        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void Disable()
    {
        isActivated = false;

        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(StartNextDefect(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
    }

}

