using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Boto))]
public class AnimacioPerCodi_Boto_Inspector : Editor
{

    SerializedProperty onEnable;
    SerializedProperty onEnter;
    SerializedProperty onClick;
    SerializedProperty onExit;
    SerializedProperty loop;
    SerializedProperty onDestroyOrDisable;

    bool mostrar;

    private void OnEnable()
    {
        onEnable = serializedObject.FindProperty("onEnable");
        onEnter = serializedObject.FindProperty("onEnter");
        onClick = serializedObject.FindProperty("onClick");
        onExit = serializedObject.FindProperty("onExit");
        loop = serializedObject.FindProperty("loop");
        onDestroyOrDisable = serializedObject.FindProperty("onDestroyOrDisable");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(onEnable, "On Enable".ToNomAnimacioEditor(target, onEnable));
        EditorGUILayout.PropertyField(onEnter, "On Enter".ToNomAnimacioEditor(target, onEnter));
        EditorGUILayout.PropertyField(onClick, "On Click".ToNomAnimacioEditor(target, onClick));
        EditorGUILayout.PropertyField(onExit, "On Exit".ToNomAnimacioEditor(target, onExit));
        EditorGUILayout.PropertyField(loop, "Loop".ToNomAnimacioEditor(target, loop));
        EditorGUILayout.PropertyField(onDestroyOrDisable, "On Destroy or Disable".ToNomAnimacioEditor(target, onDestroyOrDisable));

        serializedObject.ApplyModifiedProperties();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnable", target, onEnable);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", target, onEnter);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onClick", target, onClick);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", target, onExit);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("loop", target, loop);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onDestroyOrDisable", target, onDestroyOrDisable);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
