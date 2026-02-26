using DG.Tweening;
using Redline.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Squirkle
{
    public class BossHealthBar : Singleton<BossHealthBar>
    {
        public Image slider;
        public CanvasGroup canvasGroup;
        private EnemyInstance enemy;

        public void Show(EnemyInstance _enemy)
        {
            enemy = _enemy;
            slider.color = _enemy.enemyData.color;
            canvasGroup.DOFade(1f, 0.1f);
        }

        public void Hide()
        {
            enemy = null;
            canvasGroup.DOFade(0f, 0.1f);
        }

        void Update()
        {
            if (enemy == null) return;

            slider.fillAmount = enemy.currentHealth / enemy.enemyData.health;
        }
    }
}
