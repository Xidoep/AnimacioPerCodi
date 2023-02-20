using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_GameObject))]
public class AnimacioPerCodi_GameObject_Inspector : Editor
{
    SerializedProperty onEnabled;
    SerializedProperty idle;
    SerializedProperty onPointerEnter;
    SerializedProperty loop;
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
        loop = serializedObject.FindProperty("loop");
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
        EditorGUILayout.PropertyField(loop, "Loop".ToNomAnimacioEditor(target, loop));
        EditorGUILayout.PropertyField(onPointerDown, "On Pointer Down".ToNomAnimacioEditor(target, onPointerDown));
        EditorGUILayout.PropertyField(onPointerExit, "On Pointer Exit".ToNomAnimacioEditor(target, onPointerExit));
        EditorGUILayout.PropertyField(onDestroyOrDisable, "On Destroy/Disable".ToNomAnimacioEditor(target, onDestroyOrDisable));

        serializedObject.ApplyModifiedProperties();

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        onEnabled.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnabled", target, onEnabled);
        idle.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("idle", target, idle);
        onPointerEnter.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerEnter", target, onPointerEnter);
        loop.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("loop", target, loop);
        onPointerDown.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerDown", target, onPointerDown);
        onPointerUp.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerUp", target, onPointerUp);
        onPointerExit.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerExit", target, onPointerExit);
        onDestroyOrDisable.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDestroyOrDisable", target, onDestroyOrDisable);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
