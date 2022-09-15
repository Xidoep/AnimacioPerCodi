using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Material", fileName = "Material")]
public class Animacio_Material : Transformacions
{
    public override Transformacions Create()
    {
        Animacio_Material t = (Animacio_Material)ScriptableObject.CreateInstance<Animacio_Material>();
        t.corba = corba;
        t.materials = materials;
        t.tipusIntern = TipusIntern.nul;
        t.meshRenderer = null;
        t.skinnedMeshRenderer = null;
        t.inicials = new Material[0];
        t.transform = null;
        return t;
    }

    [SerializeField] AnimationCurve corba = new AnimationCurve();
    [SerializeField] Material[] materials;
    [SerializeField] bool canviar;

    enum TipusIntern { nul, meshRenderer, skinnedMeshRenderer }
    TipusIntern tipusIntern;

    MeshRenderer meshRenderer;
    SkinnedMeshRenderer skinnedMeshRenderer;

    Material[] inicials;
    Transform transform;



    public override void Transformar(Transform transform, float temps)
    {
        if (this.transform != transform)
        {
            this.transform = transform;
            meshRenderer = this.transform.GetComponent<MeshRenderer>();
            skinnedMeshRenderer = this.transform.GetComponent<SkinnedMeshRenderer>();
        }

        if (tipusIntern == TipusIntern.nul)
        {
            if (meshRenderer != null) 
            {
                tipusIntern = TipusIntern.meshRenderer;
                inicials = meshRenderer.materials;
            }
            if (skinnedMeshRenderer != null) 
            {
                tipusIntern = TipusIntern.skinnedMeshRenderer;
                inicials = skinnedMeshRenderer.materials;
            } 
        }

        switch (tipusIntern)
        {
            case TipusIntern.meshRenderer:
                for (int i = 0; i < inicials.Length; i++)
                {
                    meshRenderer.materials[i].Lerp(inicials[i], materials[i], temps);
                }
                break;
            case TipusIntern.skinnedMeshRenderer:
                for (int i = 0; i < inicials.Length; i++)
                {
                    skinnedMeshRenderer.materials[i].Lerp(inicials[i], materials[i], temps);
                }
                break;
        }

        if (!canviar)
            return;

        if(temps >= 1)
        {
            switch (tipusIntern)
            {
                case TipusIntern.meshRenderer:
                    meshRenderer.materials = materials;
                    break;
                case TipusIntern.skinnedMeshRenderer:
                    skinnedMeshRenderer.materials = materials;
                    break;
            }
        }
    }

}
