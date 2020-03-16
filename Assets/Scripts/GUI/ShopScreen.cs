using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private ItemData itemData = null;

    [SerializeField] private GameObject itemPrefab = null;

    [SerializeField] private RectTransform contentArea = null;

    [SerializeField] private GameObject errorPanel = null;

    private Vector3 basePosition;
    private Vector3 openedPosition;

    private bool opened = false;

    private List<GameObject> itemObjects = new List<GameObject>();

    void Awake()
    {
        ItemBuy.onCannotPurchase.AddListener(() => errorPanel.SetActive(true));
        basePosition = transform.position;
        openedPosition = new Vector3(basePosition.x, basePosition.y + Screen.height); //GetComponent<RectTransform>().rect.height);
    }

    void Start()
    {
        float spacing = contentArea.GetComponent<VerticalLayoutGroup>().spacing
               + itemPrefab.GetComponent<RectTransform>().rect.height;
        foreach (var item in itemData.Items)
        {
            contentArea.offsetMin -= new Vector2(0, spacing);
            var shopItem = Instantiate(itemPrefab, contentArea).GetComponent<ItemBuy>();
            shopItem.Init(item);
            itemObjects.Add(shopItem.gameObject);
        }
        SetActiveItems(false);
    }

    public void SwitchVisibility()
    {
        StopAllCoroutines();
        opened = !opened;

        if (opened) SetActiveItems(true);
        this.MoveWithAction(opened ? openedPosition : basePosition, () =>
        {
            if (!opened) SetActiveItems(false);
        });
        GameStateManager.SwitchGameState(!opened);
    }

    private void SetActiveItems(bool value)
    {
        contentArea.gameObject.SetActive(value);
        // StartCoroutine(ActivateRoutine(value));
    }

    private IEnumerator ActivateRoutine(bool value)
    {
        for (int i = 0; i < itemObjects.Count; i++)
        {
            itemObjects[i].SetActive(value);
            yield return new WaitForFixedUpdate();
        }
    }
}
