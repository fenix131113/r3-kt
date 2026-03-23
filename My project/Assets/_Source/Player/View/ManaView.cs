using System;
using R3;
using TMPro;
using UnityEngine;

namespace Player.View
{
    public class ManaView : MonoBehaviour
    {
        [SerializeField] private TMP_Text manaLabel;

        private PlayerStats _stats;
        private IDisposable _manaDisposable;

        public void Construct(PlayerStats stats)
        {
            _stats = stats;
        }

        private void Start()
        {
            Bind();
            DrawMana();
        }

        private void OnDestroy()
        {
            Expose();
        }

        private void OnManaChanged(int value)
        {
            DrawMana();
        }
        
        private void DrawMana()
        {
            manaLabel.text = "Mana: " + _stats.GetMana().ToString();
        }

        private void Bind()
        {
            _manaDisposable = _stats.ManaChanged.DistinctUntilChanged().Subscribe(OnManaChanged);
        }

        private void Expose()
        {
            _manaDisposable?.Dispose();
        }
    }
}