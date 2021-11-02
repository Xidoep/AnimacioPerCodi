using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimacioPerCodi_Shader : AnimacioPerCodi_Base
{
    enum Tipus { Float, Vector }
    [SerializeField] T_Shader[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

    [System.Serializable]
    public class T_Shader : Transformacions
    {
        [SerializeField] Tipus tipus;
        [SerializeField] AnimationCurve corba = new AnimationCurve();

        [Space(10)]
        [SerializeField] bool iniciDinamic;
        [SerializeField] string propietat;
        [SerializeField] Vector4 inici, final;

        enum TipusIntern { nul, image, text, spriteRenderer, tmpText, meshRenderer, skinnedMeshRenderer }
        TipusIntern tipusIntern;

        Image image;
        Text text;
        SpriteRenderer spriteRenderer;
        TMP_Text tmpText;
        MeshRenderer meshRenderer;
        SkinnedMeshRenderer skinnedMeshRenderer;

        Vector4 actual;

        public override void Transformar(Transform transform, float temps)
        {
            if (tipusIntern == TipusIntern.nul)
            {
                image = transform.GetComponent<Image>();
                text = transform.GetComponent<Text>();
                spriteRenderer = transform.GetComponent<SpriteRenderer>();
                tmpText = transform.GetComponent<TMP_Text>();
                meshRenderer = transform.GetComponent<MeshRenderer>();

                if (image != null) tipusIntern = TipusIntern.image;
                if (text != null) tipusIntern = TipusIntern.text;
                if (spriteRenderer != null) tipusIntern = TipusIntern.spriteRenderer;
                if (tmpText != null) tipusIntern = TipusIntern.tmpText;
                if (meshRenderer != null) tipusIntern = TipusIntern.meshRenderer;
                if (skinnedMeshRenderer != null) tipusIntern = TipusIntern.skinnedMeshRenderer;
            }

            if (iniciDinamic && corba.Evaluate(temps) == 0) actual = GetIniciDinamic();

            actual = Vector4.LerpUnclamped(inici, final, corba.Evaluate(temps));

            switch (tipus)
            {
                case Tipus.Float:
                    GetMaterial.SetFloat(propietat, corba.Evaluate(temps));
                    break;
                case Tipus.Vector:
                    actual = Vector4.LerpUnclamped(inici, final, corba.Evaluate(temps));
                    GetMaterial.SetVector(propietat, actual);
                    break;
            }


            /*switch (tipusIntern)
            {
                case TipusIntern.image:
                    switch (tipus)
                    {
                        case Tipus.Float:
                            image.material.SetFloat(propietat, actual.x);
                            break;
                        case Tipus.PassarCorba:
                            image.material.SetFloat(propietat, corba.Evaluate(temps));
                            break;
                        default:
                            image.material.SetVector(propietat, actual);
                            break;
                    }
                    //image.material.SetVector(propietat, actual);
                    break;
                case TipusIntern.text:
                    switch (tipus)
                    {
                        case Tipus.Float:
                            text.material.SetFloat(propietat, actual.x);
                            break;
                        case Tipus.PassarCorba:
                            break;
                        default:
                            text.material.SetVector(propietat, actual);
                            break;
                    }
                    //text.material.SetVector(propietat, actual);
                    break;
                case TipusIntern.spriteRenderer:
                    switch (tipus)
                    {
                        case Tipus.Float:
                            spriteRenderer.material.SetFloat(propietat, actual.x);
                            break;
                        case Tipus.PassarCorba:
                            break;
                        default:
                            spriteRenderer.material.SetVector(propietat, actual);
                            break;
                    }
                    //spriteRenderer.material.SetVector(propietat, actual);
                    break;
                case TipusIntern.tmpText:
                    switch (tipus)
                    {
                        case Tipus.Float:
                            tmpText.material.SetFloat(propietat, actual.x);
                            break;
                        case Tipus.PassarCorba:
                            break;
                        default:
                            tmpText.material.SetVector(propietat, actual);
                            break;
                    }
                    //tmpText.material.SetVector(propietat, actual);
                    break;
                case TipusIntern.meshRenderer:
                    switch (tipus)
                    {
                        case Tipus.Float:
                            meshRenderer.material.SetFloat(propietat, actual.x);
                            break;
                        case Tipus.PassarCorba:
                            break;
                        default:
                            meshRenderer.material.SetVector(propietat, actual);
                            break;
                    }
                    //meshRenderer.material.SetVector(propietat, actual);
                    break;
                case TipusIntern.skinnedMeshRenderer:
                    switch (tipus)
                    {
                        case Tipus.Float:
                            skinnedMeshRenderer.material.SetFloat(propietat, actual.x);
                            break;
                        case Tipus.PassarCorba:
                            break;
                        default:
                            skinnedMeshRenderer.material.SetVector(propietat, actual);
                            break;
                    }
                    //skinnedMeshRenderer.material.SetVector(propietat, actual);
                    break;
            }*/
        }

        Material GetMaterial
        {
            get
            {
                switch (tipusIntern)
                {
                    case TipusIntern.image:
                        return image.material;
                    case TipusIntern.text:
                        return text.material;
                    case TipusIntern.spriteRenderer:
                        return spriteRenderer.material;
                    case TipusIntern.tmpText:
                        return tmpText.material;
                    case TipusIntern.meshRenderer:
                        return meshRenderer.material;
                    case TipusIntern.skinnedMeshRenderer:
                        return skinnedMeshRenderer.material;
                    default:
                        return null;
                }
            }
           
        }

        Vector4 GetIniciDinamic()
        {
            switch (tipusIntern)
            {
                case TipusIntern.image:
                    return image.material.GetVector(propietat);
                case TipusIntern.text:
                    return text.material.GetVector(propietat);
                case TipusIntern.spriteRenderer:
                    return spriteRenderer.material.GetVector(propietat);
                case TipusIntern.tmpText:
                    return tmpText.material.GetVector(propietat);
                case TipusIntern.meshRenderer:
                    return meshRenderer.material.GetVector(propietat);
                case TipusIntern.skinnedMeshRenderer:
                    return skinnedMeshRenderer.material.GetVector(propietat);
                default:
                    return new Vector4();
            }
        }
    }

}
