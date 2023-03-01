using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class TargetLine : Unit
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private Monster _target;

        public void SetTargetOrNull(Monster target)
        {
            _target = target;
        }

        protected override void Awake()
        {
            base.Awake();
            _lineRenderer.widthMultiplier = .5f;
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (_target == null)
            {
                _lineRenderer.positionCount = 0;
                return;
            }

            _lineRenderer.positionCount = 2;

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _target.transform.position);
        }
    }
}