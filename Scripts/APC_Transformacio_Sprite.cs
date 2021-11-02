using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/Transformacions/Sprite", fileName = "[Sprite]")]
public class APC_Transformacio_Sprite : APC_Transformacio
{
    public override APC_Transformacio CreateInstance(APC_Transformacio transformacio)
    {
        var _so = ScriptableObject.CreateInstance<APC_Transformacio_Sprite>();
        _so.canvi = canvi;
        _so.iniciDinamic = iniciDinamic;
        _so.inici = inici;
        _so.final = final;
        return _so;
    }

    [Range(0, 1)] public float canvi;
    public bool iniciDinamic;

    Image image;
    SpriteRenderer spriteRenderer;

    public Sprite inici;
    public Sprite final;

    public override void Transformacio(Transform transform, float temps)
    {
        if (image == null && spriteRenderer == null)
        {
            image = transform.GetComponent<Image>();
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }
        if (iniciDinamic && temps == 0)
        {
            if (image != null) inici = image.sprite;
            if (spriteRenderer != null) inici = spriteRenderer.sprite;
        }

        if (image != null) image.sprite = temps > canvi ? final : inici;
        if (spriteRenderer != null) spriteRenderer.sprite = temps > canvi ? final : inici;
    }

}
