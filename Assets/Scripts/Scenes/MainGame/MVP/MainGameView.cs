using System;
using System.Collections.Generic;
using Classes;
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

        [SerializeField] private List<SurvivorView> _survivorControllers;

        [SerializeField] private List<ZombieView> _zombieControllers;

        [SerializeField] private GameObject _survivorPrefab;
        [SerializeField] private GameObject _zombiePrefab;

        [SerializeField] private GameObject _survivorsViewContainer;
        [SerializeField] private GameObject _zombieViewContainer;

        public void Start()
        {
            _mainGamePresenter = new MainGamePresenter(this);
            _mainGamePresenter.StartGame();
        }

        public List<SurvivorView> ReturnSurvivorViews()
        {
            return _survivorControllers;
        }

        public List<ZombieView> ReturnZombieViews()
        {
            return _zombieControllers;
        }

        public void AddSurvivor()
        {
            GameObject newCharacter = Instantiate(_survivorPrefab, _survivorsViewContainer.transform);
            _survivorControllers.Add(newCharacter.GetComponent<SurvivorView>());
            _mainGamePresenter.SetInfoSurvivor(newCharacter.GetComponent<SurvivorView>());
        }

        public void AddZombie()
        {
            GameObject newCharacter = Instantiate(_zombiePrefab, _zombieViewContainer.transform);
            _zombieControllers.Add(newCharacter.GetComponent<ZombieView>());
            _mainGamePresenter.SetInfoZombie(newCharacter.GetComponent<ZombieView>());
        }
    }
}
