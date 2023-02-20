using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Boto))]
public class AnimacioPerCodi_Boto_Inspector : Editor
{

    SerializedProperty onEnter;
    SerializedProperty onClick;
    SerializedProperty onExit;
    SerializedProperty loop;

    bool mostrar;

    private void OnEnable()
    {
        onEnter = serializedObject.FindProperty("onEnter");
        onClick = serializedObject.FindProperty("onClick");
        onExit = serializedObject.FindProperty("onExit");
        loop = serializedObject.FindProperty("loop");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(onEnter, "On Enter".ToNomAnimacioEditor(target, onEnter));
        EditorGUILayout.PropertyField(onClick, "On Click".ToNomAnimacioEditor(target, onClick));
        EditorGUILayout.PropertyField(onExit, "On Exit".ToNomAnimacioEditor(target, onExit));
        EditorGUILayout.PropertyField(loop, "Loop".ToNomAnimacioEditor(target, loop));

        serializedObject.ApplyModifiedProperties();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        Animacio_Inspector_Addings.AddAnimacioPerCodi("onClick", target, onClick);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", target, onEnter);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", target, onExit);
        Animacio_Inspector_Addings.AddAnimacioPerCodi("loop", target, loop);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
