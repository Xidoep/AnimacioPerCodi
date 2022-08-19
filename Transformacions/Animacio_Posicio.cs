using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Posicio", fileName = "Posicio")]
public class Animacio_Posicio : Animacio
{
    [SerializeField] Vector3 inici;
    [SerializeField] Vector3 final;


    public void Animar(Transform transform, float frame)
    {
        transform.localPosition = Vector3.LerpUnclamped(inici, final, corba.Evaluate(frame));
    }
    public void Play(Transform transform)
    {
        if(this.transform != transform) 
            lector = transform.gameObject.AddComponent<Animacio_Lector_Posicio>().Setup(Animar, temps, transicio);

        lector.Play();
    }
}



public class Animacio_Lector_Posicio : Animacio_Lector
{
    System.Action<Transform, float> animar;
    public Animacio_Lector Setup(System.Action<Transform, float> animar, float temps, Animacio.Transicio transicio)
    {
        this.animar = animar;
        Setup(temps, transicio);
        return this;
    }

    protected override void Animar(float frame) => animar.Invoke(transform, frame);
}

