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
        
        private PlayerStats _stats;

        private void Awake()
        {
            _stats = new PlayerStats();
            
            healthView.Construct(_stats);
            medkitView.Construct(_stats);
            playerDeathView.Construct(_stats);
        }
    }
}