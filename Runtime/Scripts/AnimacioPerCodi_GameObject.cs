using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XS_Utils;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Xido Studio/AnimacioPerCodi/AnimacioGameObject", fileName = "AnimacioGameObject")]
public class AnimacioPerCodi_GameObject : ScriptableObject
{
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi onEnabled;
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi idle;
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi onPointerEnter;
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi apuntat;
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi onPointerDown;
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi onPointerUp;
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi onPointerExit;
    [SerializeField, SerializeScriptableObject] public AnimacioPerCodi onDestroyOrDisable;






    bool destroyingOrdisabling = false;


    public Coroutine OnEnabled(Component component) 
    {
        destroyingOrdisabling = false;

        return component.Animacio_LoopDespres(onEnabled, this.idle);
    }
    public Coroutine OnPointerEnter(Component component, Coroutine coroutine)
    {
        if (destroyingOrdisabling)
            return null;

        return component.StopAnterior_Animacio_LoopDespres(onPointerEnter, idle, coroutine, apuntat);
    }
    public Coroutine OnPointerDown(Component component, Coroutine coroutine)
    {
        if (destroyingOrdisabling)
            return null;

        if (!onPointerUp || !onPointerUp.TeAnimacions) //No hi ha onPointerUp
        {
            return component.StopAnterior_Animacio_LoopDespres(onPointerDown, apuntat, coroutine, apuntat);
        }
        else return component.StopAnterior_Animacio(onPointerDown, apuntat, coroutine);
    }
    public Coroutine OnPointerUp(Component component, Coroutine coroutine, bool destroy = false, bool disable = false)
    {
        if (destroyingOrdisabling)
            return null;

        if (!onPointerDown || !onPointerDown.TeAnimacions) //No s'ha fet el onPointerDown
        {
            if (destroy) return DestroyAmbAnimacio(component, coroutine);
            else if (disable) return DisableAmbAnimacio(component, coroutine);
            else return component.StopAnterior_Animacio_LoopDespres(onPointerUp, apuntat, coroutine, apuntat);
        }
        else
        {
            if (destroy) return DestroyAmbAnimacio(component, true);
            else if (disable) return DisableAmbAnimacio(component, true);
            else  return component.Animacio_LoopDespres(onPointerUp, apuntat);
        }
    }
    public Coroutine OnPointerExit(Component component, Coroutine coroutine)
    {
        if (destroyingOrdisabling)
            return null;

        return component.StopAnterior_Animacio_LoopDespres(onPointerExit, apuntat, coroutine, idle);
    }



    public void Destroy(Component component, ref Coroutine loop, bool performDestroy = true)
    {
        loop = CorrutineStop(loop);
        DestroyAmbAnimacio(component, performDestroy);
    }
    Coroutine DestroyAmbAnimacio(Component component, Coroutine loop, bool performDestroy = true)
    {
        DestroyAmbAnimacio(component, performDestroy);
        return CorrutineStop(loop);
    }
    Coroutine DestroyAmbAnimacio(Component component, bool performDestroy)
    {
        destroyingOrdisabling = true;
        if (onDestroyOrDisable) onDestroyOrDisable.Play(component);
        if (performDestroy) Destroy(component.gameObject, (onDestroyOrDisable && onDestroyOrDisable.TeAnimacions) ? onDestroyOrDisable.Temps : 0);
        return null;
    }



    public void Disable(Component component, ref Coroutine loop, bool performDisable = true)
    {
        loop = CorrutineStop(loop);
        DisableAmbAnimacio(component, performDisable);
    }
    Coroutine DisableAmbAnimacio(Component component, Coroutine loop, bool performDisable = true)
    {
        DisableAmbAnimacio(component, performDisable);
        return CorrutineStop(loop);
    }
    Coroutine DisableAmbAnimacio(Component component, bool performDisable)
    {
        destroyingOrdisabling = true;
        if (onDestroyOrDisable) onDestroyOrDisable.Play(component);

        if (performDisable)
        {
            //if (onDestroyOrDisable && onDestroyOrDisable.TeAnimacions) XS_Utils.XS_Coroutine.StartCoroutine_Ending(onDestroyOrDisable.Temps, Disable);
            if (onDestroyOrDisable && onDestroyOrDisable.TeAnimacions) XS_Utils.XS_Coroutine.StartCoroutine_Ending(onDestroyOrDisable.Temps, Disable, component);
            else Disable(component);
        }

        return null;

    }
    void Disable(Component component) => component.gameObject.SetActive(false);





    Coroutine LoopPlayDelayed(Component component, float temps)
    {
        Coroutine corrutineLoop = XS_Utils.XS_Coroutine.StartCoroutine_Ending(temps, LoopAfter);

        void LoopAfter() => apuntat.Play(component);
        return corrutineLoop;
    }
    Coroutine IdlePlayDelayed(Component component, float temps)
    {
        Coroutine corrutineLoop = XS_Utils.XS_Coroutine.StartCoroutine_Ending(temps, IdleAfter);

        void IdleAfter() => idle.Play(component);
        return corrutineLoop;
    }
    Coroutine CorrutineStop(Coroutine corrutineLoop)
    {
        if (corrutineLoop != null)
            XS_Utils.XS_Coroutine.StopCoroutine(corrutineLoop);

        return null;
    }






    
    /*
    
    
    [Button("Add")] void AddOnEnabled() => onEnabled = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnabled", this, onEnabled);
    [Button("Remove")] void RemoveOnEnabled() => onEnabled = Animacio_Inspector_Addings.AddAnimacioPerCodi("onEnabled", this, onEnabled);

    
    
    [SerializeField, HorizontalGroup("idle", width: 70), Button(Name = "Add"), ShowIf("@this.idle == null")] void Addidle() => idle = Animacio_Inspector_Addings.AddAnimacioPerCodi("idle", this, idle);
    [SerializeField, HorizontalGroup("idle", width: 70), Button(Name = "Remove"), ShowIf("@this.idle != null")] void Removeidle() => idle = Animacio_Inspector_Addings.AddAnimacioPerCodi("idle", this, idle);



    [SerializeField, HorizontalGroup("onPointerEnter", width: 70), Button(Name = "Add"), ShowIf("@this.onPointerEnter == null")] void AddonPointerEnter() => onPointerEnter = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerEnter", this, onPointerEnter);
    [SerializeField, HorizontalGroup("onPointerEnter", width: 70), Button(Name = "Remove"), ShowIf("@this.onPointerEnter != null")] void RemoveonPointerEnter() => onPointerEnter = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerEnter", this, onPointerEnter);



    [SerializeField, HorizontalGroup("apuntat", width: 70), Button(Name = "Add"), ShowIf("@this.apuntat == null")] void Addapuntat() => apuntat = Animacio_Inspector_Addings.AddAnimacioPerCodi("apuntat", this, apuntat);
    [SerializeField, HorizontalGroup("apuntat", width: 70), Button(Name = "Remove"), ShowIf("@this.apuntat != null")] void Removeapuntat() => apuntat = Animacio_Inspector_Addings.AddAnimacioPerCodi("apuntat", this, apuntat);



    [SerializeField, HorizontalGroup("onPointerDown", width: 70), Button(Name = "Add"), ShowIf("@this.onPointerDown == null")] void AddonPointerDown() => onPointerDown = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerDown", this, onPointerDown);
    [SerializeField, HorizontalGroup("onPointerDown", width: 70), Button(Name = "Remove"), ShowIf("@this.onPointerDown != null")] void RemoveonPointerDown() => onPointerDown = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerDown", this, onPointerDown);



    [SerializeField, HorizontalGroup("onPointerUp", width: 70), Button(Name = "Add"), ShowIf("@this.onPointerUp == null")] void AddonPointerUp() => onPointerUp = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerUp", this, onPointerUp);
    [SerializeField, HorizontalGroup("onPointerUp", width: 70), Button(Name = "Remove"), ShowIf("@this.onPointerUp != null")] void RemoveonPointerUp() => onPointerUp = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerUp", this, onPointerUp);



    [SerializeField, HorizontalGroup("onPointerExit", width: 70), Button(Name = "Add"), ShowIf("@this.onPointerExit == null")] void AddonPointerExit() => onPointerExit = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerExit", this, onPointerExit);
    [SerializeField, HorizontalGroup("onPointerExit", width: 70), Button(Name = "Remove"), ShowIf("@this.onPointerExit != null")] void RemoveonPointerExit() => onPointerExit = Animacio_Inspector_Addings.AddAnimacioPerCodi("onPointerExit", this, onPointerExit);



    [SerializeField, HorizontalGroup("onDestroyOrDisable", width: 70), Button(Name = "Add"), ShowIf("@this.onDestroyOrDisable == null")] void AddonDestroyOrDisable() => onDestroyOrDisable = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDestroyOrDisable", this, onDestroyOrDisable);
    [SerializeField, HorizontalGroup("onDestroyOrDisable", width: 70), Button(Name = "Remove"), ShowIf("@this.onDestroyOrDisable != null")] void RemoveonDestroyOrDisable() => onDestroyOrDisable = Animacio_Inspector_Addings.AddAnimacioPerCodi("onDestroyOrDisable", this, onDestroyOrDisable);

    */
}
