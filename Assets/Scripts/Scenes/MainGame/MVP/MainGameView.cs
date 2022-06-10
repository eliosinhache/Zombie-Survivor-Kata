using TMPro;
using UnityEngine;

namespace Scenes.MainGame.MVP
{
    public class MainGameView : MonoBehaviour, IView
    {
        private IPresenter _presenter;
        [SerializeField] private TextMeshProUGUI _survivorLevel;
        [SerializeField] private TextMeshProUGUI _survivorExperience;
        [SerializeField] private TextMeshProUGUI _zombieLevel;
        [SerializeField] private TextMeshProUGUI _zombieExperience;

        public void Start()
        {
            _presenter = new MainGamePresenter(this);
            _presenter.StartGame();
        }

        public void SetSurvivorLevel(string level)
        {
            _survivorLevel.text = $"level: {level}";
        }

        public void SetSurvivorExperience(float checkExperience)
        {
            _survivorExperience.text = $"Experience: {checkExperience}";
        }
    }
}
