using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public abstract class ViewCharacterController : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _Level;
        [SerializeField] protected GameObject _survivorHeartConteinter;
        [SerializeField] protected GameObject _heartImage;

        public void Setevel(string level)
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
