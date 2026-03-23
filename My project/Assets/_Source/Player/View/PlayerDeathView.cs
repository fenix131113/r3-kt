using System;
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

        private void OnHealthChanged(int value)
        {
            if(value != 0)
                return;
            
            Death();
        }

        private void Death()
        {
            gameOverPanel.SetActive(true);
        }

        private void Bind()
        {
            _healthDisposable = _stats.HealthChanged.Subscribe(OnHealthChanged);
        }
        
        private void Expose()
        {
            _healthDisposable?.Dispose();
        }
    }
}