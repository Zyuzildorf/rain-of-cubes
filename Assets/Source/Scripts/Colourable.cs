using UnityEngine;

public class Colourable : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _defaultMaterial;

    private RandomColorChanger _randomColorChanger;

    public bool ColorChanged { get; private set; } = false;

    public void Initialize(RandomColorChanger randomColorChanger)
    {
        if (randomColorChanger == null)
        {
            Debug.LogError("Ошибка.");
            return;
        }

        _randomColorChanger = randomColorChanger;
    }

    public void SetRandomColor()
    {
        if (ColorChanged == false)
        {
            _randomColorChanger.SetRandomColor(_meshRenderer);
            ColorChanged = true;
        }
    }

    public void ResetColor()
    {
        _meshRenderer.material = _defaultMaterial;
        ColorChanged = false;
    }
}