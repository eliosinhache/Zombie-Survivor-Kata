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
        [SerializeField] private TextMeshProUGUI _selectedZombieName;
        [SerializeField] private TextMeshProUGUI _selectedZombieLevel;

        [SerializeField]
        private CharacterData _SurvivorData;

        [SerializeField] private CharacterData _zombieData;

        public void Start()
        {
            _mainGamePresenter = new MainGamePresenter(this, _SurvivorData, _zombieData);
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
        public void AddSurvivor(string name)
        {
            _mainGamePresenter.CreateSurvivor(name);
        }

        public void AddZombie(string zombieName)
        {
            _mainGamePresenter.CreateZombie();
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
            _mainGamePresenter.SubscribeNewSurvivorToData(newCharacter.GetComponent<SurvivorCharacterView>());
        }
        public void AddZombieGUI(string name)
        {
            GameObject newCharacter = Instantiate(_zombiePrefab, _zombieViewContainer.transform);
            newCharacter.name = name;
            _zombieControllers.Add(newCharacter.GetComponent<ZombieCharacterView>());
            _mainGamePresenter.SetInfoZombie(newCharacter.GetComponent<ZombieCharacterView>());
            _mainGamePresenter.SubscribeNewZombieToData(newCharacter.GetComponent<ZombieCharacterView>());
        }

        public void FillSelectedZombie(string zombieName, string zombieLevel)
        {
            _selectedZombieName.text = $"{zombieName}";
            _selectedZombieLevel.text = $"{zombieLevel}";
        }

        public void ReadHistory()
        {
            _mainGamePresenter.ReadHistory();
        }

        public void WriteLog(string log)
        {
            Debug.LogWarning(log);
        }

        public void ShowHistoryOnDebug(List<string> histoyry)
        {
            Debug.ClearDeveloperConsole();
            foreach (string log in histoyry)
            {
                Debug.Log(log);
            }
        }

        public void ASurvivorWasSelectedManually(string survivorName)
        {
            _mainGamePresenter.ASurvivorWasSelected(survivorName);
        }

        public void AZombieWasSelectedManually(string zombieName)
        {
            _mainGamePresenter.AZombieWasSelected(zombieName);
        }
        
        public void AttackZombie()
        {
            _mainGamePresenter.SurvivorAttackAZombie();
        }
        public void AttackSurvivor()
        {
            _mainGamePresenter.ZombieAttackASurvivor();
        }
    }
}
