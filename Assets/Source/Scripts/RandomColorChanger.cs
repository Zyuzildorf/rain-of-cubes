using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColorChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    public void SetRandomColor(MeshRenderer meshRenderer)
    {
        meshRenderer.material = _materials[Random.Range(0, _materials.Count)];
    }
}