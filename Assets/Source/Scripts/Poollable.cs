using System;
using UnityEngine;

public class Poollable : MonoBehaviour
{
    public event Action<Poollable> OnDisabled;

    public void Disable()
    {
        OnDisabled?.Invoke(this);
    }
}