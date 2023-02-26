using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class PopupUI : Unit
    {
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}