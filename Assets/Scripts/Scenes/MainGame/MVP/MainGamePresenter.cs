using System.Collections;
using System.Collections.Generic;
using Classes;
using Scenes.MainGame;
using Scenes.MainGame.MVP;
using UnityEngine;

public class MainGamePresenter : IMainGamePresenter
{
    private IMainGameView _mainGameView;
    private ISurvivor _survivor;
    private IZombie _zombie;
    private IGame _game;
    private List<SurvivorCharacterView> _survivorContainter;
    private List<ZombieCharacterView> _zombieContainter;
    public MainGamePresenter(IMainGameView mainGameView)
    {
        _mainGameView = mainGameView;
        _game = new Game();
        _survivorContainter = _mainGameView.ReturnSurvivorViews();
        _zombieContainter = _mainGameView.ReturnZombieViews();

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
        characterViewCharacter.Setevel(_survivor.ReturnLevel());
        characterViewCharacter.SetExperience(_survivor.CheckExperience());
        characterViewCharacter.SetLife(2);
    }

    public void SetInfoZombie(ZombieCharacterView zombieCharacterController)
    {
        zombieCharacterController.Setevel(_zombie.ReturnLevel());
        zombieCharacterController.SetLife(1);
    }

    public void CreateSurvivor(string nmeSurvivor)
    {
        _survivor = new Survivor(nmeSurvivor, _game, new SkillTree());
        _game.AddSurvivor(_survivor);
        _mainGameView.AddSurvivor();
    }

}
