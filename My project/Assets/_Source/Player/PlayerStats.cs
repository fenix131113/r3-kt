using R3;
using UnityEngine;

namespace Player
{
    public class PlayerStats
    {
        private const int START_MEDKITS = 3;
        
        public const int MAX_HEALTH = 100;
        public const int MEDKIT_HEAL_AMOUNT = 25;
        
        private readonly ReactiveProperty<int> _health = new(MAX_HEALTH);
        private readonly ReactiveProperty<int> _medkitCount = new(START_MEDKITS);

        
        public bool IsAlive => _health.Value > 0;
        public Observable<int> HealthChanged => _health;
        public Observable<int> MedkitCountChanged => _medkitCount;

        public int GetHealth() => _health.Value;
        public int GetMedkitCount() => _medkitCount.Value;
        
        public void ChangeHealth(int count)
        {
            _health.Value = Mathf.Clamp(_health.Value + count, 0, MAX_HEALTH);
        }
        
        public void ChangeMedkits(int count)
        {
            _medkitCount.Value = Mathf.Clamp(_medkitCount.Value + count, 0, int.MaxValue);   
        }

        public void UseMedkit()
        {
            ChangeHealth(MEDKIT_HEAL_AMOUNT);
            ChangeMedkits(-1);
        }
    }
}