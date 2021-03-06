using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainGame
{
    public abstract class CharacterView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _Level;
        [SerializeField] protected GameObject _survivorHeartConteinter;
        [SerializeField] protected GameObject _heartImage;
        // [SerializeField] protected TextMeshProUGUI _name;
        [SerializeField] protected Image _characterImage;
        protected List<GameObject> _lifes = new List<GameObject>();

        public void SetLevel(string level)
        {
            _Level.text = $"level: {level}";
        }
        
        public void SetLife(int lifes)
        {
                while (lifes > 0)
                {
                    GameObject hear = Instantiate(_heartImage, _survivorHeartConteinter.transform);
                    _lifes.Add(hear);
                    lifes--;
                }
        }

        public void DecreaseLife(int amoung)
        {
            while (amoung > 0 && _lifes.Count > 0)
            {
                Destroy(_lifes.First().gameObject);
                _lifes.RemoveAt(0);
                amoung--;
            }

            if (_lifes.Count == 0)
            {
                CharacterDie();
            }
        }

        protected abstract void CharacterDie();
    }
}
