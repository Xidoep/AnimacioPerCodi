using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimacioPerCodi_Color : AnimacioPerCodi_Base
{
    enum Tipus { Color, Alfa }
    [SerializeField] T_Color[] transformacions;
    internal override Transformacions[] GetTransformacions => transformacions;

    new public void Play() => base.Play();

    [System.Serializable]
    public class T_Color : Transformacions
    {
        [SerializeField] Tipus tipus;
        [SerializeField] AnimationCurve corba = new AnimationCurve();

        [Space(10)]
        [SerializeField] bool iniciDinamic;
        [SerializeField] Color inici, final;
        [SerializeField] [Tooltip("Només per tipus MeshRenderer i SkinnedMeshRenderer")] string propietat;

        enum TipusIntern { nul, image, text, spriteRenderer, tmpText, meshRenderer, skinnedMeshRenderer }
        TipusIntern tipusIntern;

        Image image;
        Text text;
        SpriteRenderer spriteRenderer;
        TMP_Text tmpText;
        MeshRenderer meshRenderer;
        SkinnedMeshRenderer skinnedMeshRenderer;

        Color actual;

        public override void Transformar(Transform transform, float temps)
        {
            if(tipusIntern == TipusIntern.nul)
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
                default:
                    return Color.magenta;
            }
        }
    }
}
