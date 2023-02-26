using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Catze.API.ReplayData;

namespace Catze.API
{
    public class Replay : API.Part
    {
        public abstract class State : UnitState
        {
            public Replay Upper => UpperUnit as Replay;

            protected ReplayData _replayData;

            public void SetReplayData(ReplayData data)
            {
                _replayData = data;
            }

            public virtual void StartState()
            {
                SetIsUseLapse();
            }

            public virtual void StopState()
            {
                SetIsUseLapse(false);
            }
        }

        public bool IsReplayMode => CurUnitState == ReplayState;

        [Header("State")]
        public ReplayState ReplayState;
        public RecordState RecordState;

        protected void Awake()
        {
            AddState(ReplayState);
            AddState(RecordState);
        }

        public void TryActivate(Action callback = null)
        {
            API.Inst.PlayFab.DataPart.LoadReplayData((result, data) =>
            {
                if (result)
                {
                    //  SetState = ReplayState
                    ReplayState.SetReplayData(data);
                    SetStateOrNull(ReplayState);
                }
                else
                {
                    // SetState = RecordState
                    SetStateOrNull(RecordState);
                }

                callback?.Invoke();
            });
        }

        public void StartRecord()
        {
            SetStateOrNull(RecordState);
        }

        public void StartReplay()
        {
            SetStateOrNull(ReplayState);
        }

        public void Stop()
        {
            SetStateOrNull(null);
        }
    }
}

