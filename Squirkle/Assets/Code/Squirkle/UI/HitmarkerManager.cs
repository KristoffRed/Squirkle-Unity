using DG.Tweening;
using Redline.Helpers;
using Redline.Resources;
using TMPro;
using UnityEngine;

namespace Squirkle
{
    public class HitmarkerManager : GenericObjectPool<TextMeshProUGUI>
    {
        Camera camMain = null;

        public override TextMeshProUGUI Create()
        {
            TextMeshProUGUI inst = Instantiate(prefab);
            inst.gameObject.Disable();
            inst.transform.SetParent(transform);
            return inst;
        }

        public override TextMeshProUGUI Enable(TextMeshProUGUI obj)
        {
            obj.gameObject.Enable();
            return obj;
        }

        public override TextMeshProUGUI Disable(TextMeshProUGUI obj)
        {
            obj.gameObject.Disable();
            return obj;
        }

        public override void Remove(TextMeshProUGUI obj)
        {
            Destroy(obj.gameObject);
        }

        void Start()
        {
            camMain = Camera.main;
        }

        public void CreateHitmarker(Vector2 worldPos, float damage, Color color)
        {
            TextMeshProUGUI label = Get();
            label.text = damage.ToString("F1");
            label.color = color;
            label.transform.position = camMain.WorldToScreenPoint(worldPos) + GlobalHelper.RandomPositionOnCircleXY(50f);
            label.transform.localScale = Vector3.one;

            label.DOColor(new Color(color.r, color.g, color.b, 0f), 0.1f).SetDelay(0.2f).OnComplete(() => Release(label));
            label.transform.DOScale(1.1f, 0.3f);
        }
    }
}
