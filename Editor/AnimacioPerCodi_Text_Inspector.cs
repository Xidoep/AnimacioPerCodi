using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Text))]
public class AnimacioPerCodi_Text_Inspector : Editor
{
    AnimacioPerCodi_Text _target;
    bool mostrar;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");

        if (!mostrar)
            return;

        _target = (AnimacioPerCodi_Text)target;
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", _target, ref _target.onEnter);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", _target, ref _target.onExit);
    }
    private void OnDisable()
    {
        if (!_target)
            return;

        AssetDatabase.SaveAssetIfDirty(_target);
    }
}
