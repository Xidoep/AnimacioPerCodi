using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(AnimacioPerCodi))]
public class AnimacioPerCodi_Inspector : Editor
{
    bool mostrar = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AnimacioPerCodi _target = (AnimacioPerCodi)target;
        Animacio_Inspector_Addings.MostrarOpcions("ADD", _target, _target.Animacions, ref mostrar);

    }
}




public static class Animacio_Inspector_Addings
{
    static void Add(List<Animacio> animacions, Object animacioPerCodi, Animacio animacio)
    {
        Undo.RecordObject(animacioPerCodi, "Add animacio");
        animacions.Add(animacio);
        EditorUtility.SetDirty(animacioPerCodi);
        PrefabUtility.RecordPrefabInstancePropertyModifications(animacioPerCodi);
    }
    public static void MostrarOpcions(string tabel, Object animacioPerCodi, List<Animacio> animacions, ref bool mostrar)
    {
        mostrar = EditorGUILayout.Foldout(mostrar, tabel);

        if (!mostrar)
            return;

        EditorGUILayout.LabelField("TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) Add(animacions, animacioPerCodi, new Animacio_Posicio());
        if (GUILayout.Button("Rotacio")) Add(animacions, animacioPerCodi, new Animacio_Rotacio());
        if (GUILayout.Button("Escala")) Add(animacions, animacioPerCodi, new Animacio_Escala());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Al voltant Vector")) Add(animacions, animacioPerCodi, new Animacio_RotacioVector());
        if (GUILayout.Button("So")) Add(animacions, animacioPerCodi, new Animacio_So());
        if (GUILayout.Button("Event Generic")) Add(animacions, animacioPerCodi, new Animacio_EsdevenimentGeneric());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("GPU")) Add(animacions, animacioPerCodi, new Animacio_GPU());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("RECT TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) Add(animacions, animacioPerCodi, new Animacio_RectPosicio());
        if (GUILayout.Button("Ancor")) Add(animacions, animacioPerCodi, new Animacio_RectAncor());
        if (GUILayout.Button("Escala")) animacions.Add(new Animacio_RectEscala());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("MESH RENDERER");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Shader Float")) animacions.Add(new Animacio_ShaderFloat());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("AUDIO SOURCE");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Play")) animacions.Add(new Animacio_Audio());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("IMAGE");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Gradient")) animacions.Add(new Animacio_Gradient_Image());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("TEXT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Gradient")) animacions.Add(new Animacio_Text_Gradient());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("SKINNED MESH RENDERER");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("BlendShape")) animacions.Add(new Animacio_BlendShape());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("CAP COMPONENT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Esdeveniment")) animacions.Add(new Animacio_Esdeveniment());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("ANTERIOR COMPONENT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Proxima animacio")) animacions.Add(new Animacio_ProximaAnimacio());
        GUILayout.EndHorizontal();
    }

}


