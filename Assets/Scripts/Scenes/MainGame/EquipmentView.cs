using System.Collections;
using System.Collections.Generic;
using Scenes.MainGame.MVP;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentView : MonoBehaviour
{
    [SerializeField] private MainGameView _view;
    [SerializeField] private Image _equipmentImage;
    [SerializeField] private GameObject _optionPanel;
    public void ShowOptions()
    {
        _optionPanel.SetActive(!_optionPanel.activeSelf);
    }
    
    public void EquipateInReserve()
    {
        _view.EquippateInReserve(transform.gameObject.name);
        ShowOptions();
    }
    public void EquipateInHand()
    {
        _view.EquippateInHand(transform.gameObject.name);
        ShowOptions();
    }

    public void SuccefullyEquippedInReserve()
    {
        _equipmentImage.color = Color.cyan;
    }
    public void SuccefullyEquippedInHand()
    {
        _equipmentImage.color = Color.green;
    }
}
