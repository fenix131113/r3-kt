using System;
using R3;
using TMPro;
using UnityEngine;

namespace Player.View
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinsLabel;

        private PlayerStats _stats;
        private IDisposable _coinsDisposable;

        public void Construct(PlayerStats stats)
        {
            _stats = stats;
        }

        private void Start()
        {
            Bind();
            DrawCoins();
        }

        private void OnDestroy()
        {
            Expose();
        }

        private void OnCoinsChanged(int value)
        {
            DrawCoins();
        }
        
        private void DrawCoins()
        {
            coinsLabel.text = "Coins: " + _stats.GetCoins().ToString();
        }

        private void Bind()
        {
            _coinsDisposable = _stats.CoinsChanged.DistinctUntilChanged().Subscribe(OnCoinsChanged);
        }

        private void Expose()
        {
            _coinsDisposable?.Dispose();
        }
    }
}