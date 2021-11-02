using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APC_Transformacio : ScriptableObject
{
    public abstract APC_Transformacio CreateInstance(APC_Transformacio transformacio);

    public abstract void Transformacio(Transform transform, float temps);
    public virtual void Reset() { }
}
