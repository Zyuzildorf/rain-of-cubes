using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    private void OnEnable()
    {
        if (_colourable.ColorChanged == false)
        {
            _colourable.OnPreferColorChange += OnObjectEnteredTrigger;
        }
    }

    private void OnDisable()
    {
        _colourable.OnPreferColorChange -= OnObjectEnteredTrigger;
    }

    private void OnObjectEnteredTrigger(Colourable colourable)
    {
        Material newMaterial = _materials[Random.Range(0, _materials.Count)];
        colourable.Initialize(newMaterial);
    }
}