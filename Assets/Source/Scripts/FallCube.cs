using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;

public class FallCube : MonoBehaviour
{
    [SerializeField] private Poollable _poollable;
    [SerializeField] private Colourable _colourable;
    [SerializeField] private Vector2 _minMaxLifeTime = new Vector2(2, 5);

    private float _lifeTime;

    public void Initialize(Spawner spawner, RandomColorChanger randomColorChanger)
    {
        _colourable.Initialize(randomColorChanger);
        _poollable.Initialize(spawner);
    }

    private void OnEnable()
    {
        _colourable.ResetColor();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out FallCube fallCube))
        {
            return;
        }

        _colourable.SetRandomColor();
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

        _poollable.ReturnObjectToPool();
    }
}