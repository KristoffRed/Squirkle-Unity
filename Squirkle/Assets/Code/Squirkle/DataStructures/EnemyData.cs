using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class EnemyData
    {
        public float health = 20f;
        public ResistanceData resistances = new ResistanceData();
        public float maxSpeed = 0.5f;
        public float size = 0.5f;

        [Header("Visuals")]
        public Color color;
        public Color color2;
        public Sprite sprite;
    }
}
