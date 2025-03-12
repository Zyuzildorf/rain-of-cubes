using UnityEngine;

public class FallCubeSpawner : Spawner
{
    [SerializeField] private RandomColorChanger _randomColorChanger;
    
    public override void InitilizePoolable(Poollable poollable)
    {
        if (poollable.TryGetComponent(out FallCube fallCube))
        {
            fallCube.Initialize(this,_randomColorChanger);
        }
        else
        {
            Debug.LogError("Уээ. Ошибка.");
        }
    }
}