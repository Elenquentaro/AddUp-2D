using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Summand : MonoBehaviour
{
    [SerializeField] private Text valueDisplay = null;

    private int number = 1;
    public int Number => number;

    private Rect bounds;

    private CellIndex ParentCellIndex { get; set; }

    private GenericAction<CellIndex, Vector2> onRelease;

    public void AssignBehaviour(GenericAction<CellIndex, Vector2> action)
    {
        bounds = GridField.Bounds;
        onRelease = action;
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
        AssignValue(ParentCellIndex, number + 1);
    }

    public void SetPosToParentCell()
    {
        transform.position = ParentCellIndex.GetPosition(GridField.Bounds.min, GridField.CellSize);
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
        objPosition.x = Mathf.Clamp(objPosition.x, bounds.xMin, bounds.xMax);
        objPosition.y = Mathf.Clamp(objPosition.y, bounds.yMax, bounds.yMin);
        transform.position = objPosition;
    }

    // public void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.magenta;
    //     Gizmos.DrawLine(bounds.max, new Vector2(bounds.max.x, bounds.min.y));
    //     Gizmos.DrawLine(bounds.min, new Vector2(bounds.min.x, bounds.max.y));
    //     Gizmos.DrawLine(bounds.max, new Vector2(bounds.min.x, bounds.max.y));
    //     Gizmos.DrawLine(bounds.min, new Vector2(bounds.max.x, bounds.min.y));
    // }
}
