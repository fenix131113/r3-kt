using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthLabel;
        [SerializeField] private GameObject warning;
        [SerializeField] private Button dealDamageButton;
        [SerializeField] private int damagePerDeal;
        [SerializeField][Range(0f, 1f)] private float warningHealthEdge;

        private PlayerStats _stats;
        private IDisposable _healthDisposable;

        public void Construct(PlayerStats stats)
        {
            _stats = stats;
        }

        private void Start()
        {
            Bind();
            DrawHealth();
        }

        private void OnDestroy()
        {
            Expose();
        }

        private void OnHealthChanged(int value)
        {
            warning.SetActive(value / (float)PlayerStats.MAX_HEALTH < warningHealthEdge);
            DrawHealth();
        }

        private void OnDealDamageClicked()
        {
            _stats.ChangeHealth(-damagePerDeal);
        }

        private void DrawHealth()
        {
            healthLabel.text = "Health: " + _stats.GetHealth().ToString();
        }

        private void Bind()
        {
            _healthDisposable = _stats.HealthChanged.DistinctUntilChanged().Subscribe(OnHealthChanged);
            dealDamageButton.onClick.AddListener(OnDealDamageClicked);
        }

        private void Expose()
        {
            _healthDisposable?.Dispose();
            dealDamageButton.onClick.RemoveAllListeners();
        }
    }
}