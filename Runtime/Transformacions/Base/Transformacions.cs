using UnityEngine;

[System.Serializable]
public abstract class Transformacions : ScriptableObject
{
    public abstract void Transformar(Transform transform, float temps);
    public abstract Transformacions Create();
}
