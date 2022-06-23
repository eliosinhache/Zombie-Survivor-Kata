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
    private EquipmentData _equipmentData;
    private ReactiveProperty<StringReactiveProperty> _nameSelected;
    private int _numOfZombie=0;

    public MainGamePresenter(IMainGameView mainGameView, CharacterData survivorData, CharacterData zombieData, EquipmentData equipment)
    {
        _mainGameView = mainGameView;
        _game = new Game();
        _survivorContainter = _mainGameView.ReturnSurvivorViews();
        _zombieContainter = _mainGameView.ReturnZombieViews();
        _selectedSurvivor = survivorData;
        _selectedZombie = zombieData;
        _equipmentData = equipment;
        SubscribeEquipmentViewToData();
    }

    private void SubscribeEquipmentViewToData()
    {
        foreach (EquipmentView view in _mainGameView.ReturnEquipmentViewList())
        {
            _equipmentData.Attach(view);
        }
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
    public void CreateSurvivor(string nameSurvivor)
    {
        SkillTree skillTree = CreateInitialSkillTree();
        _survivor = new Survivor(nameSurvivor, _game, new SkillTree());
        if (CanAddSurvivorWithName(nameSurvivor))
        {
            _game.AddSurvivor(_survivor);
            _mainGameView.AddSurvivorGUI(_survivor.ReturnName());
        }
    }

    private SkillTree CreateInitialSkillTree()
    {
        ISkill skill = new Skill("+1 Action",  LevelEnum.Yellow, 7);
        skill = new Skill("+1 Die", LevelEnum.Orange, 18);
        skill = new Skill("+1 Free Move Action", LevelEnum.Orange, 18);
        skill = new Skill("Hoard", LevelEnum.Red, 42);
        skill = new Skill("Tough", LevelEnum.Red, 42);
        skill = new Skill("Sniper", LevelEnum.Red, 42);
        return new SkillTree();
    }

    public void CreateZombie()
    {
        _zombie = new Zombie();
        _zombie.SetName($"zombieN {_numOfZombie}");
        _game.AddZombie(_zombie);
        _mainGameView.AddZombieGUI(_zombie.ReturnName());
        _numOfZombie++;
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
        if (_survivor == null || _zombie == null) {return; }
        _survivor.DealDamage(_zombie);

        _survivorContainter[_game.ReturnAllSurvivors().IndexOf(_survivor)].SetExperience(_survivor.CheckExperience());
        _survivorContainter[_game.ReturnAllSurvivors().IndexOf(_survivor)].SetLevel(_survivor.ReturnLevel().ToString());
        
        _selectedZombie.Notify();
        
        RefreshDataSurvivorSelected(_survivor.ReturnName());
        RefreshDataZombieSelected(_zombie.ReturnName());
    }

    public void SubscribeNewZombieToData(ZombieCharacterView zombieView)
    {
        _selectedZombie.Attach(zombieView);
    }

    public void SubscribeNewSurvivorToData(SurvivorCharacterView survivorView)
    {
        _selectedSurvivor.Attach(survivorView);
    }

    public void ZombieAttackASurvivor()
    {
        _survivor = SearchSurvivorWithName(_selectedSurvivor.characterName.Value);
        _zombie = SearchZombieWithName(_selectedZombie.characterName.Value);
        _zombie.DealDamage(_survivor);
        
        RefreshDataSurvivorSelected(_survivor.ReturnName());
        RefreshDataZombieSelected(_zombie.ReturnName());
        
        _selectedSurvivor.Notify();
    }

    public void ReadHistory()
    {
        _mainGameView.ShowHistoryOnDebug(_game.returnCompleteHistory());
    }

    public void EquipateInReserve(string equipment)
    {
        _survivor = SearchSurvivorWithName(_selectedSurvivor.characterName.Value);
        if (_survivor == null) {return;}
        Equipment newEquipment = new Equipment(equipment);
        _survivor.Equipate(newEquipment, "In Reserve");
        _game.ASurvivorEquippedAWeapon(_survivor, newEquipment, "In Reserve");
        RefreshDataEquipmentOfSurvivorSelected(_survivor);
    }

    private void RefreshDataEquipmentOfSurvivorSelected(ISurvivor survivor)
    {
        _equipmentData.RefreshEquipment(survivor.CheckIfIsAlive() ? survivor.ReturnAllEquipment() : new List<Equipment>());
    }

    public void EquipateInHand(string equipment)
    {
        _survivor = SearchSurvivorWithName(_selectedSurvivor.characterName.Value);
        if (_survivor == null) {return;}
        Equipment newEquipment = new Equipment(equipment);
        _survivor.Equipate(newEquipment, "In Hand");
        _game.ASurvivorEquippedAWeapon(_survivor, newEquipment, "In Hand");
        RefreshDataEquipmentOfSurvivorSelected(_survivor);
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
                _selectedSurvivor.lifes = itemSurvivor.ReturnLifes();
                RefreshDataEquipmentOfSurvivorSelected(itemSurvivor);
                FillSelectedSurvivor(); //cambiar a reactividad de CharacterDAta
            }
        }
    }

}
