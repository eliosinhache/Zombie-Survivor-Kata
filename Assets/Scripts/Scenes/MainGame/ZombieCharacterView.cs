using Classes;
using ScriptableObject;
using UniRx;
using UnityEngine;

namespace Scenes.MainGame
{
    public class ZombieCharacterView : CharacterView, IObserver
    {
        [SerializeField] private CharacterData _zombieSelectedData;
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
    }
}