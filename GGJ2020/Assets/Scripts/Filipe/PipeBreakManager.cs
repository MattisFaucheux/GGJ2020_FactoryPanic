using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBreakManager : MonoBehaviour
{
    public List<Light> m_pipelights;
    [SerializeField] private List<GameObject> m_pipes;
    [SerializeField] private List<GameObject> m_valve;

    public List<bool> isBorked;
    
    public int minSecondsBeforeNextBreak = 0;
    public int maxSecondsBeforeNextBreak = 5;

    private List<bool> isFixingValve;
    private List<float> timeFixingValve;
    private List<bool> isFixingPipe;
    private List<float> timeFixingPipe;

    public float timeToActive = 4.0f;

    void Start()
    {
        StartCoroutine(StartNextDefect(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
        isFixingValve = new List<bool>();
        isFixingPipe = new List<bool>();

        timeFixingPipe = new List<float>();
        timeFixingValve = new List<float>();
        for (int i = 0; i < 3; i++)
        {
            isBorked.Add(false);
            isFixingValve.Add(false);
            isFixingPipe.Add(false);
            timeFixingPipe.Add(0.0f);
            timeFixingValve.Add(0.0f);
        }
    }

    void Update()
    {
        for(int i =0; i < 3; i++)
        {
            if(isFixingPipe[i] && isFixingValve[i])
            {
                ResetValveAndPipe(i);
                m_pipelights[i].GetComponent<Light>().enabled = false;
            }
                                 
            if(timeFixingPipe[i] > 0.0f)
            {
                timeFixingPipe[i] -= Time.deltaTime;
            }
            else
            {
                isFixingPipe[i] = false;
            }

            if (timeFixingValve[i] > 0.0f)
            {
                timeFixingValve[i] -= Time.deltaTime;
            }
            else
            {
                isFixingValve[i] = false;
            }
        }
    }

    IEnumerator StartNextDefect(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        int index = Random.Range(0, 3);

        if (isBorked[index].Equals(true))
            yield return StartNextDefect(1);

        m_pipes[index].GetComponent<Animator>().SetBool("play", true);
        m_pipes[index].transform.GetComponentInChildren<ParticleSystem>().Play();
        m_pipelights[index].GetComponent<Light>().enabled = true;
        isBorked[index] = true;

        yield return StartNextDefect(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak));
    }

    public void FixValve(string name)
    {
        int index = 0;
        switch (name)
        {
            case "left":
                index = 0;
                break;
            case "middle":
                index = 1;
                break;
            case "right":
                index = 2;
                break;
        }

        timeFixingValve[index] = timeToActive;
        isFixingValve[index] = true;
    }
    
    public void FixPipe(string name)
    {
        int index = 0;
        switch (name)
        {
            case "left":
                index = 0;
                break;
            case "middle":
                index = 1;
                break;
            case "right":
                index = 2;
                break;
        }

        timeFixingPipe[index] = timeToActive;
        isFixingPipe[index] = true;
    }

    void ResetValveAndPipe(int index)
    {
        isBorked[index] = false;
        m_pipes[index].transform.GetComponentInChildren<ParticleSystem>().Stop();
        m_pipes[index].GetComponent<Animator>().SetBool("play", false);
    }

}