using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_GameObject))]
public class AnimacioPerCodi_GameObject_Inspector : Editor
{
    AnimacioPerCodi_GameObject _target;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _target = (AnimacioPerCodi_GameObject)target;
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnabled", _target, ref _target.onEnabled);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("idle", _target, ref _target.idle);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerEnter", _target, ref _target.onPointerEnter);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("loop", _target, ref _target.loop);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerDown", _target, ref _target.onPointerDown);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerUp", _target, ref _target.onPointerUp);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerExit", _target, ref _target.onPointerExit);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onDestroyOrDisable", _target, ref _target.onDestroyOrDisable);
    }
    private void OnDisable()
    {
        AssetDatabase.SaveAssetIfDirty(_target);
    }
}
