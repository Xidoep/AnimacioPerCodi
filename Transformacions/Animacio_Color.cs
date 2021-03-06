using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Color", fileName = "Color")]
public class Animacio_Color : AnimacioPerCodi_Base.Transformacions
{
    public override AnimacioPerCodi_Base.Transformacions Create()
    {
        Animacio_Color t = (Animacio_Color)ScriptableObject.CreateInstance<Animacio_Color>();
        t.tipus = tipus;
        t.corba = corba;
        t.iniciDinamic = iniciDinamic;
        t.inici = inici;
        t.final = final;
        t.propietat = propietat;
        t.tipusIntern = TipusIntern.nul;
        t.image = null;
        t.text = null;
        t.spriteRenderer = null;
        t.tmpText = null;
        t.meshRenderer = null;
        t.skinnedMeshRenderer = null;
        t.toggle = null;
        t.imageToggle = null;
        t.actual = iniciDinamic ? GetIniciDinamic() : inici;
        t.transform = null;
        return t;
    }

    enum Tipus { Color, Alfa }

    [SerializeField] Tipus tipus;
    [SerializeField] AnimationCurve corba = new AnimationCurve();

    [Space(10)]
    [SerializeField] bool iniciDinamic;
    [SerializeField] Color inici, final;
    [SerializeField] [Tooltip("Nom?s per tipus MeshRenderer i SkinnedMeshRenderer")] string propietat;

    enum TipusIntern { nul, image, text, spriteRenderer, tmpText, meshRenderer, skinnedMeshRenderer, toggle }
    TipusIntern tipusIntern;

    Image image;
    Text text;
    SpriteRenderer spriteRenderer;
    TMP_Text tmpText;
    MeshRenderer meshRenderer;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Toggle toggle;
    Image imageToggle;

    Color actual;
    Transform transform;

    private void OnEnable()
    {
        transform = null;
    }

    public override void Transformar(Transform transform, float temps)
    {
        if(this.transform != transform)
        {
            this.transform = transform;

            image = this.transform.GetComponent<Image>();
            text = this.transform.GetComponent<Text>();
            spriteRenderer = this.transform.GetComponent<SpriteRenderer>();
            tmpText = this.transform.GetComponent<TMP_Text>();
            meshRenderer = this.transform.GetComponent<MeshRenderer>();
            skinnedMeshRenderer = this.transform.GetComponent<SkinnedMeshRenderer>();
            toggle = this.transform.GetComponent<Toggle>();
        }

        if (tipusIntern == TipusIntern.nul)
        {
            if (image != null) tipusIntern = TipusIntern.image;
            if (text != null) tipusIntern = TipusIntern.text;
            if (spriteRenderer != null) tipusIntern = TipusIntern.spriteRenderer;
            if (tmpText != null) tipusIntern = TipusIntern.tmpText;
            if (meshRenderer != null) tipusIntern = TipusIntern.meshRenderer;
            if (skinnedMeshRenderer != null) tipusIntern = TipusIntern.skinnedMeshRenderer;
            if (toggle != null) 
            {
                tipusIntern = TipusIntern.toggle;
                imageToggle = this.transform.GetComponentInChildren<Image>();
            } 

        }

        if (iniciDinamic && corba.Evaluate(temps) == 0) actual = GetIniciDinamic();
        else actual = inici;

        switch (tipus)
        {
            case Tipus.Color:
                actual = new Color(
                    Mathf.LerpUnclamped(actual.r, final.r, corba.Evaluate(temps)),
                    Mathf.LerpUnclamped(actual.g, final.g, corba.Evaluate(temps)),
                    Mathf.LerpUnclamped(actual.b, final.b, corba.Evaluate(temps)),
                    inici.a
                    );
                break;
            case Tipus.Alfa:
                actual = new Color(
                    actual.r,
                    actual.g,
                    actual.b,
                    Mathf.LerpUnclamped(actual.a, final.a, corba.Evaluate(temps))
                    );
                break;
        }

        switch (tipusIntern)
        {
            case TipusIntern.image:
                if(image == null) image = this.transform.GetComponent<Image>();
                image.color = actual;
                break;
            case TipusIntern.text:
                text.color = actual;
                break;
            case TipusIntern.spriteRenderer:
                spriteRenderer.color = actual;
                break;
            case TipusIntern.tmpText:
                tmpText.color = actual;
                break;
            case TipusIntern.meshRenderer:
                meshRenderer.material.SetColor(propietat, actual);
                break;
            case TipusIntern.skinnedMeshRenderer:
                skinnedMeshRenderer.material.SetColor(propietat, actual);
                break;
            case TipusIntern.toggle:
                if (imageToggle == null) imageToggle = this.transform.GetComponentInChildren<Image>();
                imageToggle.color = actual;
                break;
        }
    }

    Color GetIniciDinamic()
    {
        switch (tipusIntern)
        {
            case TipusIntern.image:
                return image.color;
            case TipusIntern.text:
                return text.color;
            case TipusIntern.spriteRenderer:
                return spriteRenderer.color;
            case TipusIntern.tmpText:
                return tmpText.color;
            case TipusIntern.meshRenderer:
                return meshRenderer.material.GetColor(propietat);
            case TipusIntern.skinnedMeshRenderer:
                return skinnedMeshRenderer.material.GetColor(propietat);
            case TipusIntern.toggle:
                return imageToggle.color;
            default:
                return Color.magenta;
        }
    }

}
