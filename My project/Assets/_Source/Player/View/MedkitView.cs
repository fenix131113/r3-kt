using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.View
{
    public class MedkitView : MonoBehaviour
    {
        [SerializeField] private TMP_Text medkitCounterLabel;
        [SerializeField] private Button healButton;
        
        private PlayerStats _stats;
        private DisposableBag _disposables;
        
        public void Construct(PlayerStats stats) => _stats = stats;

        private void Start()
        {
            Bind();
            DrawMedkitCount(_stats.GetMedkitCount());
        }

        private void OnDestroy() => Expose();

        private void OnHealClicked()
        {
            if(_stats.GetHealth() == PlayerStats.MAX_HEALTH || _stats.GetMedkitCount() == 0)
                return;
            
            _stats.UseMedkit();
        }

        private void CheckForHealButtonState(int value)
        {
            healButton.gameObject.SetActive(value != PlayerStats.MAX_HEALTH);
        }

        private void DrawMedkitCount(int value)
        {
            medkitCounterLabel.text = "Medkits: " + value.ToString();
        }

        private void Bind()
        {
            healButton.onClick.AddListener(OnHealClicked);
            _disposables.Add(_stats.HealthChanged.DistinctUntilChanged().Subscribe(CheckForHealButtonState));
            _disposables.Add(_stats.MedkitCountChanged.DistinctUntilChanged().Subscribe(DrawMedkitCount));
        }

        private void Expose()
        {
            healButton.onClick.RemoveAllListeners();
            _disposables.Dispose();
        }
    }
}