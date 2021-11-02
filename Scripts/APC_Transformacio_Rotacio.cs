using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Transformacions/Rotacio", fileName = "[Rotacio]")]
public class APC_Transformacio_Rotacio : APC_Transformacio
{
    public override APC_Transformacio CreateInstance(APC_Transformacio transformacio)
    {
        var _so = ScriptableObject.CreateInstance<APC_Transformacio_Rotacio>();
        _so.corba = corba;
        _so.iniciDinamic = iniciDinamic;
        _so.inici = inici;
        _so.final = final;
        return _so;
    }

    public AnimationCurve corba = new AnimationCurve();
    public bool iniciDinamic;

    public Vector3 inici;
    public Vector3 final;

    public override void Transformacio(Transform transform, float temps)
    {
        if (iniciDinamic && corba.Evaluate(temps) == 0)
        {
            inici = transform.localEulerAngles;
        }
        transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(temps)));
    }
}
