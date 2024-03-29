using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public class AnimacioPerCodi_Prova : MonoBehaviour
{
    [SerializeField] Animacio_Posicio animacio;
    [SerializeField] AnimacioPerCodi scriptable;

    private void OnEnable()
    {
        //animacio = new Animacio_Posicio(Vector3.zero,Vector3.one).Play(transform,3,Transicio.clamp)
    }
    private void Update()
    {
        if (XS_Input.OnPress(Key.Q)) new Animacio_Posicio(Vector3.zero, Vector3.one).Play(transform, 3, Transicio.clamp, true);
        if (XS_Input.OnPress(Key.W)) scriptable.Play(transform);
        if (XS_Input.OnPress(Key.E)) animacio.Stop();
    }


}
