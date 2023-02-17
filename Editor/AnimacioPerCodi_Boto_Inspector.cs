using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Boto))]
public class AnimacioPerCodi_Boto_Inspector : Editor
{
    AnimacioPerCodi_Boto _target;
    bool mostrar;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");

        if (!mostrar)
            return;

        _target = (AnimacioPerCodi_Boto)target;
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onClick", _target, ref _target.onClick);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", _target, ref _target.onEnter);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", _target, ref _target.onExit);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("loop", _target, ref _target.loop);
    }
    private void OnDisable()
    {
        if (!_target)
            return;

        AssetDatabase.SaveAssetIfDirty(_target);
    }
}
