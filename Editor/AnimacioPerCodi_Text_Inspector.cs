using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Text))]
public class AnimacioPerCodi_Text_Inspector : Editor
{
    bool mostrar1 = false;
    bool mostrar2 = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AnimacioPerCodi_Text _target = (AnimacioPerCodi_Text)target;
        Animacio_Inspector_Addings.MostrarOpcions("ADD onEnter", _target, _target.OnEnter.Animacions, ref mostrar1);
        Animacio_Inspector_Addings.MostrarOpcions("ADD onExit", _target, _target.OnExit.Animacions, ref mostrar2);

        AssetDatabase.SaveAssetIfDirty(_target);
    }
}
