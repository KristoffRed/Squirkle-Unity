using Inputs;
using UnityEngine;

namespace Squirkle
{
    public class CursorSlash : MonoBehaviour
    {
        public float slashThreshold = 1f;
        public float abilityCooldown = 10f;
        public Transform cursorParent;
        private Vector2 targetPosition;
        private Vector2 previousPosition;

        private bool isTryingToUseAbility;
        private float abilityUseTimer;
        private float abilityCooldownTimer;

        void OnEnable()
        {
            GlobalInput.startHolding += StartHolding;
            GlobalInput.stopHolding += StopAbility;
        }

        void OnDisable()
        {
            GlobalInput.startHolding -= StartHolding;
            GlobalInput.stopHolding -= StopAbility;
        }

        void Update()
        {
            targetPosition = Camera.main.ScreenToWorldPoint(GlobalInput.GetScreenPosition());

            previousPosition = cursorParent.position;
            cursorParent.position = Vector2.Lerp(cursorParent.position, targetPosition, Time.deltaTime * 30f);

            abilityCooldownTimer -= Time.deltaTime;

            if (isTryingToUseAbility)
            {
                abilityUseTimer += Time.deltaTime;

                if (abilityUseTimer >= 1f)
                {
                    // Activate ability
                    abilityCooldownTimer = abilityCooldown;
                    AbilityEvents.onAbilityActivated?.Invoke(transform.position);
                    StopAbility();
                    Debug.Log("Activated ability!");
                }

                if (GetSpeed() > slashThreshold) StopAbility();
            }
        }

        public Vector2 GetVelocity() => (Vector2)cursorParent.position - previousPosition;
        public float GetSpeed() => GetVelocity().magnitude;
        public bool IsSlashing() => GetSpeed() > slashThreshold;

        private void StartHolding()
        {
            if (abilityCooldownTimer > 0f) return;

            abilityUseTimer = 0f;
            isTryingToUseAbility = true;
        }

        private void StopAbility()
        {
            abilityUseTimer = 0f;
            isTryingToUseAbility = false;
        }
    }
}
