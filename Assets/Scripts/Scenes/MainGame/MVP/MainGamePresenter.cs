using System.Collections;
using System.Collections.Generic;
using Classes;
using Scenes.MainGame.MVP;
using UnityEngine;

public class MainGamePresenter : IPresenter
{
    private IView _view;
    private ISurvivor _survivor;
    private IZombie _zombie;
    private IGame _game;
    public MainGamePresenter(IView view)
    {
        _view = view;
        _game = new Game();
    }

    public void StartGame()
    {
        _survivor = new Survivor("Marian", _game, new SkillTree());
        _zombie = new Zombie();
        ShowSurvivorInfo(_survivor);
    }


    private void ShowSurvivorInfo(ISurvivor survivor)
    {
        _view.SetSurvivorLevel(survivor.ReturnLevel());
        _view.SetSurvivorExperience(survivor.CheckExperience());
    }
}
