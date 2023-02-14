using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(AnimacioPerCodi))]
public class AnimacioPerCodi_Inspector : Editor
{
    bool mostrar = false;

    AnimacioPerCodi _target;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _target = (AnimacioPerCodi)target;
        Animacio_Inspector_Addings.MostrarOpcions("ADD", _target, _target.Animacions, ref mostrar);
    }
    private void OnDisable()
    {
        AssetDatabase.SaveAssetIfDirty(_target);
    }
}




public static class Animacio_Inspector_Addings
{
    public static void AddAnimacioPerCodi(string label, Object scriptableBase, ref AnimacioPerCodi animacio)
    {
        if (GUILayout.Button($"{(animacio == null ? "Add" : "Remove")} {label}"))
        {
            if(animacio == null)
            {
                var add = ScriptableObject.CreateInstance<AnimacioPerCodi>();
                add.name = label.Substring(0, 1).ToUpper() + label.Substring(1);
                AssetDatabase.AddObjectToAsset(add, scriptableBase);
                animacio = add;
            }
            else
            {
                if (AssetDatabase.IsSubAsset(animacio))
                {
                    if (EditorUtility.DisplayDialog("Borrar l'animacio????", "Abans de borrar l'animacio has de COMPROVAR que no la fagi servir ning� m�s...", "BORRAR!", "NO NO NO"))
                    {
                        AssetDatabase.RemoveObjectFromAsset(animacio);
                        animacio = null;
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Impossible!", "No es pot borrar aquesta animaci�, ja que no es filla de ning�.\nProva a reemplecarla directament si la vols canviar o treure", "OK");
                }
            }

            EditorUtility.SetDirty(scriptableBase);
            PrefabUtility.RecordPrefabInstancePropertyModifications(scriptableBase);
            AssetDatabase.SaveAssetIfDirty(scriptableBase);
        }
    }
    static void Add(Animacio[] animacions, Object animacioPerCodi, Animacio animacio)
    {
        Undo.RecordObject(animacioPerCodi, "Add animacio");

        List<Animacio> tmp = new List<Animacio>(animacions);
        tmp.Add(animacio);
        animacions = tmp.ToArray();

        EditorUtility.SetDirty(animacioPerCodi);
        PrefabUtility.RecordPrefabInstancePropertyModifications(animacioPerCodi);
        AssetDatabase.SaveAssetIfDirty(animacioPerCodi);
        
    }
    public static void MostrarOpcions(string tabel, Object animacioPerCodi, Animacio[] animacions, ref bool mostrar)
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
        if (GUILayout.Button("Escala")) Add(animacions, animacioPerCodi, new Animacio_RectEscala());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("MESH RENDERER");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Shader Float")) Add(animacions, animacioPerCodi, new Animacio_ShaderFloat());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("AUDIO SOURCE");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Play")) Add(animacions, animacioPerCodi, new Animacio_Audio());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("IMAGE");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Gradient")) Add(animacions, animacioPerCodi, new Animacio_Gradient_Image());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("TEXT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Gradient")) Add(animacions, animacioPerCodi, new Animacio_Text_Gradient());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("SKINNED MESH RENDERER");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("BlendShape")) Add(animacions, animacioPerCodi, new Animacio_BlendShape());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("CAP COMPONENT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Esdeveniment")) Add(animacions, animacioPerCodi, new Animacio_Esdeveniment());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("ANTERIOR COMPONENT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Proxima animacio")) Add(animacions, animacioPerCodi, new Animacio_ProximaAnimacio());
        GUILayout.EndHorizontal();
    }

}


