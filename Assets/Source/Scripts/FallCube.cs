using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;

public class FallCube : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxLifeTime = new Vector2(2, 5);
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private RandomColorChanger _randomColorChanger;

    private float _lifeTime;
    private bool _colorChanged = false;

    public event Action<FallCube> OnLifeTimeEnded;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out FallCube fallCube))
        {
            return;
        }
        
        if (_colorChanged == false)
        {
            _randomColorChanger.SetRandomColor(_meshRenderer);
            _colorChanged = true;
        }
        
        StartLifeTimeDecreasing();
    }

    private void StartLifeTimeDecreasing()
    {
        GetRandomLifeTime();
        StartCoroutine(nameof(DecreaseLifeTime));
    }

    private void GetRandomLifeTime()
    {
        _lifeTime = Convert.ToInt32(Random.Range(_minMaxLifeTime.x, _minMaxLifeTime.y));
    }

    private IEnumerator DecreaseLifeTime()
    {
        while (_lifeTime >= 0)
        {
            _lifeTime -= Time.deltaTime;
            yield return null;
        }

        OnLifeTimeEnded?.Invoke(this);
    }
}