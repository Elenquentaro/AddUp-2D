using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void PositionAction(Vector2 position);

public delegate void GenericAction<T0>(T0 arg0);

public delegate void GenericAction<T0, T1>(T0 arg0, T1 arg1);

public class GenericEvent<T> : UnityEvent<T> { }