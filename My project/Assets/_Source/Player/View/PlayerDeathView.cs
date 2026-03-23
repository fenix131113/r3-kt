using System;
using System.Linq;
using R3;
using UnityEngine;

namespace Player.View
{
    public class PlayerDeathView : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;

        private PlayerStats _stats;
        private IDisposable _healthDisposable;

        public void Construct(PlayerStats stats)
        {
            _stats = stats;
        }

        private void Start()
        {
            Bind();
        }

        private void OnDestroy()
        {
            Expose();
        }

        private void CheckForDeath(int[] values)
        {
            if (values.All(x => x != 0))
                return;

            Death();
        }

        private void Death()
        {
            gameOverPanel.SetActive(true);
        }

        private void Bind()
        {
            _healthDisposable = Observable.CombineLatest(_stats.HealthChanged, _stats.ManaChanged)
                .Subscribe(CheckForDeath);
        }

        private void Expose()
        {
            _healthDisposable?.Dispose();
        }
    }
}