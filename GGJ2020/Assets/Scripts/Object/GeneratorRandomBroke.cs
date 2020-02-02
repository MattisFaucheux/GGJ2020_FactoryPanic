using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRandomBroke : MonoBehaviour
{
    public int minSecondsBeforeNextBreak = 0;
    public int maxSecondsBeforeNextBreak = 5;

    void Start()
    {
        StartCoroutine(SetGeneratorOnFire(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
    }

    public void SetGeneratorOnFire()
    {
        StartCoroutine(SetGeneratorOnFire(Random.Range(minSecondsBeforeNextBreak, maxSecondsBeforeNextBreak)));
    }

    IEnumerator SetGeneratorOnFire(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<Flammable>().SetIsOnFire(true);

    }
}
