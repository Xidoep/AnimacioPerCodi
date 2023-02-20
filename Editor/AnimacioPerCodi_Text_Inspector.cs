using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimacioPerCodi_Text))]
public class AnimacioPerCodi_Text_Inspector : Editor
{
    SerializedProperty onEnter;
    SerializedProperty onExit;

    bool mostrar;

    private void OnEnable()
    {
        onEnter = serializedObject.FindProperty("onEnter");
        onExit = serializedObject.FindProperty("onExit");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(onEnter, "On Enter".ToNomAnimacioEditor(target, onEnter));
        EditorGUILayout.PropertyField(onExit, "On Exit".ToNomAnimacioEditor(target, onExit));

        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");
        if (!mostrar)
            return;

        onEnter.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnter", target, onEnter);
        onExit.objectReferenceValue = Animacio_Inspector_Addings.AddAnimacioPerCodi("onExit", target, onExit);
    }
    private void OnDisable() => AssetDatabase.SaveAssetIfDirty(target);
}
