using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Boto))]
public class AnimacioPerCodi_Boto_Inspector : Editor
{
    bool mostrar1 = false;
    bool mostrar2 = false;
    bool mostrar3 = false;
    bool mostrar4 = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AnimacioPerCodi_Boto _target = (AnimacioPerCodi_Boto)target;
        Animacio_Inspector_Addings.MostrarOpcions("ADD onClick", _target, _target.OnClick.Animacions, ref mostrar1);
        Animacio_Inspector_Addings.MostrarOpcions("ADD onEnter", _target, _target.OnEnter.Animacions, ref mostrar2);
        Animacio_Inspector_Addings.MostrarOpcions("ADD onExit", _target, _target.OnExit.Animacions, ref mostrar3);
        Animacio_Inspector_Addings.MostrarOpcions("ADD Loop", _target, _target.Loop.Animacions, ref mostrar4);

        AssetDatabase.SaveAssetIfDirty(_target);
    }
}
