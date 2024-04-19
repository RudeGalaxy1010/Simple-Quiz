using Source.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Menu
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IScenesService _scenesService;
        
        private void Start()
        {
            _scenesService = Infrastructure.Services.Container.Single<IScenesService>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
        
        private void OnButtonClick()
        {
            _scenesService.LoadGame();
        }
    }
}
