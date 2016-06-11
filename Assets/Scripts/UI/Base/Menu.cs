﻿using UnityEngine;
using System.Collections;

namespace Sol
{
    public class Menu : MonoBehaviour
    {
        public delegate void MenuOpen();
        public static event MenuOpen OnMenuOpen;

        public delegate void MenuClose();
        public static event MenuClose OnMenuClose;

        public AudioClip clickButtonEffect;
        public AudioClip openEffect;
        public AudioClip closeEffect;

        public GameObject root;
        public bool stopsMovement = true;
        public bool useTransitionEffect = true;

        protected bool isActive = false;
        protected float menuFadeTime = 0.2f;
        protected SoundManager cachedSoundManager;


        public SoundManager CachedSoundManager
        {
            get { return (cachedSoundManager != null) ? cachedSoundManager : cachedSoundManager = GameManager.Get<SoundManager>();  }
        }


        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if(isActive != value)
                {
                    isActive = value;

                    if(isActive)
                        Open();
                    else
                        Close();
                }
            }
        }

        ///<summary>
        ///Open the menu
        ///</summary>
        public virtual void Open()
        {
            if (!IsActive)
            {
                if (openEffect != null) CachedSoundManager.Play(openEffect);

                CanvasGroup cg = root.GetComponent<CanvasGroup>();
                if (cg != null && useTransitionEffect)
                {
                    StopAllCoroutines();
                    StartCoroutine(FadeMenu(cg, 0f, 1f));
                }

                isActive = true;
                root.SetActive(true);
                if (stopsMovement && OnMenuClose != null) OnMenuOpen();
            }
        }

        ///<summary>
        ///Close the menu
        ///</summary>
        public virtual void Close()
        {
            if (IsActive)
            {
                if (closeEffect != null) CachedSoundManager.Play(closeEffect);

                CanvasGroup cg = root.GetComponent<CanvasGroup>();
                if (cg != null && useTransitionEffect)
                {
                    StopAllCoroutines();
                    StartCoroutine(FadeMenu(cg, 1f, 0f, true));
                }
                else
                {
                    isActive = false;
                    root.SetActive(false);
                    if (stopsMovement) OnMenuClose();
                }
            }
        }

        ///<summary>
        ///Process clicked button
        ///</summary>
        public virtual bool ClickButton()
        {
            if (clickButtonEffect != null)
            {
                CachedSoundManager.Play(clickButtonEffect);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Fade menu in and out
        /// </summary>
        /// <param name="cg"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        protected virtual IEnumerator FadeMenu(CanvasGroup cg, float from, float to, bool close = false)
        {
            cg.alpha = from;
            float elapsedTime = 0f;
            Vector3 travelTo = Vector3.zero;
            Vector3 travelFrom = Vector3.zero;

            RectTransform rt = cg.GetComponent<RectTransform>();

            if(close)
            {
                travelTo = new Vector3(rt.localPosition.x, rt.localPosition.y - 15, rt.localPosition.z);
                travelFrom = new Vector3(rt.localPosition.x, rt.localPosition.y, rt.localPosition.z);
            }
            else
            {
                travelFrom = new Vector3(rt.localPosition.x, rt.localPosition.y - 15, rt.localPosition.z);
                travelTo = new Vector3(rt.localPosition.x, rt.localPosition.y, rt.localPosition.z);
            }

            while(elapsedTime < menuFadeTime)
            {
                rt.localPosition = Vector3.Lerp(travelFrom, travelTo, elapsedTime / menuFadeTime);
                cg.alpha = Mathf.Lerp(from, to, elapsedTime / menuFadeTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cg.alpha = to;

            if(close)
            {
                rt.localPosition = Vector3.zero;
                isActive = false;
                root.SetActive(false);
                if (stopsMovement) OnMenuClose();
            }
        }
    }
}