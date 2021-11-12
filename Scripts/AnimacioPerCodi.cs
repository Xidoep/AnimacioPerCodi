using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimacioPerCodi : MonoBehaviour
{
    public Animacio[] animacions;

    [ContextMenu("Play")]


    public void Play()
    {
        if (!gameObject.activeSelf)
            return;

        StartCoroutine(animacions[0].Play(transform, Prova));
    }
    public void Play(int index)
    {
        for (int i = 0; i < animacions.Length; i++) animacions[i].Stop(false);

        if (!gameObject.activeSelf)
            return;

        StartCoroutine(animacions[index].Play(transform));
    }
    [ContextMenu("Stop")]
    void Stop()
    {
        // if (Play(0) == null)
        //     return;

        for (int i = 0; i < animacions.Length; i++) animacions[i].Stop(false);
    }

    [ContextMenu("StopFinal")]
    void StopAlFinal()
    {
        // if (actual == null)
        //    return;

        for (int i = 0; i < animacions.Length; i++) animacions[i].Stop(true);
    }

    void Prova()
    {
        Debug.Log("Event!");
    }


    [System.Serializable]
    public class Animacio
    {
        public enum Transformacio_Tipus
        {
            nul, posicio, rotacio, escala, color, degradat, sprite
        }
        public enum Transicio_Tipus
        {
            clamp, loop, pingpong, loopPingpong, invertit
        }



        public Transicio_Tipus transicio;
        public float temps = 1;
        //[Range(0, 1)] public float esdevenimentTemps = 1;
        //public Transformacio[] transformacions;
        public ModificacioTranformacio[] transformacions;
        public ModificacioColor[] colors;
        public ModificacioEvent[] esdeveniments;
        //public AnimacioPerCodi_Transformacio.Transformacions[] prova;
        //[Range(0, 1)] public float tempsEsdeveniment = 0.8f;
        //bool llançat = false;



        //UnityEvent esdeveniment;
        Transform transform;
        float time;
        float TempsDesdePlay
        {
            get
            {
                return (Time.unscaledTime - time) / temps;
            }
            set
            {
                time = value;
            }
        }
        bool looping;
        bool pingPong;
        bool invertit;

        bool finalitzarAlFinalAnimacio;
        bool finalitzat;


        /*public IEnumerator Play(Transform _transform)
        {
            this.transform = _transform;
            Play(transform);
            return ActualitzarCorrutina();
        }*/


        public IEnumerator Play(Transform _transform, System.Action esdeveniment = null)
        {
            this.transform = _transform;
            switch (transicio)
            {
                case Transicio_Tipus.clamp:
                    pingPong = false;
                    looping = false;
                    invertit = false;
                    break;
                case Transicio_Tipus.pingpong:
                    pingPong = true;
                    looping = false;
                    invertit = false;
                    break;
                case Transicio_Tipus.loop:
                    pingPong = false;
                    looping = true;
                    invertit = false;
                    break;
                case Transicio_Tipus.loopPingpong:
                    pingPong = true;
                    looping = true;
                    invertit = false;
                    break;
                case Transicio_Tipus.invertit:
                    pingPong = false;
                    looping = false;
                    invertit = true;
                    break;
            }
            time = Time.unscaledTime;

            return ActualitzarCorrutina();
        }



        public bool Actualitzar()
        {
            if (!pingPong)
            {
                if (!invertit)
                {
                    Clamp(TempsDesdePlay, transform);
                }
                else
                {
                    ClampInvertit(TempsDesdePlay, transform);
                }
            }
            else
            {
                PingPong(TempsDesdePlay, transform);
            }

            if (TempsDesdePlay >= 1)
            {
                if (finalitzarAlFinalAnimacio)
                {
                    finalitzarAlFinalAnimacio = false;
                    finalitzat = true;
                }
                else
                {
                    if (looping)
                    {
                        time = Time.unscaledTime;
                        /*for (int i = 0; i < scriptablesActuals.Length; i++)
                        {
                            scriptablesActuals[i].Reset();
                        }*/
                        // for (int i = 0; i < transformacions.Length; i++) transformacions[i].Reset();
                        // for (int i = 0; i < colors.Length; i++) colors[i].Reset();
                        // for (int i = 0; i < esdeveniments.Length; i++) esdeveniments[i].Reset();
                    }
                }
            }

            return TempsDesdePlay < 1;
        }
        public void Stop(bool esperarFinalAnimacio = false)
        {
            if (esperarFinalAnimacio)
            {
                finalitzarAlFinalAnimacio = true;
            }
            else
            {
                finalitzat = false;
            }
        }


        public IEnumerator ActualitzarCorrutina()
        {
            finalitzat = true;
            while (finalitzat)
            {
                finalitzat = Actualitzar();
                yield return null;
            }
            //Destroy(this);
            yield return null;
        }

        void Clamp(float _t, Transform transform)
        {
            //Debug.Log(transform);
            for (int i = 0; i < transformacions.Length; i++) transformacions[i].Transformar(transform, Mathf.Clamp01(_t));
            for (int i = 0; i < colors.Length; i++) colors[i].Transformar(transform, Mathf.Clamp01(_t));
            for (int i = 0; i < esdeveniments.Length; i++) esdeveniments[i].Transformar(transform, Mathf.Clamp01(_t));
            //for (int i = 0; i < prova.Length; i++) prova[i].Transformar(transform, Mathf.Clamp01(_t));
            //if(scriptablesActuals.Length > 0) for (int i = 0; i < scriptablesActuals.Length; i++) scriptablesActuals[i].Transformacio(transform, Mathf.Clamp01(_t));
        }
        void PingPong(float _t, Transform transform)
        {
            if (_t < 0.5f)
            {
                for (int i = 0; i < transformacions.Length; i++) transformacions[i].Transformar(transform, Mathf.Clamp01(_t) * 2);
                for (int i = 0; i < colors.Length; i++) colors[i].Transformar(transform, Mathf.Clamp01(_t) * 2);
                for (int i = 0; i < esdeveniments.Length; i++) esdeveniments[i].Transformar(transform, Mathf.Clamp01(_t) * 2);
                //if (scriptablesActuals.Length > 0) for (int i = 0; i < scriptablesActuals.Length; i++) scriptablesActuals[i].Transformacio(transform, Mathf.Clamp01(_t) * 2);
            }
            if (_t > 0.5f)
            {
                for (int i = 0; i < transformacions.Length; i++) transformacions[i].Transformar(transform, 2 - (Mathf.Clamp01(_t) * 2));
                for (int i = 0; i < colors.Length; i++) colors[i].Transformar(transform, 2 - (Mathf.Clamp01(_t) * 2));
                for (int i = 0; i < esdeveniments.Length; i++) esdeveniments[i].Transformar(transform, 2 - (Mathf.Clamp01(_t) * 2));
                //if (scriptablesActuals.Length > 0) for (int i = 0; i < scriptablesActuals.Length; i++) scriptablesActuals[i].Transformacio(transform, 2 - (Mathf.Clamp01(_t) * 2));
            }
        }
        void ClampInvertit(float _t, Transform transform)
        {
            for (int i = 0; i < transformacions.Length; i++) transformacions[i].Transformar(transform, 1 - Mathf.Clamp01(_t));
            for (int i = 0; i < colors.Length; i++) colors[i].Transformar(transform, 1 - Mathf.Clamp01(_t));
            for (int i = 0; i < esdeveniments.Length; i++) esdeveniments[i].Transformar(transform, 1 - Mathf.Clamp01(_t));
            //if (scriptablesActuals.Length > 0) for (int i = 0; i < scriptablesActuals.Length; i++) scriptablesActuals[i].Transformacio(transform, 1 - Mathf.Clamp01(_t));
        }

    }

    public abstract class Modificacio
    {
        public abstract void Transformar(Transform transform, float temps);
    }

    [System.Serializable]
    public class Transformacio
    {
        public enum Tipus
        {
            Moviment,
            Rotacio,
            Escalat,
            Color,
            Alfa
        }
        [SerializeField] Tipus tipus;
        [SerializeField] AnimationCurve corba = new AnimationCurve();
        [SerializeField] bool iniciDinamic;
        [SerializeField] Vector3 inici;
        [SerializeField] Vector3 final;

        UnityEngine.UI.Image image;
        UnityEngine.UI.Text text;
        SpriteRenderer spriteRenderer;
        TMPro.TMP_Text tmpText;
        Color color;

        public void Transformar(Transform transform, float temps)
        {
            switch (tipus)
            {
                case Tipus.Moviment:
                    if (iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        inici = transform.localPosition;
                    }
                    transform.localPosition = Vector3.LerpUnclamped(inici, final, corba.Evaluate(temps));
                    break;
                case Tipus.Rotacio:
                    if (iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        inici = transform.localEulerAngles;
                    }
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(temps)));
                    break;
                case Tipus.Escalat:
                    if (iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        inici = transform.localScale;
                    }
                    transform.localScale = Vector3.LerpUnclamped(inici, final, corba.Evaluate(temps));
                    break;
                case Tipus.Color:
                    GetAndSetColor(transform);
                    if (iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        if (image != null) inici = (Vector4)image.color;
                        if (text != null) inici = (Vector4)text.color;
                        if (spriteRenderer != null) inici = (Vector4)spriteRenderer.color;
                        if (tmpText != null) inici = (Vector4)tmpText.color;
                    }
                    color = new Color(
                        Mathf.LerpUnclamped(inici.x, final.x, corba.Evaluate(temps)),
                        Mathf.LerpUnclamped(inici.y, final.y, corba.Evaluate(temps)),
                        Mathf.LerpUnclamped(inici.z, final.z, corba.Evaluate(temps)),
                        color.a
                        );
                    SetColor();
                    break;
                case Tipus.Alfa:
                    GetAndSetColor(transform);
                    color = new Color(
                        color.r,
                        color.g,
                        color.b,
                        Mathf.LerpUnclamped(inici.x, final.x, corba.Evaluate(temps))
                        );
                    SetColor();
                    break;
            }
        }

        void GetAndSetColor(Transform transform)
        {
            if (image == null && text == null && spriteRenderer == null && tmpText == null)
            {
                image = transform.GetComponent<UnityEngine.UI.Image>();
                text = transform.GetComponent<UnityEngine.UI.Text>();
                spriteRenderer = transform.GetComponent<SpriteRenderer>();
                tmpText = transform.GetComponent<TMPro.TMP_Text>();

                if (image != null) color = image.color;
                if (text != null) color = text.color;
                if (spriteRenderer != null) color = spriteRenderer.color;
                if (tmpText != null) color = tmpText.color;
            }
        }
        void SetColor()
        {
            if (image != null) image.color = color;
            if (text != null) text.color = color;
            if (spriteRenderer != null) spriteRenderer.color = color;
            if (tmpText != null) tmpText.color = color;
        }
        public void Reset() { }
    }

    [System.Serializable]
    public class ModificacioTranformacio : Modificacio
    {
        public enum Tipus { Moviment, Rotacio, Escalat, RectPosition}
        [SerializeField] Tipus tipus;
        [SerializeField] AnimationCurve corba = new AnimationCurve();
        [Space(10)]
        [SerializeField] bool iniciDinamic;
        [SerializeField] Vector3 inici;
        [SerializeField] Vector3 final;

        RectTransform rectTransform;

        public override void Transformar(Transform transform, float temps)
        {
            switch (tipus)
            {
                case Tipus.Moviment:
                    if (iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        inici = transform.localPosition;
                    }
                    transform.localPosition = Vector3.LerpUnclamped(inici, final, corba.Evaluate(temps));
                    break;
                case Tipus.Rotacio:
                    if (iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        inici = transform.localEulerAngles;
                    }
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(inici, final, corba.Evaluate(temps)));
                    break;
                case Tipus.Escalat:
                    if (iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        inici = transform.localScale;
                    }
                    transform.localScale = Vector3.LerpUnclamped(inici, final, corba.Evaluate(temps));
                    break;
                case Tipus.RectPosition:
                    if(rectTransform == null)
                    {
                        rectTransform = transform.GetComponent<RectTransform>();
                    }

                    if(iniciDinamic && corba.Evaluate(temps) == 0)
                    {
                        inici = rectTransform.anchoredPosition;
                    }
                    rectTransform.anchoredPosition = Vector2.LerpUnclamped(inici, final, corba.Evaluate(temps));
                    break;
            }
        }
    }

    [System.Serializable]
    public class ModificacioColor : Modificacio
    {
        public enum Tipus { Color, Alfa, ShaderFloat, ShaderVector2, ShaderVector3, ShaderColor}
        [SerializeField] Tipus tipus;
        [SerializeField] AnimationCurve corba = new AnimationCurve();
        [Space(10)]
        [SerializeField] bool iniciDinamic;
        [SerializeField] Color inici;
        [SerializeField] Color final;
        [SerializeField] [Tooltip("Només per tipus Shader")] string propietat;

        enum TipusIntern {image, text, spriteRenderer, tmpText, meshRenderer }
        TipusIntern tipusIntern;

        UnityEngine.UI.Image image;
        UnityEngine.UI.Text text;
        SpriteRenderer spriteRenderer;
        TMPro.TMP_Text tmpText;
        MeshRenderer meshRenderer; 

        Color tmp;

        public override void Transformar(Transform transform, float temps)
        {

            if (image == null && text == null && spriteRenderer == null && tmpText == null && meshRenderer == null)
            {
                image = transform.GetComponent<UnityEngine.UI.Image>();
                text = transform.GetComponent<UnityEngine.UI.Text>();
                spriteRenderer = transform.GetComponent<SpriteRenderer>();
                tmpText = transform.GetComponent<TMPro.TMP_Text>();
                meshRenderer = transform.GetComponent<MeshRenderer>();

                if (image != null) tipusIntern = TipusIntern.image;
                if (text != null) tipusIntern = TipusIntern.text;
                if (spriteRenderer != null) tipusIntern = TipusIntern.spriteRenderer;
                if (tmpText != null) tipusIntern = TipusIntern.tmpText;
                if (meshRenderer != null) tipusIntern = TipusIntern.meshRenderer;
            }

            if (iniciDinamic && corba.Evaluate(temps) == 0)
            {
                switch (tipusIntern)
                {
                    case TipusIntern.image:
                        tmp = image.color;
                        break;
                    case TipusIntern.text:
                        tmp = text.color;
                        break;
                    case TipusIntern.spriteRenderer:
                        tmp = spriteRenderer.color;
                        break;
                    case TipusIntern.tmpText:
                        tmp = tmpText.color;
                        break;
                    case TipusIntern.meshRenderer:
                        tmp = meshRenderer.material.GetColor(propietat);
                        break;
                }
            }

            switch (tipus)
            {
                case Tipus.Color:
                    tmp = new Color(
                        Mathf.LerpUnclamped(inici.r, final.r, corba.Evaluate(temps)),
                        Mathf.LerpUnclamped(inici.g, final.g, corba.Evaluate(temps)),
                        Mathf.LerpUnclamped(inici.b, final.b, corba.Evaluate(temps)),
                        inici.a
                        );
                    break;
                case Tipus.Alfa:
                    tmp = new Color(
                        inici.r,
                        inici.g,
                        inici.b,
                        Mathf.LerpUnclamped(inici.a, final.a, corba.Evaluate(temps))
                        );
                    break;
                case Tipus.ShaderFloat:
                case Tipus.ShaderVector2:
                case Tipus.ShaderVector3:
                case Tipus.ShaderColor:
                    tmp = new Color(
                        Mathf.LerpUnclamped(inici.r, final.r, corba.Evaluate(temps)),
                        Mathf.LerpUnclamped(inici.g, final.g, corba.Evaluate(temps)),
                        Mathf.LerpUnclamped(inici.b, final.b, corba.Evaluate(temps)),
                        Mathf.LerpUnclamped(inici.a, final.a, corba.Evaluate(temps))
                    );
                    break;
            }

            switch (tipus)
            {
                case Tipus.Color:
                case Tipus.Alfa:
                    switch (tipusIntern)
                    {
                        case TipusIntern.image:
                            image.color = tmp;
                            break;
                        case TipusIntern.text:
                            text.color = tmp;
                            break;
                        case TipusIntern.spriteRenderer:
                            spriteRenderer.color = tmp;
                            break;
                        case TipusIntern.tmpText:
                            tmpText.color = tmp;
                            break;
                        case TipusIntern.meshRenderer:
                            meshRenderer.material.SetColor(propietat, tmp);
                            break;
                    }
                    break;
                case Tipus.ShaderFloat:
                    switch (tipusIntern)
                    {
                        case TipusIntern.image:
                            image.material.SetFloat(propietat, tmp.r);
                            break;
                        case TipusIntern.text:
                            text.material.SetFloat(propietat, tmp.r);
                            break;
                        case TipusIntern.spriteRenderer:
                            spriteRenderer.material.SetFloat(propietat, tmp.r);
                            break;
                        case TipusIntern.tmpText:
                            tmpText.material.SetFloat(propietat, tmp.r);
                            break;
                        case TipusIntern.meshRenderer:
                            meshRenderer.material.SetFloat(propietat, tmp.r);
                            break;
                    }
                    break;
                case Tipus.ShaderVector2:
                case Tipus.ShaderVector3:
                    switch (tipusIntern)
                    {
                        case TipusIntern.image:
                            image.material.SetVector(propietat, tmp);
                            break;
                        case TipusIntern.text:
                            text.material.SetVector(propietat, tmp);
                            break;
                        case TipusIntern.spriteRenderer:
                            spriteRenderer.material.SetVector(propietat, tmp);
                            break;
                        case TipusIntern.tmpText:
                            tmpText.material.SetVector(propietat, tmp);
                            break;
                        case TipusIntern.meshRenderer:
                            meshRenderer.material.SetVector(propietat, tmp);
                            break;
                    }
                    break;
                case Tipus.ShaderColor:
                    switch (tipusIntern)
                    {
                        case TipusIntern.image:
                            image.material.color = tmp;
                            break;
                        case TipusIntern.text:
                            text.material.color = tmp;
                            break;
                        case TipusIntern.spriteRenderer:
                            spriteRenderer.material.color = tmp;
                            break;
                        case TipusIntern.tmpText:
                            tmpText.material.color = tmp;
                            break;
                        case TipusIntern.meshRenderer:
                            meshRenderer.material.SetColor(propietat, tmp);
                            break;
                    }
                    break;
            }
            
        }

    }

    [System.Serializable]
    public class ModificacioEvent : Modificacio
    {
        [Range(0, 1)] [SerializeField] float play = 0.5f;
        //internal System.Action esdeveniment;
        [SerializeField] So so;
        [SerializeField] Sprite sprite;
        [SerializeField] UnityEvent esdeveniment;

        bool played = false;
        UnityEngine.UI.Image image;
        SpriteRenderer spriteRenderer;

        public override void Transformar(Transform transform, float temps)
        {
            if (temps > play && !played)
            {
                played = true;

                if(so != null) so.Play(transform);

                if(sprite != null)
                {
                    if (image == null && spriteRenderer == null)
                    {
                        image = transform.GetComponent<UnityEngine.UI.Image>();
                        spriteRenderer = transform.GetComponent<SpriteRenderer>();
                    }
                    if (image != null) image.sprite = sprite;
                    if (spriteRenderer != null) spriteRenderer.sprite = sprite;
                }

                esdeveniment?.Invoke();
                //esdeveniment?.Invoke();
            }
            if(played && temps >= 1) played = false;
        }
    }

    [System.Serializable]
    public class ModificacioBlendShapes : Modificacio
    {
        [SerializeField] int blendShapeIndex;
        [SerializeField] AnimationCurve corba = new AnimationCurve();
        SkinnedMeshRenderer skinnedMeshRenderer;

        public override void Transformar(Transform transform, float temps)
        {
            if(skinnedMeshRenderer == null)
            {
                skinnedMeshRenderer = transform.GetComponent<SkinnedMeshRenderer>();
            }

            skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, corba.Evaluate(temps));
        }
    }
}
