using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummandSpawner : MonoBehaviour
{
    [SerializeField] private GridField grid;

    [SerializeField] private Transform spawnArea = null;

    [SerializeField] private GameObject summandPrefab = null;

    [SerializeField, Range(1, 10)] private int spawnRate = 10;

    void Start()
    {
        StartCoroutine(Spawn(spawnRate));
    }

    private IEnumerator Spawn(float rate)
    {
        while (true)
        {
            grid.InsertToRandomEmptyCell(Instantiate(summandPrefab, spawnArea).GetComponent<Summand>());
            yield return new WaitForSeconds(rate);
        }
    }
}
