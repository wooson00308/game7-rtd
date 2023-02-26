using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class TTargetSelector : TAttackPart.Part
    {
        protected Monster _target;
        public Monster Target => _target;

        [Header("Tower Target Selector")]
        [SerializeField] private CircleCollider2D _collider;
        private float _range;

        private List<Monster> _targetList = new List<Monster>();

        public void SetRange(float range) => _range = range;

        protected override void Awake()
        {
            base.Awake();

            Activate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            _collider.radius = _range;

            SearchTarget();
        }

        void SearchTarget()
        {
            Monster minTarget = null;

            float minDistance = 999;

            foreach (var target in _targetList)
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
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            var monster = col.gameObject.GetComponentInParent<Monster>();
            if (monster != null)
            {
                _targetList.Add(monster);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D col)
        {
            _targetList.Remove(col.GetComponentInParent<Monster>());
        }
    }
}