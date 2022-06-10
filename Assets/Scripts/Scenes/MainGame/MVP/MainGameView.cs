using System;
using TMPro;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainGame.MVP
{
    public class MainGameView : MonoBehaviour, IView
    {
        private IPresenter _presenter;
        [SerializeField] private TextMeshProUGUI _survivorLevel;
        [SerializeField] private TextMeshProUGUI _survivorExperience;
        [SerializeField] private GameObject _survivorHeartConteinter;
        [SerializeField] private GameObject _zombieHeartConteinter;
        [SerializeField] private TextMeshProUGUI _zombieLevel;
        [SerializeField] private TextMeshProUGUI _zombieExperience;
        [SerializeField] private GameObject _heartImage;

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

        public void SetZombieLevel(string returnLevel)
        {
            _zombieLevel.text = $"Level: {returnLevel}";
        }

        public void SetSurvivorLife(int lifes)
        {
            while (lifes > 0)
            {
                GameObject hear = Instantiate(_heartImage, Vector2.zero, Quaternion.identity, _survivorHeartConteinter.transform);
                lifes--;
            }
        }

        public void SetZombieLife(int lifes)
        {
            while (lifes > 0)
            {
                GameObject hear = Instantiate(_heartImage, Vector2.zero, Quaternion.identity, _zombieHeartConteinter.transform);
                lifes--;
            }
        }
    }
}
