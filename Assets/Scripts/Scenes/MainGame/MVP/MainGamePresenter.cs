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
    private List<SurvivorView> _survivorContainter;
    private List<ZombieView> _zombieContainter;
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

    public void SetInfoSurvivor(SurvivorView viewCharacter)
    {
        viewCharacter.Setevel(_survivor.ReturnLevel());
        viewCharacter.SetExperience(_survivor.CheckExperience());
        viewCharacter.SetLife(2);
    }

    public void SetInfoZombie(ZombieView zombieController)
    {
        zombieController.Setevel(_zombie.ReturnLevel());
        zombieController.SetLife(1);
    }

    public void CreateSurvivor(string nmeSurvivor)
    {
        _survivor = new Survivor(nmeSurvivor, _game, new SkillTree());
        _game.AddSurvivor(_survivor);
        _mainGameView.AddSurvivor();
    }

}
