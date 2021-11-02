using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Transformacions/Esdeveniment", fileName = "[Esdeveniment]")]
public class APC_Transformacio_Esdeveniment : APC_Transformacio
{
    public override APC_Transformacio CreateInstance(APC_Transformacio transformacio)
    {
        var _so = ScriptableObject.CreateInstance<APC_Transformacio_Esdeveniment>();
        return _so;
    }

    public delegate void Esdeveniment();
    Esdeveniment esdeveniment = null;
    public void AfegirEsdeveniment(Esdeveniment _esdeveniment)
    {
        esdeveniment = null;
        esdeveniment = _esdeveniment;
    }

    //public UnityEvent esdeveniment;
    [Range(0, 1)] public float invocar = 0.8f;
    bool llançat = false;

    public override void Transformacio(Transform transform, float temps)
    {
        if(temps >= invocar && !llançat)
        {
            Debug.Log("esdevenir!");
            llançat = true;
            esdeveniment.Invoke();
        }
    }

    public override void Reset()
    {
        llançat = false;
    }

}
