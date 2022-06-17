using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using Scenes.MainGame;
using Scenes.MainGame.MVP;
using ScriptableObject;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class MainGamePresenter : IMainGamePresenter
{
    private IMainGameView _mainGameView;
    private ISurvivor _survivor;
    private IZombie _zombie;
    private IGame _game;
    private List<SurvivorCharacterView> _survivorContainter;
    private List<ZombieCharacterView> _zombieContainter;

    private CharacterData _selectedSurvivor;
    private CharacterData _selectedZombie;
    private ReactiveProperty<StringReactiveProperty> _nameSelected;

    public MainGamePresenter(IMainGameView mainGameView, CharacterData survivorData, CharacterData zombieData)
    {
        _mainGameView = mainGameView;
        _game = new Game();
        _survivorContainter = _mainGameView.ReturnSurvivorViews();
        _zombieContainter = _mainGameView.ReturnZombieViews();
        _selectedSurvivor = survivorData;
        _selectedZombie = zombieData;
        // _selectedSurvivor.characterName.Subscribe(item => FillSelectedSurvivor());
    }

    private void FillSelectedSurvivor()
    {
        _mainGameView.FillSelectedSurvivor(_selectedSurvivor.characterName.Value, _selectedSurvivor.characterLevel.ToString());
    }

    public void StartGame()
    {
        CreateSurvivor("Marian");
        CreateZombie();
    }

    private void CreateZombie()
    {
        _zombie = new Zombie();
        _game.AddZombie(_zombie);
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

    public void CreateSurvivor(string nameSurvivor)
    {
        _survivor = new Survivor(nameSurvivor, _game, new SkillTree());
        if (CanAddSurvivorWithName(nameSurvivor))
        {
            _game.AddSurvivor(_survivor);
            _mainGameView.AddSurvivorGUI(_survivor.ReturnName());
        }
    }

    private bool CanAddSurvivorWithName(string nameSurvivor)
    {
        return !_game.ReturnAllSurvivors().Exists(survivor => survivor.ReturnName() == nameSurvivor);
    }

    public void ASurvivorWasSelected(string survivor)
    {
        RefreshDataSurvivorSelected(survivor);
    }

    public void AZombieWasSelected(string zombieName)
    {
        RefreshDataZombieSelected(zombieName);
    }

    public void SurvivorAttackAZombie()
    {
        _survivor = SearchSurvivorWithName(_selectedSurvivor.characterName.Value);
        _zombie = SearchZombieWithName(_selectedZombie.characterName.Value);
        _survivor.DealDamage(_zombie);

        _survivorContainter[_game.ReturnAllSurvivors().IndexOf(_survivor)].SetExperience(_survivor.CheckExperience());
        _survivorContainter[_game.ReturnAllSurvivors().IndexOf(_survivor)].SetLevel(_survivor.ReturnLevel().ToString());
        
        _zombieContainter[_game.ReturnAllZombies().IndexOf(_zombie)].DecreaseLife(1); //Al zombie have when receive 1 damage
        
        RefreshDataSurvivorSelected(_survivor.ReturnName());
        RefreshDataZombieSelected(_zombie.ReturnName());
    }

    private IZombie SearchZombieWithName(string selectedZombieName)
    {
        foreach (IZombie itemZombie in _game.ReturnAllZombies())
        {
            if (itemZombie.ReturnName() == selectedZombieName )
            {
                return itemZombie;
            }
        }

        return null;
    }

    private ISurvivor SearchSurvivorWithName(string selectedSurvivorName)
    {
        foreach (ISurvivor itemSurvivor in _game.ReturnAllSurvivors())
        {
            if (itemSurvivor.ReturnName() == selectedSurvivorName)
            {
                return itemSurvivor;
            }
        }

        return null;
    }

    private void RefreshDataZombieSelected(string zombieName)
    {
        bool existAny = false;
        foreach (IZombie itemZombie in _game.ReturnAllZombies())
        {
            if (itemZombie.ReturnName() == zombieName && !existAny)
            {
                existAny = true;
                _selectedZombie.characterLevel.Value = itemZombie.ReturnLevel().ToString();
                _selectedZombie.characterName.Value = itemZombie.ReturnName();
                FillSelectedZombie();
            }
        }
    }

    private void FillSelectedZombie()
    {
        _mainGameView.FillSelectedZombie(_selectedZombie.characterName.Value, _selectedZombie.characterLevel.ToString());
    }

    private void RefreshDataSurvivorSelected(string survivor)
    {
        foreach (ISurvivor itemSurvivor in _game.ReturnAllSurvivors())
        {
            if (itemSurvivor.ReturnName() == survivor)
            {
                _selectedSurvivor.characterLevel.Value = itemSurvivor.ReturnLevel().ToString();
                _selectedSurvivor.characterName.Value = itemSurvivor.ReturnName();
                FillSelectedSurvivor();
            }
        }
    }

}
