using System;
using Classes;
using Scenes.MainGame.MVP;
using ScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.MainGame
{
    public class SurvivorCharacterView: CharacterView, IObserver, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _experience;
        [SerializeField] private CharacterData _selectedSurvivorData;
        private MainGameView _gameView;
        public void SetExperience(float checkExperience)
        {
            _experience.text = $"Exp: {checkExperience}";
        }

        public void SetNameView(string name)
        {
            _selectedSurvivorData.characterName.Value = name;
        }

        public void ReceiveUpdate()
        {
            if (_selectedSurvivorData.characterName.Value == name)
            {
                DecreaseLife(_lifes.Count - _selectedSurvivorData.lifes);
            }
        }

        protected override void CharacterDie()
        {
            _characterImage.color = Color.black;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _gameView.ASurvivorWasSelectedManually(name);
        }

        public void SetGameView(MainGameView mainGameView)
        {
            _gameView = mainGameView;
        }
    }
}