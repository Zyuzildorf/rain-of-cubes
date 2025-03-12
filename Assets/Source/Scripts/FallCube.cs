using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;

public class FallCube : MonoBehaviour
{
    [SerializeField] private Poollable _poollable;
    [SerializeField] private Colourable _colourable;
    [SerializeField] private Vector2 _minMaxLifeTime = new Vector2(2, 5);

    public Poollable Poollable => _poollable;
    public Colourable Colourable => _colourable;
    
    private float _lifeTime;
    
    public event Action ObjectEnteredTrigger;
    public event Action OnEndedLifeTime;

    private void OnTriggerEnter(Collider other)
    {
        ObjectEnteredTrigger?.Invoke();
        _colourable.PreferColorChange();
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
        
        _poollable.Disable();
    }
}
