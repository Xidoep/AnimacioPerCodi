using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimacioAutomatitzada : ScriptableObject
{
    public virtual void OnEnabled(Component component, ref Coroutine corrutineLoop) { }
    public virtual void Idle(Component component, ref Coroutine corrutineLoop) { }
    public virtual void OnPointerEnter(Component component) { }
    public virtual void OnPointerEnter(Component component, ref Coroutine corrutineLoop) { }
    public virtual void OnPointed(Component component, ref Coroutine corrutineLoop) { }
    public virtual void OnPointerDown(Component component, ref Coroutine corrutineLoop) { }
    public virtual void OnPointerUp(Component component, ref Coroutine corrutineLoop) { }
    public virtual void OnPointerExit(Component component, ref Coroutine corrutineLoop) { }
    public virtual void OnDestroyOrDisable(Component component, ref Coroutine corrutineLoop) { }



}
