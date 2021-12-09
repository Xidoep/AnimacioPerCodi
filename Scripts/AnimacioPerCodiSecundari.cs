using UnityEngine;

public class AnimacioPerCodiSecundari : AnimacioPerCodi_Base
{
    [SerializeField] Transformacions[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;
}
