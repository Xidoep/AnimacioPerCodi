using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Animacio))]
public class Animacio_Inspector : Editor
{
    SerializedProperty animacions;

    private void OnEnable()
    {
        animacions = serializedObject.FindProperty("animacions");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        
        serializedObject.Update();
        EditorGUILayout.PropertyField(animacions);
        serializedObject.ApplyModifiedProperties();

        //Animacio_Inspector_Addings.MostrarOpcions((animacio).Animacions);

    }

}
