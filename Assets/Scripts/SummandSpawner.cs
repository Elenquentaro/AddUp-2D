using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummandSpawner : MonoBehaviour
{
    public static bool HasEmptyCells = true;

    [SerializeField] private GridField grid = null;

    [SerializeField] private Transform spawnArea = null;

    [SerializeField] private GameObject summandPrefab = null;

    private float lastSpawnTime = 0;
    private float timeRemains = 0;

    private List<Summand> summandsOnGrid = new List<Summand>();
    private Queue<Summand> summandsPool = new Queue<Summand>();

    void Awake()
    {
        Summand.onRemove.AddListener((summand) =>
        {
            AddSummandToPool(summand);
            DataManager.SaveGridState(summandsOnGrid);
        });
        GameStateManager.onGameStateChanged.AddListener(OnGameStateChanged);
        ItemBuy.onPurchase.AddListener(Spawn);
    }

    void Start()
    {
        int poolCount = grid.GridSize * grid.GridSize;
        for (int i = 0; i < poolCount; i++)
        {
            Summand summand = Instantiate(summandPrefab, spawnArea).GetComponent<Summand>();
            AddSummandToPool(summand);
        }

        var savedSummands = DataManager.CurrentProgress.summandsOnGrid;
        foreach (SummandInfo info in savedSummands)
        {
            HardSpawn(info.value, info.position);
        }
        StartCoroutine(SpawnRoutine(DataManager.CurrentSettings.SummandSpawnRate));
    }

    private IEnumerator SpawnRoutine(float rate)
    {
        while (summandsPool.Count > 0)
        {
            Spawn(summandsOnGrid.Count >= summandsPool.Count ?
                UnityEngine.Random.Range(1, grid.MaxSummandAtField) : 1);
            lastSpawnTime = Time.time;
            yield return new WaitForSeconds(rate);
        }
    }

    private void Spawn(int summandValue)
    {
        if (summandsPool.Count > 0)
        {
            grid.InsertToRandomEmptyCell(GetSummandFromPool(), summandValue);
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
        grid.InsertToCell(index, GetSummandFromPool(), summandValue);
    }

    public void AddSummandToPool(Summand summand)
    {
        summand.gameObject.SetActive(false);
        summandsOnGrid.Remove(summand);
        summandsPool.Enqueue(summand);
        if (!HasEmptyCells) HasEmptyCells = true;
    }

    public Summand GetSummandFromPool()
    {
        if (summandsPool.Count == 0)
        {
            return Instantiate(summandPrefab, spawnArea).GetComponent<Summand>();
        }
        var summand = summandsPool.Dequeue();
        if (summandsPool.Count == 0) HasEmptyCells = false;
        summand?.gameObject.SetActive(true);
        summandsOnGrid.Add(summand);
        DataManager.SaveGridState(summandsOnGrid);
        return summand;
    }

    /*
    при необходимости в метод GameOver можно будет добавить
    активацию события проигрыша с появлением соответствующего окошка,
    обнулением поля и тому подобным.
    */
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
            timeRemains = Mathf.Clamp(Time.time - lastSpawnTime, 0, DataManager.CurrentSettings.SummandSpawnRate);
        }

        for (int i = 0; i < summandsOnGrid.Count; i++)
        {
            summandsOnGrid[i].GetComponent<BoxCollider2D>().enabled = isPlaying;
        }
    }
}
