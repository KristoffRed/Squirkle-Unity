using System;
using System.Collections.Generic;
using Redline.Helpers;
using Squircle.Runtime;
using UnityEngine;

namespace Squirkle
{
    public class AreaManager : Singleton<AreaManager>
    {
        public List<GameObject> areas;
        public int currentArea = -1;

        [Button("LoadNext")]
        public bool loadNext;

        [Button("LoadPrevious")]
        public bool loadPrevious;

        void Start()
        {
            LoadArea(0);
        }

        public void LoadArea(int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= areas.Count) return;

            if (currentArea != -1) areas[currentArea].Disable();
            areas[areaIndex].Enable();

            currentArea = areaIndex;
        }

        private void LoadNext() => LoadArea(currentArea + 1);
        private void LoadPrevious() => LoadArea(currentArea - 1);
    }
}
