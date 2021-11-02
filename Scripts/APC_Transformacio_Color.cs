using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Transformacions/Color", fileName = "[Color]")]
public class APC_Transformacio_Color : APC_Transformacio
{
    public override APC_Transformacio CreateInstance(APC_Transformacio transformacio)
    {
        var _so = ScriptableObject.CreateInstance<APC_Transformacio_Color>();
        _so.corba = corba;
        _so.iniciDinamic = iniciDinamic;
        _so.inici = inici;
        _so.final = final;
        return _so;
    }

    public AnimationCurve corba = new AnimationCurve();
    public bool iniciDinamic;

    Image image;
    Text text;
    SpriteRenderer spriteRenderer;
    TMPro.TMP_Text tmpText;

    public Color inici;
    public Color final;

    public override void Transformacio(Transform transform, float temps)
    {
        if (image == null && text == null && spriteRenderer == null && tmpText == null)
        {
            image = transform.GetComponent<Image>();
            text = transform.GetComponent<Text>();
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
            tmpText = transform.GetComponent<TMPro.TMP_Text>();
        }
        if (iniciDinamic && corba.Evaluate(temps) == 0)
        {
            if (image != null) inici = image.color;
            if (text != null) inici = text.color;
            if (spriteRenderer != null) inici = spriteRenderer.color;
            if (tmpText != null) inici = tmpText.color;
        }
        Color _color = new Color(
            Mathf.LerpUnclamped(inici.r, final.r, corba.Evaluate(temps)),
            Mathf.LerpUnclamped(inici.g, final.g, corba.Evaluate(temps)),
            Mathf.LerpUnclamped(inici.b, final.b, corba.Evaluate(temps)),
            Mathf.LerpUnclamped(inici.a, final.a, corba.Evaluate(temps))
            );
        if (image != null) image.color = _color;
        if (text != null) text.color = _color;
        if (spriteRenderer != null) spriteRenderer.color = _color;
        if (tmpText != null) tmpText.color = _color;
    }
}
