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
        Animacio_Inspector_Addings.MostrarOpcions("ADD",_target.Animacions, ref mostrar);

    }
}

[CustomEditor(typeof(AnimacioPerCodi_Boto))]
public class AnimacioPerCodi_Boto_Inspector : Editor
{
    bool mostrar = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AnimacioPerCodi_Boto _target = (AnimacioPerCodi_Boto)target;
        Animacio_Inspector_Addings.MostrarOpcions("ADD onClick",_target.OnClick.Animacions, ref mostrar);
        Animacio_Inspector_Addings.MostrarOpcions("ADD onEnter", _target.OnEnter.Animacions, ref mostrar);
        Animacio_Inspector_Addings.MostrarOpcions("ADD onExit", _target.OnExit.Animacions, ref mostrar);
        Animacio_Inspector_Addings.MostrarOpcions("ADD Loop", _target.Loop.Animacions, ref mostrar);
    }
}

public static class Animacio_Inspector_Addings
{

    public static void MostrarOpcions(string tabel, List<Animacio> animacions, ref bool mostrar)
    {
        mostrar = EditorGUILayout.Foldout(mostrar, tabel);

        if (!mostrar)
            return;

        EditorGUILayout.LabelField("TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) animacions.Add(new Animacio_Posicio());
        if (GUILayout.Button("Escala")) animacions.Add(new Animacio_Escala());
        if (GUILayout.Button("Rotacio")) animacions.Add(new Animacio_Rotacio());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Al voltant Vector")) animacions.Add(new Animacio_RotacioVector());
        if (GUILayout.Button("So")) animacions.Add(new Animacio_So());
        if (GUILayout.Button("Event Generic")) animacions.Add(new Animacio_EsdevenimentGeneric());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("GPU")) animacions.Add(new Animacio_GPU());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("RECT TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) animacions.Add(new Animacio_RectPosicio());
        if (GUILayout.Button("Ancor")) animacions.Add(new Animacio_RectAncor());
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
        if (GUILayout.Button("Gradient Image")) animacions.Add(new Animacio_Gradient_Image());
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


