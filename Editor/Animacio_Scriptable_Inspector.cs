using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Animacio_Scriptable))]
public class Animacio_Scriptable_Inspector : Editor
{
    bool mostrar = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Animacio_Scriptable _target = (Animacio_Scriptable)target;

        Animacio_Inspector_Addings.MostrarOpcions(_target.Animacions, ref mostrar);

    }
}



public static class Animacio_Inspector_Addings
{
    
    public static void MostrarOpcions(List<Animacio> animacions, ref bool mostrar)
    {
        mostrar = EditorGUILayout.Foldout(mostrar, "ADD");

        if (!mostrar)
            return;

        EditorGUILayout.LabelField("TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) animacions.Add(new Animacio_Posicio());
        if (GUILayout.Button("Escala")) animacions.Add(new Animacio_Escala());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rotacio")) animacions.Add(new Animacio_Rotacio());
        if (GUILayout.Button("Al voltant Vector")) animacions.Add(new Animacio_RotacioVector());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("RECT TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) animacions.Add(new Animacio_RectPosicio());
        if (GUILayout.Button("Ancor")) animacions.Add(new Animacio_RectAncor());
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Escala")) animacions.Add(new Animacio_RectEscala());

        EditorGUILayout.LabelField("SHADER");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Float")) animacions.Add(new Animacio_ShaderFloat());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("AUDIO");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("So")) animacions.Add(new Animacio_So());
        if (GUILayout.Button("Audio")) animacions.Add(new Animacio_Audio());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("ESDEVENIMENT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Custom Extern")) animacions.Add(new Animacio_Esdeveniment());
        if (GUILayout.Button("Generic")) animacions.Add(new Animacio_EsdevenimentGeneric());
        if (GUILayout.Button("GPU")) animacions.Add(new Animacio_GPU());
        if (GUILayout.Button("Proxima animacio")) animacions.Add(new Animacio_ProximaAnimacio());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("COLOR");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Gradient Image")) animacions.Add(new Animacio_Gradient_Image());
        GUILayout.EndHorizontal();
    }

}


