using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(AnimacioPerCodi_GameObject))]
public class AnimacioPerCodi_GameObject_Inspector : Editor
{
    SerializedProperty onEnabled;
    SerializedProperty idle;
    SerializedProperty onPointerEnter;
    SerializedProperty apuntat;
    SerializedProperty onPointerDown;
    SerializedProperty onPointerUp;
    SerializedProperty onPointerExit;
    SerializedProperty onDestroyOrDisable;

    bool mostrar;
    private void OnEnable()
    {
        onEnabled = serializedObject.FindProperty("onEnabled");
        idle = serializedObject.FindProperty("idle");
        onPointerEnter = serializedObject.FindProperty("onPointerEnter");
        apuntat = serializedObject.FindProperty("apuntat");
        onPointerDown = serializedObject.FindProperty("onPointerDown");
        onPointerUp = serializedObject.FindProperty("onPointerUp");
        onPointerExit = serializedObject.FindProperty("onPointerExit");
        onDestroyOrDisable = serializedObject.FindProperty("onDestroyOrDisable");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(onEnabled, "On Enabled".ToNomAnimacioEditor(target, onEnabled));
        EditorGUILayout.PropertyField(idle, "Idle".ToNomAnimacioEditor(target, idle));
        EditorGUILayout.PropertyField(onPointerEnter, "On Pointer Enter".ToNomAnimacioEditor(target, onPointerEnter));
        EditorGUILayout.PropertyField(apuntat, "Apuntat".ToNomAnimacioEditor(target, apuntat));
        EditorGUILayout.PropertyField(onPointerDown, "On Pointer Down".ToNomAnimacioEditor(target, onPointerDown));
        EditorGUILayout.PropertyField(onPointerUp, "On Pointer Up".ToNomAnimacioEditor(target, onPointerUp));
        EditorGUILayout.PropertyField(onPointerExit, "On Pointer Exit".ToNomAnimacioEditor(target, onPointerExit));
        EditorGUILayout.PropertyField(onDestroyOrDisable, "On Destroy/Disable".ToNomAnimacioEditor(target, onDestroyOrDisable));

        serializedObject.ApplyModifiedProperties();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        onEnabled.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnabled", target, onEnabled);
        idle.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("idle", target, idle);
        onPointerEnter.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerEnter", target, onPointerEnter);
        apuntat.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("apuntat", target, apuntat);
        onPointerDown.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerDown", target, onPointerDown);
        onPointerUp.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerUp", target, onPointerUp);
        onPointerExit.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerExit", target, onPointerExit);
        onDestroyOrDisable.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDestroyOrDisable", target, onDestroyOrDisable);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
