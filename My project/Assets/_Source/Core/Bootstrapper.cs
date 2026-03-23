using Player;
using Player.View;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private HealthView healthView;
        [SerializeField] private MedkitView medkitView;
        [SerializeField] private PlayerDeathView playerDeathView;
        [SerializeField] private SpellView spellView;
        [SerializeField] private CoinsView coinsView;
        [SerializeField] private ManaView manaView;
        
        private PlayerStats _stats;

        private void Awake()
        {
            _stats = new PlayerStats();
            
            healthView.Construct(_stats);
            medkitView.Construct(_stats);
            playerDeathView.Construct(_stats);
            spellView.Construct(_stats);
            coinsView.Construct(_stats);
            manaView.Construct(_stats);
        }
    }
}