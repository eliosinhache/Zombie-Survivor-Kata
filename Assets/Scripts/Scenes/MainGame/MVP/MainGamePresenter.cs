using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using Scenes.MainGame;
using Scenes.MainGame.MVP;
using ScriptableObject;
using UniRx;
using UnityEngine;

public class MainGamePresenter : IMainGamePresenter
{
    private IMainGameView _mainGameView;
    private ISurvivor _survivor;
    private IZombie _zombie;
    private IGame _game;
    private List<ISurvivor> _survivorsCreated = new List<ISurvivor>();
    private List<SurvivorCharacterView> _survivorContainter;
    private List<ZombieCharacterView> _zombieContainter;

    private CharacterData selectedSurvivor;
    private ReactiveProperty<StringReactiveProperty> _nameSelected;

    public MainGamePresenter(IMainGameView mainGameView, CharacterData characterData)
    {
        _mainGameView = mainGameView;
        _game = new Game();
        _survivorContainter = _mainGameView.ReturnSurvivorViews();
        _zombieContainter = _mainGameView.ReturnZombieViews();
        selectedSurvivor = characterData;
        selectedSurvivor.characterName.Subscribe(item => FillSelectedSurvivor());
    }

    private void FillSelectedSurvivor()
    {
        _mainGameView.FillSelectedSurvivor(selectedSurvivor.characterName.Value, selectedSurvivor.characterLevel.ToString());
    }

    public void StartGame()
    {
        CreateSurvivor("Marian");
        CreateZombie();
    }

    private void CreateZombie()
    {
        _zombie = new Zombie();
        _mainGameView.AddZombie();
    }

    public void SetInfoSurvivor(SurvivorCharacterView characterViewCharacter)
    {
        characterViewCharacter.SetLevel(_survivor.ReturnLevel().ToString());
        characterViewCharacter.SetExperience(_survivor.CheckExperience());
        characterViewCharacter.SetLife(2);
        characterViewCharacter.SetNameView(characterViewCharacter.name);
    }

    public void SetInfoZombie(ZombieCharacterView zombieCharacterController)
    {
        zombieCharacterController.SetLevel(_zombie.ReturnLevel().ToString());
        zombieCharacterController.SetLife(1);
    }

    public void CreateSurvivor(string nmeSurvivor)
    {
        _survivor = new Survivor(nmeSurvivor, _game, new SkillTree());
        _game.AddSurvivor(_survivor);
        _survivorsCreated.Add(_survivor);
        _mainGameView.AddSurvivorGUI(_survivor.ReturnName());
    }

    public void ASurvivorWasSelected(string survivor)
    {
        RefreshDataSurvivorSelected(survivor);
    }

    private void RefreshDataSurvivorSelected(string survivor)
    {
        foreach (ISurvivor itemSurvivor in _survivorsCreated)
        {
            if (itemSurvivor.ReturnName() == survivor)
            {
                selectedSurvivor.characterLevel.Value = itemSurvivor.ReturnLevel().ToString();
                selectedSurvivor.characterName.Value = itemSurvivor.ReturnName();
                FillSelectedSurvivor();
            }
        }
    }
}
