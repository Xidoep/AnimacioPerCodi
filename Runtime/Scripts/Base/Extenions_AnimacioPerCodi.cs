using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extenions_AnimacioPerCodi
{

    public static void SetupAndPlay(this Component component, ref Lector lector, List<Animacio> animacions, float temps, Transicio transicio)
    {
        if (animacions.Count == 0)
            return;

        component.GetLector(ref lector);
        lector.AddAnimacions(animacions, component, temps, transicio);

        lector.Play();
    }
    static void GetLector(this Component component, ref Lector lector)
    {
        lector = component.gameObject.GetComponent<LectorComponent>();
        if (!lector) lector = component.gameObject.AddComponent<LectorComponent>();
    }
    static void AddAnimacions(this Lector lector, List<Animacio> animacions, Component component, float temps, Transicio transicio)
    {
        for (int i = 0; i < animacions.Count; i++)
        {
            if (i == 0) lector.Setup(animacions[i].Transformar, component, temps, transicio);
            else lector.Add(animacions[i].Transformar);
        }
    }
}
