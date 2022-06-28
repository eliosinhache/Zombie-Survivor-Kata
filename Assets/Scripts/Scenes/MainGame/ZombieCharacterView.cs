using Classes;
using Scenes.MainGame.MVP;
using ScriptableObject;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.MainGame
{
    public class ZombieCharacterView : CharacterView, IObserver, IPointerClickHandler
    {
        [SerializeField] private CharacterData _zombieSelectedData;
        private MainGameView _gameView;
        public void ReceiveUpdate()
        {
            if (_zombieSelectedData.characterName.Value == name)
            {
                DecreaseLife(_lifes.Count - _zombieSelectedData.lifes);
            }
        }

        protected override void CharacterDie()
        {
            _characterImage.color = Color.black;
        }

        public void SetGameView(MainGameView mainGameView)
        {
            _gameView = mainGameView;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _gameView.AZombieWasSelectedManually(name);
        }
    }
}