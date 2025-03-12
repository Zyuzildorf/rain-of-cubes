using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private Poollable _template;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 15;

    private GameObject _startPoint;
    private ObjectPool<Poollable> _pool;

    public abstract void InitilizePoolable(Poollable poollable);
    
    public void ReleasePoolable(Poollable poollable)
    {
        _pool.Release(poollable);
    }    

    private void Awake()
    {
        _pool = new ObjectPool<Poollable>(
            createFunc: () => CreatePoolable(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, _repeatRate);
    }

    private Poollable CreatePoolable()
    {
        Poollable poollable = Instantiate(_template);
        InitilizePoolable(poollable);

        return poollable;
    }
    
    private void GetObject()
    {
        _pool.Get();
    }

    private void ActionOnGet(Poollable poollable)
    {
        poollable.transform.position = GetRandomPosition();
        poollable.gameObject.SetActive(true);
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition;
        float zPosition;
        int minRandom = -8;
        int maxRandom = 8;

        xPosition = Random.Range(minRandom, maxRandom);
        zPosition = Random.Range(minRandom, maxRandom);

        return new Vector3(xPosition, _template.transform.position.y, zPosition);
    }
}