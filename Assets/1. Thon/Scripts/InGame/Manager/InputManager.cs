using UnityEngine;


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
            if (Input.touches.Length > 0)
            {
                int index = 0;
                foreach(var touch in Input.touches)
                {
                    OnTouchStay(index++);
                }
            }
            else
            {
                OnTouchExit();
            }
#elif UNITY_EDITOR
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
#endif
        }

        bool ValidTouchTag(int index = 0)
        {
#if UNITY_ANDROID
            _pos = Camera.main.ScreenToWorldPoint(Input.touches[index].position);
#elif UNITY_EDITOR
            _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif

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

        void OnTouchStay(int index = 0)
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
