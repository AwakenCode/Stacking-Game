using UnityEngine;
using System.Collections;

public class CoroutineExecutionProvider : MonoBehaviour
{
    public void StartEnumerator(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    public void StopEnumerator(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }
}