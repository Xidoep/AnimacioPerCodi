using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif



public static class Animacio_Inspector_Addings
{
#if UNITY_EDITOR
    public static GUIContent ToNomAnimacioEditor(this string nom, Object animacioPerCodi, SerializedProperty animacio) => new GUIContent(nom + (animacio.objectReferenceValue == null ? " - null - " : (AssetDatabase.GetAssetPath(animacioPerCodi) != AssetDatabase.GetAssetPath(animacio.objectReferenceValue) ? "   (EXTERIOR)" : "")));

    public static Object AddAnimacioPerCodi(string label, Object scriptableBase, SerializedProperty animacio)
    {
        if (GUILayout.Button($"{(animacio.objectReferenceValue == null ? "Add" : "Remove")}", new GUIStyle(GUI.skin.button) { fixedWidth = 60 }))
        {
            if (animacio.objectReferenceValue == null)
            {
                var add = ScriptableObject.CreateInstance<AnimacioPerCodi>();
                add.name = label.Substring(0, 1).ToUpper() + label.Substring(1);
                AssetDatabase.AddObjectToAsset(add, scriptableBase);
                animacio.objectReferenceValue = add;
            }
            else
            {
                if (AssetDatabase.IsSubAsset(animacio.objectReferenceValue))
                {
                    if (AssetDatabase.GetAssetPath(scriptableBase) == AssetDatabase.GetAssetPath(animacio.objectReferenceValue))
                    {
                        if (EditorUtility.DisplayDialog("Borrar l'animacio????", "Abans de borrar l'animacio has de COMPROVAR que no la fagi servir ning� m�s...", "BORRAR!", "NO NO NO"))
                        {
                            AssetDatabase.RemoveObjectFromAsset(animacio.objectReferenceValue);
                            animacio.objectReferenceValue = null;
                        }
                    }
                    else
                    {
                        Debug.Log(AssetDatabase.GetAssetPath(animacio.objectReferenceValue));
                        EditorUtility.DisplayDialog("Impossible!", "Aquesta animacio �s filla d'alg� altre i tu no tens permis per borrar-la, nom�s el seu pare", "OK");
                    }
                }
                else
                {
                    if(EditorUtility.DisplayDialog("Impossible!", "No es pot borrar aquesta animaci�, ja que no es filla de ning�.\nCom a molt es pot treure.", "TREURE!! NO LA VULL AQUI", "MANTENIR..."))
                    {
                        animacio.objectReferenceValue = null;
                    }
                }
            }

            EditorUtility.SetDirty(scriptableBase);
            PrefabUtility.RecordPrefabInstancePropertyModifications(scriptableBase);
            AssetDatabase.SaveAssetIfDirty(scriptableBase);
        }
        return animacio.objectReferenceValue;
    }
    public static AnimacioPerCodi AddAnimacioPerCodi(string label, Object scriptableBase, AnimacioPerCodi animacio)
    {
        if (animacio == null)
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
                if (AssetDatabase.GetAssetPath(scriptableBase) == AssetDatabase.GetAssetPath(animacio))
                {
                    if (EditorUtility.DisplayDialog("Borrar l'animacio????", "Abans de borrar l'animacio has de COMPROVAR que no la fagi servir ning� m�s...", "BORRAR!", "NO NO NO"))
                    {
                        AssetDatabase.RemoveObjectFromAsset(animacio);
                        animacio = null;
                    }
                }
                else
                {
                    Debug.Log(AssetDatabase.GetAssetPath(animacio));
                    EditorUtility.DisplayDialog("Impossible!", "Aquesta animacio �s filla d'alg� altre i tu no tens permis per borrar-la, nom�s el seu pare", "OK");
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
        return animacio;
    }
    static void Add(AnimacioPerCodi animacioPerCodi, Animacio animacio)
    {
        Undo.RecordObject(animacioPerCodi, "Add animacio");

        List<Animacio> tmp = new List<Animacio>(animacioPerCodi.Animacions);
        tmp.Add(animacio);
        animacioPerCodi.Animacions = tmp.ToArray();

        EditorUtility.SetDirty(animacioPerCodi);
        PrefabUtility.RecordPrefabInstancePropertyModifications(animacioPerCodi);
        AssetDatabase.SaveAssetIfDirty(animacioPerCodi);

        //return tmp.ToArray();
    }
    public static void MostrarOpcions(string tabel, AnimacioPerCodi animacioPerCodi, ref bool mostrar)
    {
        mostrar = EditorGUILayout.Foldout(mostrar, tabel);

        if (!mostrar)
            return;

        EditorGUILayout.LabelField("TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) Add(animacioPerCodi, new Animacio_Posicio());
        if (GUILayout.Button("Rotacio")) Add(animacioPerCodi, new Animacio_Rotacio());
        if (GUILayout.Button("Escala")) Add(animacioPerCodi, new Animacio_Escala());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Al voltant Vector")) Add(animacioPerCodi, new Animacio_RotacioVector());
        if (GUILayout.Button("So")) Add(animacioPerCodi, new Animacio_So());
        if (GUILayout.Button("Event Generic")) Add(animacioPerCodi, new Animacio_EsdevenimentGeneric());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("GPU")) Add(animacioPerCodi, new Animacio_GPU());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("RECT TRANSFORM");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Posicio")) Add(animacioPerCodi, new Animacio_RectPosicio());
        if (GUILayout.Button("Ancor")) Add(animacioPerCodi, new Animacio_RectAncor());
        if (GUILayout.Button("Escala")) Add(animacioPerCodi, new Animacio_RectEscala());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("MESH RENDERER");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Shader Float")) Add(animacioPerCodi, new Animacio_ShaderFloat());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("AUDIO SOURCE");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Play")) Add(animacioPerCodi, new Animacio_Audio());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("IMAGE");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Gradient")) Add(animacioPerCodi, new Animacio_Gradient_Image());
        if (GUILayout.Button("Shader TempsActual")) Add(animacioPerCodi, new Animacio_Shader_Image_TempsActual());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("TEXT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Gradient")) Add(animacioPerCodi, new Animacio_Text_Gradient());
        if (GUILayout.Button("Color")) Add(animacioPerCodi, new Animacio_Text_Color());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("SKINNED MESH RENDERER");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("BlendShape")) Add(animacioPerCodi, new Animacio_BlendShape());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("BEHAVIOUR");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Enable")) Add(animacioPerCodi, new Animacio_Enable());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("CAP COMPONENT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Esdeveniment")) Add(animacioPerCodi, new Animacio_Esdeveniment());
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("ANTERIOR COMPONENT");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Proxima animacio")) Add(animacioPerCodi, new Animacio_ProximaAnimacio());
        GUILayout.EndHorizontal();
    }
#endif
}


