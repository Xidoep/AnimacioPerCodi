using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Toggle))]
public class AnimacioPerCodi_Toggle_Inspector : Editor
{
    SerializedProperty onEnter;
    SerializedProperty loop;
    SerializedProperty onClick;
    SerializedProperty onExit;

    bool mostrar;

    private void OnEnable()
    {
        onEnter = serializedObject.FindProperty("onEnter");
        loop = serializedObject.FindProperty("loop");
        onClick = serializedObject.FindProperty("onClick");
        onExit = serializedObject.FindProperty("onExit");
    }
    public override void OnInspectorGUI()
    {

        EditorGUILayout.PropertyField(onEnter, "On Enter".ToNomAnimacioEditor(target, onEnter));
        EditorGUILayout.PropertyField(loop, "Loop".ToNomAnimacioEditor(target, loop));
        EditorGUILayout.PropertyField(onClick, "On Click".ToNomAnimacioEditor(target, onClick));
        EditorGUILayout.PropertyField(onExit, "On Exit".ToNomAnimacioEditor(target, onExit));

        serializedObject.ApplyModifiedProperties();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        onEnter.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", target, onEnter);
        loop.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("loop", target, loop);
        onClick.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onClick", target, onClick);
        onExit.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", target, onExit);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
