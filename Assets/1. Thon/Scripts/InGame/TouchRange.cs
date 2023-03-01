using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class TouchRange : Unit
    {
        readonly static List<Monster> s_topPriorityMonsters = new ();
        public static List<Monster> TopPriorityMonsters => s_topPriorityMonsters;

        private void OnDisable()
        {
            s_topPriorityMonsters.Clear();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Monster monster = collision.GetComponentInParent<Monster>();
            if (monster)
            {
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