using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using XS_Utils;

public class Utilitzacions : EditorWindow
{
    public Object animaio = null;
    public string path;

    public List<UnityEngine.Object> elements = new List<UnityEngine.Object>();
    public List<string> nomsFuncions = new List<string>();

    public Vector2 scrollPosition;

    [MenuItem("XidoStudio/Utilitzacions animacions")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(Utilitzacions));
    }
    private void OnEnable()
    {
        Repaint();
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        animaio = EditorGUILayout.ObjectField(animaio, typeof(AnimacioPerCodi), false);
        if (GUILayout.Button("Buscar"))
        {
            Buscar();
            BuscarAssets();
        }
        EditorGUILayout.EndHorizontal();
        path = EditorGUILayout.TextField("path", path);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, true);

        for (int i = 0; i < elements.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(i.ToString(), elements[i], typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.ObjectField("", null, typeof(GameObject), true);
        }

        EditorGUILayout.EndScrollView();
    }
    void Buscar()
    {
        elements = new List<Object>();
        Debug.Log("-----------------------------------------------------------------");
        foreach (var item in FindObjectsOfType<Button>(true))
        {
            for (int e = 0; e < item.onClick.GetPersistentEventCount(); e++)
            {
                //Debug.Log($"({item.name})- {item.onClick.GetPersistentMethodName(e)}");
                if (animaio == item.onClick.GetPersistentTarget(e))
                    AfegirElement(item.gameObject);
            }
        }
        foreach (var item in FindObjectsOfType<EnEnable_Temps>(true))
        {
            for (int e = 0; e < item.Esdeveniment.GetPersistentEventCount(); e++)
            {
                Debug.Log($"({item.name})- {item.Esdeveniment.GetPersistentTarget(e)}");
                if (animaio == item.Esdeveniment.GetPersistentTarget(e))
                    AfegirElement(item.gameObject);
            }
        }
    }
    void BuscarAssets()
    {
        if (path == null)
            return;

        foreach (var Object in AssetDatabase.LoadAllAssetsAtPath(path))
        {
            Debug.Log(Object.name);
            //Debug.Log(AssetDatabase.LoadAssetAtPath(path, typeof(Object)));
        }
        /*foreach (var path in AssetDatabase.GetAllAssetPaths())
        {
            Debug.Log(AssetDatabase.LoadAssetAtPath(path, typeof(Object)));
        }*/
    }



    void AfegirElement(GameObject gameObject)
    {
        elements.Add(gameObject);
    }
}
