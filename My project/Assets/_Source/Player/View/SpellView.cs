using UnityEngine.UI;
using UnityEngine;
using System;
using R3;

namespace Player.View
{
    public class SpellView : MonoBehaviour
    {
        [SerializeField] private int spellCost;
        [SerializeField] private Button spellButton;
        
        private PlayerStats _stats;
        private IDisposable _manaDisposable;
        
        public void Construct(PlayerStats stats) => _stats = stats;

        private void Start()
        {
            Bind();
            CheckSpellButton(_stats.GetMana());
        }

        private void OnDestroy() => Expose();

        private void OnSpellClicked()
        {
            if(_stats.GetMana() < spellCost)
                return;
            
            _stats.ChangeMana(-spellCost);
        }

        private void CheckSpellButton(int value)
        {
            spellButton.interactable =  _stats.GetMana() >= spellCost && _stats.IsAlive;
        }

        private void Bind()
        {
            spellButton.onClick.AddListener(OnSpellClicked);
            _manaDisposable = _stats.ManaChanged.Subscribe(CheckSpellButton);
        }

        private void Expose()
        {
            spellButton.onClick.RemoveAllListeners();
            _manaDisposable?.Dispose();
        }
    }
}