using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class TouchRange : Unit
    {
        static Transform s_rangeTr;

        public static Transform RangeTr => s_rangeTr;

        readonly static List<Monster> s_topPriorityMonsters = new ();
        public static List<Monster> TopPriorityMonsters => s_topPriorityMonsters;

        protected override void Awake()
        {
            base.Awake();

            s_rangeTr = transform;
        }

        private void OnDisable()
        {
            s_topPriorityMonsters.Clear();
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            Monster monster = collision.GetComponentInParent<Monster>();
            if (monster)
            {
                if (s_topPriorityMonsters.Contains(monster))
                    return;

                Log($"{monster.UnitId} ENTER");

                s_topPriorityMonsters.Add(monster);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Monster monster = collision.GetComponentInParent<Monster>();
            if (monster)
            {
                Log($"{monster.UnitId} EXIT");
                
                s_topPriorityMonsters.Remove(monster);
            }
        }

    }
}