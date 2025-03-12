using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Poollable _template;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 15;

    private GameObject _startPoint;
    private ObjectPool<Poollable> _pool;

    public ObjectPool<Poollable> SpawnObjects()
    {
        _pool = new ObjectPool<Poollable>(
        createFunc: () => Instantiate(_template),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);

        StartInfiniteSpawning();
        
        return _pool;
    }
    
    private void StartInfiniteSpawning()
    {
        InvokeRepeating(nameof(GetObject),0.0f, _repeatRate);
    }
    
    private void GetObject()
    {
        _pool.Get();
    }
    
    private void ActionOnGet(Poollable obj)
    {
        obj.transform.position = GetRandomPosition();
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.gameObject.SetActive(true);
    }
    
    private Vector3 GetRandomPosition()
    {
        float xPosition;
        float zPosition;
        int minRandom = -8;
        int maxRandom = 8;

        xPosition = Random.Range(minRandom, maxRandom);
        zPosition = Random.Range(minRandom, maxRandom);

        return new Vector3(xPosition,_template.transform.position.y,zPosition);
    }
}