using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Catze
{
    public class InputManager : MUnit<InputManager>
    {

        Vector2 _pos;
        protected override void Awake()
        {
            base.Awake();
            Activate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
#if UNITY_ANDROID
            if (Input.touches.Length <= 0) return;

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    OnTouch();
                }
            }
#endif

            if (Input.GetMouseButtonDown(0))
            {
                OnTouchEnter();
            }

            else if (Input.GetMouseButton(0))
            {
                OnTouchStay();
            }
            else
            {
                OnTouchExit();
            }
        }

        bool ValidTouchTag()
        {
            _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Ray2D ray = new Ray2D(_pos, Vector2.zero);
            int layerMask = (-1) - (1 << LayerMask.NameToLayer("Tower Range"));
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
            
            if (!hit) return false;

            string tag = hit.transform.tag;
            bool isTouched = tag != "Ship";

            return isTouched;
        }
        
        void OnTouchEnter()
        {
            UIManager.Instance.SetActiveTouchRangeUI(ValidTouchTag());
        }

        void OnTouchStay()
        {
            UIManager.Instance.SetActiveTouchRangeUI(ValidTouchTag());
            UIManager.Instance.MoveTouchRangeUI(_pos);
        }

        void OnTouchExit()
        {
            UIManager.Instance.SetActiveTouchRangeUI(false);
        }
    }
}
