using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catze
{
    public class TTargetSelector : TAttackPart.Part
    {
        protected Monster _target;
        public Monster Target => _target;

        [Header("Tower Target Selector")]
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private GameObject _model;
        
        private float _range;

        private List<Monster> _targets = new List<Monster>();

        public void SetRange(float range) => _range = range;
        public void SetRangeModel(bool value) => _model.SetActive(value);

        private Vector2 defaultModelScale;

        protected override void Awake()
        {
            base.Awake();

            defaultModelScale = _model.transform.localScale;
            SetRangeModel(false);

            Activate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            _collider.radius = _range / 2;
            
            if(_model.activeSelf)
            {
                _model.transform.localScale = defaultModelScale * _range;
            }

            SearchTarget();
        }

        private void OnDrawGizmos()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, GetComponent<CircleCollider2D>().radius);
        }

        void SearchTarget()
        {
            // 타겟 우선순위 검색
            if (TouchRange.TopPriorityMonsters.Count > 0)
            {
                var tpTargets = TouchRange.TopPriorityMonsters.ToList();

                var removeTargets = new List<Monster>();

                foreach (var tpTarget in tpTargets)
                {
                    if (tpTarget == null) continue;
                    if(!_targets.Contains(tpTarget))
                    {
                        removeTargets.Add(tpTarget);
                    }
                }

                foreach(var remove in removeTargets)
                {
                    tpTargets.Remove(remove);
                }
                
                if(SetMinDistanceTarget(tpTargets))
                {
                    return;
                }
            }
            
            SetMinDistanceTarget(_targets);
        }

        bool SetMinDistanceTarget(List<Monster> list)
        {
            if (list.Count == 0) return false;
            
            // 가장 가까운 타겟 검색
            Monster minTarget = null;

            float minDistance = 999;
            
            foreach (var target in list)
            {
                if (target == null) continue;

                float distance = Vector2.Distance(transform.position, target.transform.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    minTarget = target;
                }
            }

            if (minTarget != null)
            {
                _target = minTarget;
            }

            return minTarget != null;
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            var monster = col.gameObject.GetComponentInParent<Monster>();
            if (monster != null)
            {
                _targets.Add(monster);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D col)
        {
            var monster = col.gameObject.GetComponentInParent<Monster>();
            if (monster != null)
            {
                _targets.Remove(monster);
            }
        }
    }
}