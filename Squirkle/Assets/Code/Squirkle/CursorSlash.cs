using Inputs;
using UnityEngine;

namespace Squirkle
{
    public class CursorSlash : MonoBehaviour
    {
        public WeaponData weaponData;
        public float slashThreshold = 1f;
        public Transform cursorParent;
        private Vector2 targetPosition;
        private Vector2 previousPosition;

        void Update()
        {
            targetPosition = Camera.main.ScreenToWorldPoint(GlobalInput.GetScreenPosition());

            previousPosition = cursorParent.position;
            cursorParent.position = Vector2.Lerp(cursorParent.position, targetPosition, Time.deltaTime * 30f);
        }

        public Vector2 GetVelocity() => (Vector2)cursorParent.position - previousPosition;
        public float GetSpeed() => GetVelocity().magnitude;
        public bool IsSlashing() => GetSpeed() > slashThreshold;
    }
}
