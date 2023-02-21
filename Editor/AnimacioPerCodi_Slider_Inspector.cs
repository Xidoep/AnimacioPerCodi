using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Slider))]
public class AnimacioPerCodi_Slider_Inspector : Editor
{
    SerializedProperty onEnter;
    SerializedProperty onDown;
    SerializedProperty loop;
    SerializedProperty modificar;
    SerializedProperty onUp;
    SerializedProperty onExit;

    bool mostrar;

    private void OnEnable()
    {
        onEnter = serializedObject.FindProperty("onEnter");
        onDown = serializedObject.FindProperty("onDown");
        loop = serializedObject.FindProperty("loop");
        modificar = serializedObject.FindProperty("modificar");
        onUp = serializedObject.FindProperty("onUp");
        onExit = serializedObject.FindProperty("onExit");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(onEnter, "On Enter".ToNomAnimacioEditor(target, onEnter));
        EditorGUILayout.PropertyField(onDown, "On Down".ToNomAnimacioEditor(target, onDown));
        EditorGUILayout.PropertyField(loop, "Loop".ToNomAnimacioEditor(target, loop));
        EditorGUILayout.PropertyField(modificar, "Modificar".ToNomAnimacioEditor(target, modificar));
        EditorGUILayout.PropertyField(onUp, "On Up".ToNomAnimacioEditor(target, onUp));
        EditorGUILayout.PropertyField(onExit, "On Exit".ToNomAnimacioEditor(target, onExit));

        serializedObject.ApplyModifiedProperties();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        onEnter.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", target, onEnter);
        onDown.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDown", target, onDown);
        loop.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("loop", target, loop);
        modificar.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("modificar", target, modificar);
        onUp.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onUp", target, onUp);
        onExit.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", target, onExit);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
