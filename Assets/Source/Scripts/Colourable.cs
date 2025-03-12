
using System;
using UnityEngine;

public class Colourable : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    
    public bool ColorChanged { get; private set; } = false;
    public event Action<Colourable> OnPreferColorChange;

    public void PreferColorChange()
    {
        OnPreferColorChange?.Invoke(this);
    }
    
    public void Initialize(Material newMaterial)
    {
        if (newMaterial == null)
        {
            Debug.LogError("material is null");
            return;
        }

        _meshRenderer.material = newMaterial;
        ColorChanged = true;
    }
}
