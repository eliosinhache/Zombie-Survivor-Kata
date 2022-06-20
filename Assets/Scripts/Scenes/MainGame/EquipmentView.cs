using System.Collections;
using System.Collections.Generic;
using Classes;
using Scenes.MainGame.MVP;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentView : MonoBehaviour, IObserver
{
    [SerializeField] private MainGameView _view;
    [SerializeField] private Image _equipmentImage;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private EquipmentData _equipmentSelected;
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
    public void NotEquippated()
    {
        _equipmentImage.color = Color.gray;
    }

    public void ReceiveUpdate()
    {
        NotEquippated();
        foreach (Equipment equipment in _equipmentSelected.ReturnEquipments())
        {
            if (equipment == null) {
                return;}
            if (equipment.name == name)
            {
                if (equipment.equiped == "In Reserve") {SuccefullyEquippedInReserve();}
                else { SuccefullyEquippedInHand(); }
                return;
            }
        }
    }
}
