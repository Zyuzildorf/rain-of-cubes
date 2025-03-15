using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class FallCubeSpawner : MonoBehaviour
{
    [SerializeField] private FallCube _prefab;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 15;

    private ObjectPool<FallCube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<FallCube>(
            createFunc: () => Instantiate(_prefab),
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

    private void GetObject()
    {
        _pool.Get();
    }

    private void ReleaseObject(FallCube fallCube)
    {
        _pool.Release(fallCube);
        fallCube.OnLifeTimeEnded -= ReleaseObject;
    }
    
    private void ActionOnGet(FallCube fallCube)
    {
        fallCube.transform.position = GetRandomPosition();
        fallCube.OnLifeTimeEnded += ReleaseObject;
        fallCube.gameObject.SetActive(true);
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition;
        float zPosition;
        int minRandom = -8;
        int maxRandom = 8;

        xPosition = Random.Range(minRandom, maxRandom);
        zPosition = Random.Range(minRandom, maxRandom);

        return new Vector3(xPosition, _prefab.transform.position.y, zPosition);
    }
}