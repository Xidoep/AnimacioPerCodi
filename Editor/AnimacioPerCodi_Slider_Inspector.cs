using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Slider))]
public class AnimacioPerCodi_Slider_Inspector : Editor
{
    SerializedProperty onEnter;
    SerializedProperty onDown;
    SerializedProperty onUp;
    SerializedProperty onExit;

    bool mostrar;

    private void OnEnable()
    {
        onEnter = serializedObject.FindProperty("onEnter");
        onDown = serializedObject.FindProperty("onDown");
        onUp = serializedObject.FindProperty("onUp");
        onExit = serializedObject.FindProperty("onExit");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(onEnter, "On Enter".ToNomAnimacioEditor(target, onEnter));
        EditorGUILayout.PropertyField(onDown, "On Down".ToNomAnimacioEditor(target, onDown));
        EditorGUILayout.PropertyField(onUp, "On Up".ToNomAnimacioEditor(target, onUp));
        EditorGUILayout.PropertyField(onExit, "On Exit".ToNomAnimacioEditor(target, onExit));

        serializedObject.ApplyModifiedProperties();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        onEnter.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", target, onEnter);
        onDown.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDown", target, onDown);
        onUp.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onUp", target, onUp);
        onExit.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", target, onExit);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
