using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public class SurvivorView: ViewCharacterController
    {
        [SerializeField] private TextMeshProUGUI _experience;
        public void SetExperience(float checkExperience)
        {
            _experience.text = $"Experience: {checkExperience}";
        }
    }
}