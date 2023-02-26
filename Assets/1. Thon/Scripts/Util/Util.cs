using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [Serializable]
    public class PathStorage
    {
        public bool isLoop = true;
        public bool isLocalPath;
        [SerializeField] private List<Vector2> paths;
        public List<Vector2> Paths => paths;

        private int pathIndex = 0;

        public int CurIndex => pathIndex;

        public void Setup(List<Vector2> paths, bool isLocalPath, bool isLoop = true)
        {
            this.paths = paths;
            this.isLocalPath = isLocalPath;
            this.isLoop = isLoop;
        }

        public Vector2 GetPath()
        {
            if(pathIndex >= paths.Count)
            {
                if (!isLoop) return paths[pathIndex];

                pathIndex = 0;
            }

            return paths[pathIndex++];
        }
    }

    public class Util
    {
        public static string GetTimerFormat(float time)
        {
            int min = TimeSpan.FromSeconds(time).Minutes;
            int sec = TimeSpan.FromSeconds(time).Seconds;

            return string.Format("{0:D2}:{1:D2}", min, sec);
        }

        static readonly float minRate = 0.000001f;
        public static bool GetRateResult(float rate)
        {
            if (rate <= minRate) rate = minRate;

            return UnityEngine.Random.Range(0, 100) <= rate;
        }

        static float GetRandom(float[] inputDatas)
        {
            float total = 0;
 
            for (int i = 0; i < inputDatas.Length; i++)
            {
                total += inputDatas[i];
            }
 
            for (int i = 0; i < inputDatas.Length; i++)
            {
                if (total < inputDatas[i])
                {
                    return inputDatas[i];
                }
                else
                {
                    total -= inputDatas[i];
                }
            }
            return inputDatas[inputDatas.Length - 1];
        }
    }
}