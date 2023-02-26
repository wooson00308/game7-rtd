using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    public class OfflineState : AuthPart.NetworkState
    {
        public override void Activate()
        {
            base.Activate();

            // TODO : logout logic
        }
    }
}