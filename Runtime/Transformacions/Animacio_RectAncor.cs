using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Animacio_RectAncor : Animacio
{
    public Animacio_RectAncor() { }
    public Animacio_RectAncor(Vector3 iniciMin, Vector3 iniciMax, Vector3 finalMin, Vector3 finalMax, bool dinamic)
    {
        min = Corba.Linear;
        max = Corba.Linear;
        this.iniciMin = iniciMin;
        this.iniciMax = iniciMax;

        this.finalMin = finalMin;
        this.finalMax = finalMax;

        this.dinamic = dinamic;
    }

    [Title("Rect Ancor", horizontalLine: false)]
    [SerializeField, LabelWidth(40)] protected AnimationCurve min = new AnimationCurve();
    [SerializeField, LabelWidth(40)] protected AnimationCurve max = new AnimationCurve();

    [HorizontalGroup("Split")]
    [BoxGroup("Split/Left", GroupName = "Inici"), SerializeField, LabelText("Min"), LabelWidth(27), HideIf("@this.dinamic == true")] Vector2 iniciMin;
    [BoxGroup("Split/Left", GroupName = "Inici"), SerializeField, LabelText("Max"), LabelWidth(27), HideIf("@this.dinamic == true")] Vector2 iniciMax;
    [BoxGroup("Split/Right", GroupName = "Final"), SerializeField, LabelText("Min"), LabelWidth(27), HideLabel] Vector2 finalMin;
    [BoxGroup("Split/Right", GroupName = "Final"), SerializeField, LabelText("Max"), LabelWidth(27), HideLabel] Vector2 finalMax;


    [SerializeField, ToggleLeft] bool dinamic;

    //INTERN
    Vector2 iniciMinDin;
    Vector2 iniciMaxDin;

    public override void Transformar(Component objectiu, float frame)
    {
        if (!dinamic) Accio(iniciMin, iniciMax, objectiu, frame);
        else Dinamic(objectiu, frame);
    }

    void Dinamic(Component objectiu, float frame)
    {
        if (frame == 0) 
        {
            iniciMinDin = ((RectTransform)objectiu.transform).anchorMin;
            iniciMaxDin = ((RectTransform)objectiu.transform).anchorMax;
        }

        Accio(iniciMinDin, iniciMaxDin, objectiu, frame);
    }

    void Accio(Vector2 iniciMin, Vector2 iniciMax, Component objectiu, float frame)
    {
        ((RectTransform)objectiu.transform).anchorMin = Vector2.LerpUnclamped(iniciMin, finalMin, min.Evaluate(frame));
        ((RectTransform)objectiu.transform).anchorMax = Vector2.LerpUnclamped(iniciMax, finalMax, max.Evaluate(frame));
    }
}
