using Player;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common
{
    public class SceneManager : MonoBehaviour
    {
        private WaveManager _wave;
        private PlayerObserver _player;

        [Inject]
        private void Construct(WaveManager wave, PlayerObserver player)
        {
            _player = player;
            _wave = wave;
        }
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private AttackButtonView _buttonView;

        private void OnEnable()
        {
            _player.Health.OnDied += _wave.StopSpawn;
            _player.Health.OnDied += ActivateRestart;
            _player.Health.OnDied += _buttonView.DisableAllButtons;
            _restartButton.onClick.AddListener(Restart); 
        }

        private static void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        private void ActivateRestart()
        {
            _restartButton.gameObject.SetActive(true); 
        }

        private void OnDisable()
        {
            _player.Health.OnDied -= _wave.StopSpawn;
            _player.Health.OnDied -= ActivateRestart;
            _restartButton.onClick.RemoveListener(Restart); 
        }
    }
}