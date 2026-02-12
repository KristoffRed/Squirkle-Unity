using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class ResistanceData
    {
        public float circleResist;
        public float squareResist;
        public float triangleResist;

        public DamageValue ApplyToDamage(DamageValue currentDamage)
        {
            currentDamage.circleDamage *= 1f - circleResist;
            currentDamage.squareDamage *= 1f - squareResist;
            currentDamage.triangleDamage *= 1f - triangleResist;
            return currentDamage;
        }
    }
}
