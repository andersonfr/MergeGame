using UnityEngine;
using System.Collections;

public class WorldTimer : MonoBehaviour
{
    float time;
    float boxRate;

    [SerializeField]
    private GameObject boxObject;

    private void Update()
    {
        time += Time.deltaTime;
    }

    IEnumerator BoxSpawn()
    {
        yield return null;
    }
}