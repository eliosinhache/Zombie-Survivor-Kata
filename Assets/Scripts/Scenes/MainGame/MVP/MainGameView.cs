using System;
using System.Collections.Generic;
using Classes;
using ScriptableObject;
using TMPro;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainGame.MVP
{
    public class MainGameView : MonoBehaviour, IMainGameView
    {
        private IMainGamePresenter _mainGamePresenter;

        [SerializeField] private List<SurvivorCharacterView> _survivorControllers;

        [SerializeField] private List<ZombieCharacterView> _zombieControllers;

        [SerializeField] private GameObject _survivorPrefab;
        [SerializeField] private GameObject _zombiePrefab;

        [SerializeField] private GameObject _survivorsViewContainer;
        [SerializeField] private GameObject _zombieViewContainer;
        [SerializeField] private TextMeshProUGUI _selectedSurvivorName;
        [SerializeField] private TextMeshProUGUI _selectedsurvivorLevel;

        [SerializeField]
        private CharacterData _SurvivorData;

        public void Start()
        {
            _mainGamePresenter = new MainGamePresenter(this, _SurvivorData);
            _mainGamePresenter.StartGame();
        }


        public List<SurvivorCharacterView> ReturnSurvivorViews()
        {
            return _survivorControllers;
        }

        public List<ZombieCharacterView> ReturnZombieViews()
        {
            return _zombieControllers;
        }

        // public void AddSurvivor(string name)
        // {
        //     GameObject newCharacter = Instantiate(_survivorPrefab, _survivorsViewContainer.transform);
        //     newCharacter.name = name;
        //     _survivorControllers.Add(newCharacter.GetComponent<SurvivorCharacterView>());
        //     _mainGamePresenter.SetInfoSurvivor(newCharacter.GetComponent<SurvivorCharacterView>());
        // }
        public void AddSurvivor(string name)
        {
            _mainGamePresenter.CreateSurvivor(name);
        }

        public void AddZombie()
        {
            GameObject newCharacter = Instantiate(_zombiePrefab, _zombieViewContainer.transform);
            _zombieControllers.Add(newCharacter.GetComponent<ZombieCharacterView>());
            _mainGamePresenter.SetInfoZombie(newCharacter.GetComponent<ZombieCharacterView>());
        }

        public void FillSelectedSurvivor(string survivorName, string survivorLevel)
        {
            _selectedSurvivorName.text = $"{survivorName}";
            _selectedsurvivorLevel.text = $"{survivorLevel}";
        }

        public void AddSurvivorGUI(string name)
        {
            GameObject newCharacter = Instantiate(_survivorPrefab, _survivorsViewContainer.transform);
            newCharacter.name = name;
            _survivorControllers.Add(newCharacter.GetComponent<SurvivorCharacterView>());
            _mainGamePresenter.SetInfoSurvivor(newCharacter.GetComponent<SurvivorCharacterView>());
        }

        public void ASurvivorWasSelectedManually(string survivorName)
        {
            // ISurvivor local = survivor.GetComponent<ISurvivor>();
            _mainGamePresenter.ASurvivorWasSelected(survivorName);
            // _SurvivorData.characterName.Value = local.ReturnName();
            // _SurvivorData.characterLevel.Value = local.ReturnLevel().ToString();
        }
    }
}
