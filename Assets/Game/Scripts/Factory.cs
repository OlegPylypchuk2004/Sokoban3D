using UnityEngine;

public abstract class Factory<T> where T : MonoBehaviour
{
    public T Spawn(T prefab)
    {
        return GameObject.Instantiate(prefab);
    }
}