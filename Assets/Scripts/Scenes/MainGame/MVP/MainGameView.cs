﻿using System;
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

        [SerializeField] private List<SurvivorCharacterView> _survivorControllers;

        [SerializeField] private List<ZombieCharacterView> _zombieControllers;

        [SerializeField] private GameObject _survivorPrefab;
        [SerializeField] private GameObject _zombiePrefab;

        [SerializeField] private GameObject _survivorsViewContainer;
        [SerializeField] private GameObject _zombieViewContainer;

        public void Start()
        {
            _mainGamePresenter = new MainGamePresenter(this);
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

        public void AddSurvivor()
        {
            GameObject newCharacter = Instantiate(_survivorPrefab, _survivorsViewContainer.transform);
            _survivorControllers.Add(newCharacter.GetComponent<SurvivorCharacterView>());
            _mainGamePresenter.SetInfoSurvivor(newCharacter.GetComponent<SurvivorCharacterView>());
        }

        public void AddZombie()
        {
            GameObject newCharacter = Instantiate(_zombiePrefab, _zombieViewContainer.transform);
            _zombieControllers.Add(newCharacter.GetComponent<ZombieCharacterView>());
            _mainGamePresenter.SetInfoZombie(newCharacter.GetComponent<ZombieCharacterView>());
        }
    }
}
