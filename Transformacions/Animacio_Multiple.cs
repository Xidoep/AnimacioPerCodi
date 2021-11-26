using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "XS/AnimacioPerCodi/All", fileName = "All")]
public class Animacio_Multiple : AnimacioPerCodi_Base.Transformacions
{
    [SerializeField] AnimacioPerCodi_Base.Transformacions[] animacions;
    /*public Animacio_Transformacio[] transformacio;
    public Animacio_Color[] color;
    public Animacio_Shader[] shader;
    public Animacio_BlendShape[] blendShape;
    public Animacio_Esdeveniment[] esdeveniment;
    public Animacio_So[] so;*/

    public override void Transformar(Transform transform, float temps)
    {
        for (int i = 0; i < animacions.Length; i++) animacions[i].Transformar(transform, temps);


        /*for (int i = 0; i < transformacio.Length; i++) transformacio[i].Transformar(transform, temps);
        for (int i = 0; i < color.Length; i++) color[i].Transformar(transform, temps);
        for (int i = 0; i < shader.Length; i++) shader[i].Transformar(transform, temps);
        for (int i = 0; i < blendShape.Length; i++) blendShape[i].Transformar(transform, temps);
        for (int i = 0; i < esdeveniment.Length; i++) esdeveniment[i].Transformar(transform, temps);
        for (int i = 0; i < so.Length; i++) so[i].Transformar(transform, temps);*/
    }
}
