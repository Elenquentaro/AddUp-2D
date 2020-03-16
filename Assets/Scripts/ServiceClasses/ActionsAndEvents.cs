using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void EmptyAction();

public delegate void GenericAction<T0, T1>(T0 arg0, T1 arg1);

public delegate bool Condition();

public class IntEvent : UnityEvent<int> { }

public class EmptyEvent : UnityEvent { }

public class BoolEvent : UnityEvent<bool> { }

public class GenericEvent<T> : UnityEvent<T> { }