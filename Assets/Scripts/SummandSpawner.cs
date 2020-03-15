using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummandSpawner : MonoBehaviour
{
    [SerializeField] private GridField grid = null;

    [SerializeField] private Transform spawnArea = null;

    [SerializeField] private GameObject summandPrefab = null;

    // [SerializeField] private int autosaveRate = 3;

    private float lastSpawnTime = 0;
    private float timeRemains = 0;

    void Awake()
    {
        GameStateManager.onGameStateChanged.AddListener(OnGameStateChanged);
        ItemBuy.onPurchase.AddListener(Spawn);
    }

    void Start()
    {
        var savedSummands = DataManager.CurrentProgress.summandsOnGrid;
        foreach (SummandInfo info in savedSummands)
        {
            HardSpawn(info.value, info.position);
        }
        StartCoroutine(SpawnRoutine(DataManager.CurrentSettings.SummandSpawnRate));
    }

    private IEnumerator SpawnRoutine(float rate)
    {
        while (grid.HasEmptyCells)
        {
            Spawn(grid.HalfFilled ? UnityEngine.Random.Range(1, grid.MaxSummandAtField) : 1);
            lastSpawnTime = Time.time;
            yield return new WaitForSeconds(rate);
        }
    }

    private void Spawn(int summandValue)
    {
        if (grid.HasEmptyCells)
        {
            grid.InsertToRandomEmptyCell(Instantiate(summandPrefab, spawnArea).GetComponent<Summand>(), summandValue);
        }
        else
        {
            GameOver();
        }
    }

    public void Spawn(ShopItem item)
    {
        Spawn(item.number);
    }

    public void HardSpawn(int summandValue, CellIndex index)
    {
        grid.InsertToCell(index, Instantiate(summandPrefab, spawnArea).GetComponent<Summand>(), summandValue);
    }

    public void GameOver()
    {
        StopAllCoroutines();
        Debug.Log("Game over!");
    }

    public void OnGameStateChanged(bool isPlaying)
    {
        StopAllCoroutines();
        if (isPlaying)
        {
            this.DelayedAction(timeRemains, () => StartCoroutine(SpawnRoutine(DataManager.CurrentSettings.SummandSpawnRate)));
        }
        else
        {
            DataManager.SaveGridState(grid.GetSummandsOnGrid());
            timeRemains = Mathf.Clamp(Time.time - lastSpawnTime, 0, DataManager.CurrentSettings.SummandSpawnRate);
        }
    }
}
