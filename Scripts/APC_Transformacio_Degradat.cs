using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Transformacions/Degradat", fileName = "[Degradat]")]
public class APC_Transformacio_Degradat : APC_Transformacio
{
    public override APC_Transformacio CreateInstance(APC_Transformacio transformacio)
    {
        var _so = ScriptableObject.CreateInstance<APC_Transformacio_Degradat>();
        _so.degradat = degradat;
        return _so;
    }

    Image image;
    Text text;
    SpriteRenderer spriteRenderer;

    public Gradient degradat;

    public override void Transformacio(Transform transform, float temps)
    {
        if (image == null && text == null && spriteRenderer == null)
        {
            image = transform.GetComponent<Image>();
            text = transform.GetComponent<Text>();
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }
        Color _degradat = degradat.Evaluate(temps);
        if (image != null) image.color = _degradat;
        if (text != null) text.color = _degradat;
        if (spriteRenderer != null) spriteRenderer.color = _degradat;
    }

}
