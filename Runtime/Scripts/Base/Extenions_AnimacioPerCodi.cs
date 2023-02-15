using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extenions_AnimacioPerCodi
{

    public static void SetupAndPlay(this Component component, Lector lector, Animacio[] animacions, float temps, Transicio transicio)
    {
        //Debug.LogError($"2.-Component = {component.name}", component);
        if (animacions == null || animacions.Length == 0)
            return;

        //component.GetLector(ref lector);
        lector.Setup(animacions, component, temps, transicio);
        lector.Play();
        //lector.Setup(animacions, temps, transicio);
        //lector.AddAnimacions(animacions, component, temps, transicio);

        //lector.Play();
    }
    static void GetLector(this Component component, ref Lector lector)
    {
        if(lector)
            lector = component.gameObject.GetComponent<Lector>();
        else lector = component.gameObject.AddComponent<Lector>();
    }
    /*static void AddAnimacions(this Lector lector, List<Animacio> animacions, Component component, float temps, Transicio transicio)
    {
        for (int i = 0; i < animacions.Count; i++)
        {
            if (i == 0) lector.Setup(animacions[i].Transformar, component, temps, transicio);
            else lector.Add(animacions[i].Transformar);
        }
    }*/
}
