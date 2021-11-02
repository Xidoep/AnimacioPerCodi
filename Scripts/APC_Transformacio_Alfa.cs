using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Transformacions/Alfa", fileName = "[Alfa]")]
public class APC_Transformacio_Alfa : APC_Transformacio
{
    public override APC_Transformacio CreateInstance(APC_Transformacio transformacio)
    {
        var _so = ScriptableObject.CreateInstance<APC_Transformacio_Color>();
        
        return _so;
    }

    public override void Transformacio(Transform transform, float temps)
    {

    }
}
