using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBreakManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_pipes;
    [SerializeField] private List<Material> m_pipelights;
    [SerializeField] private List<Material> m_valveLights;
    private List<bool> isBorked;

    [SerializeField] private List<GameObject> m_valve;

    public int minSecondsBeforeNextBreak = 0;
    public int maxSecondsBeforeNextBreak = 5;

    public List<bool> isFixingValve;
    public List<bool> isFixingPipe;

    void Start()
    {
        StartCoroutine(StartNextDefect(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
        isBorked = new List<bool>();
        isFixingValve = new List<bool>();
        isFixingPipe = new List<bool>();

        for (int i = 0; i < 3; i++)
        {
            isBorked.Add(false);
            isFixingValve.Add(false);
            isFixingPipe.Add(false);
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
        isBorked[index] = true;
        float colx = Random.Range(0, 256);
        float coly = Random.Range(0, 256);
        float colz = Random.Range(0, 256);
        m_pipelights[index].SetColor("_Color", new Color(colx / 255.0f, coly / 255.0f, colz / 255.0f));
        m_valveLights[index].SetColor("_Color", new Color(colx / 255.0f, coly / 255.0f, colz / 255.0f));

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

        isFixingPipe[index] = true;
    }

    void StoppedFixingValve(int valveIndex)
    {
        isFixingValve[valveIndex] = false;
    }
    
    void StoppedFixingPipe(int valveIndex)
    {
        isFixingPipe[valveIndex] = false;
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < 3; i++)
        {
            m_pipelights[i].SetColor("_Color", Color.white);
            m_valveLights[i].SetColor("_Color", Color.white);
        }
    }
}