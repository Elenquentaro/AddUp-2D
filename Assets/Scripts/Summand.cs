using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Summand : MonoBehaviour
{
    public static IntEvent onIncrease = new IntEvent();

    private GenericAction<CellIndex, Vector2> onRelease;

    [SerializeField] private Text valueDisplay = null;

    private int number = 1;
    public int Number => number;

    private CellIndex ParentCellIndex { get; set; }

    private Borders borders;


    public void AssignBehaviour(GenericAction<CellIndex, Vector2> action)
    {
        onRelease = action;
        borders = GridField.Borders;
    }

    public void AssignValue(CellIndex index, int value = 1)
    {
        ParentCellIndex = index;
        if (number < value) number = value;
        valueDisplay.text = number.ToString();
        SetPosToParentCell();
    }

    public void IncreaseValue()
    {
        onIncrease?.Invoke(number);
        AssignValue(ParentCellIndex, number + 1);
    }

    public void SetPosToParentCell()
    {
        transform.position = ParentCellIndex.GetPosition(GridField.Borders.BaseRect.min, GridField.CellSize);
    }

    public void OnMouseUp()
    {
        onRelease?.Invoke(ParentCellIndex, transform.position);
        SetPosToParentCell();
    }

    public void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        objPosition.x = Mathf.Clamp(objPosition.x, borders.left, borders.right);
        transform.position = objPosition;
    }
}