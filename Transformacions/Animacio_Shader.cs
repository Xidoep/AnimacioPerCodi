using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Shader", fileName = "Shader")]
public class Animacio_Shader : AnimacioPerCodi_Base.Transformacions
{
    enum Tipus { Float, Vector }

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
