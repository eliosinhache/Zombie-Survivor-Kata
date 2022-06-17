using System;
using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public abstract class CharacterView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _Level;
        [SerializeField] protected GameObject _survivorHeartConteinter;
        [SerializeField] protected GameObject _heartImage;

        public void SetLevel(string level)
        {
            _Level.text = $"level: {level}";
        }
        
        public void SetLife(int lifes)
        {
            while (lifes > 0)
            {
                GameObject hear = Instantiate(_heartImage, _survivorHeartConteinter.transform);
                lifes--;
            }
        }

        
    }
}
