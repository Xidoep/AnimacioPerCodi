using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Animacio_Scriptable))]
public class Animacio_Scriptable_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Animacio_Scriptable _target = (Animacio_Scriptable)target;

        Animacio_Inspector_Addings.MostrarOpcions(_target.Animacions);

    }
}



public static class Animacio_Inspector_Addings
{
    public static void MostrarOpcions(List<Animacio> animacions)
    {
        EditorGUILayout.LabelField("Add");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) animacions.Add(new Animacio_Posicio());
        if (GUILayout.Button("Rotacio")) animacions.Add(new Animacio_Posicio());
        GUILayout.EndHorizontal();
    }

}


