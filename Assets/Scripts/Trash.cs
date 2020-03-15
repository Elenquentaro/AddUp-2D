using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trash : MonoBehaviour
{
    public static bool IsSelected = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Summand>())
        {
            IsSelected = true;
            Debug.Log("Trash is selected = " + IsSelected);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Summand>())
        {
            IsSelected = false;
            Debug.Log("Trash is selected = " + IsSelected);
        }
    }
}
