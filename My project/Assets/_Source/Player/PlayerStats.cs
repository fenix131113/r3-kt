using R3;
using UnityEngine;

namespace Player
{
    public class PlayerStats
    {
        private const int START_MEDKITS = 3;

        public const int MAX_HEALTH = 100;
        public const int MAX_MANA = 100;
        public const int MEDKIT_HEAL_AMOUNT = 25;

        private readonly ReactiveProperty<int> _health = new(MAX_HEALTH);
        private readonly ReactiveProperty<int> _mana = new(MAX_MANA);
        private readonly ReactiveProperty<int> _coins = new(0);
        private readonly ReactiveProperty<int> _medkitCount = new(START_MEDKITS);


        public bool IsAlive => _health.Value > 0;
        public Observable<int> HealthChanged => _health;
        public Observable<int> MedkitCountChanged => _medkitCount;
        public Observable<int> ManaChanged => _mana;
        public Observable<int> CoinsChanged => _coins;

        public int GetHealth() => _health.Value;
        public int GetMana() => _mana.Value;
        public int GetCoins() => _coins.Value;
        public int GetMedkitCount() => _medkitCount.Value;

        public void ChangeHealth(int count)
        {
            _health.Value = Mathf.Clamp(_health.Value + count, 0, MAX_HEALTH);
        }

        public void ChangeMana(int count) => _mana.Value = Mathf.Clamp(_mana.Value + count, 0, MAX_MANA);

        public void ChangeCoins(int count) => _coins.Value = Mathf.Clamp(_coins.Value + count, 0, int.MaxValue);

        public void ChangeMedkits(int count) =>
            _medkitCount.Value = Mathf.Clamp(_medkitCount.Value + count, 0, int.MaxValue);

        public void UseMedkit()
        {
            ChangeHealth(MEDKIT_HEAL_AMOUNT);
            ChangeMedkits(-1);
        }
    }
}