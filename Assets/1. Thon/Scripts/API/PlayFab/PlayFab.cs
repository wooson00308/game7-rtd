using UnityEngine;

namespace Catze.API
{
    public class PlayFab : API.Part
    {
        public abstract class Part : UnitPart
        {
            public PlayFab Upper => UpperUnit as PlayFab;
        }

        [Header("Parts")]
        public AuthPart AuthPart;
        public DataPart DataPart;

        protected void Awake()
        {
            AddPart(AuthPart);
            AddPart(DataPart);
        }
    }
}