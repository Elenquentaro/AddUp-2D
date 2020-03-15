using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private ItemData itemData = null;

    [SerializeField] private GameObject itemPrefab = null;

    [SerializeField] private RectTransform contentArea = null;

    private Vector3 basePosition;
    private Vector3 openedPosition;

    private bool opened = false;

    void Awake()
    {
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
            Instantiate(itemPrefab, contentArea).GetComponent<ItemBuy>().Init(item);
        }
    }

    public void SwitchVisibility()
    {
        StopAllCoroutines();
        opened = !opened;
        this.SmoothMoveTo(opened ? openedPosition : basePosition);
        GameStateManager.SwitchGameState(!opened);
    }
}
