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
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(onEnabled, "On Enabled".ToNomAnimacioEditor(target, onEnabled));
        EditorGUILayout.EndVertical();
        onEnabled.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnabled", target, onEnabled);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(idle, "Idle".ToNomAnimacioEditor(target, idle));
        EditorGUILayout.EndVertical();
        idle.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("idle", target, idle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(onPointerEnter, "On Pointer Enter".ToNomAnimacioEditor(target, onPointerEnter));
        EditorGUILayout.EndVertical();
        onPointerEnter.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerEnter", target, onPointerEnter);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(apuntat, "Apuntat".ToNomAnimacioEditor(target, apuntat));
        EditorGUILayout.EndVertical();
        apuntat.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("apuntat", target, apuntat);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(onPointerDown, "On Pointer Down".ToNomAnimacioEditor(target, onPointerDown));
        EditorGUILayout.EndVertical();
        onPointerDown.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerDown", target, onPointerDown);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(onPointerUp, "On Pointer Up".ToNomAnimacioEditor(target, onPointerUp));
        EditorGUILayout.EndVertical();
        onPointerUp.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerUp", target, onPointerUp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(onPointerExit, "On Pointer Exit".ToNomAnimacioEditor(target, onPointerExit));
        EditorGUILayout.EndVertical();
        onPointerExit.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerExit", target, onPointerExit);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(onDestroyOrDisable, "On Destroy/Disable".ToNomAnimacioEditor(target, onDestroyOrDisable));
        EditorGUILayout.EndVertical();
        onDestroyOrDisable.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDestroyOrDisable", target, onDestroyOrDisable);
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();

    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
    
}
