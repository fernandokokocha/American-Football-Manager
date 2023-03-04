using UnityEngine;
using Zenject;
using TMPro;
using System;
using System.Collections.Generic;

namespace AmericanFootballManager {
  public class FormationWindow : MonoBehaviour {
    [Inject] private ActionController ActionController;
    public TMP_Dropdown ProgramDropdown;
    private Cap Selected;
    public void Start() {
      Apply();
      gameObject.SetActive(false);
    }

    public void Apply() {
      GameObject[] Caps = GameObject.FindGameObjectsWithTag("Cap");

      for (int i = 0; i < 11; i++) {
        Cap Cap = Caps[i].GetComponent<Cap>();
        Cap.RepositionPlayer();
      }
    }

    public void HandleSelect(Cap Cap) {
      if (Selected) Selected.Unselect();
      Selected = Cap;
      GameObject Player = Cap.MyPlayer;
      Cap.Select();
      SetupDropdown(Cap);
    }
    public void HandleUnselect() {
      if (Selected) Selected.Unselect();
      ProgramDropdown.ClearOptions();
    }
    private void SetupDropdown(Cap Cap) {
      ProgramDropdown.ClearOptions();
      List<String> Options = new();
      foreach (string prog in Enum.GetNames(typeof(AvailableProgram))) {
        Options.Add(prog);
      }
      ProgramDropdown.AddOptions(Options);
      PlayerBehaviour Behaviour = Cap.MyPlayer.GetComponent<PlayerBehaviour>();
      ProgramDropdown.SetValueWithoutNotify(((int)Behaviour.ChosenProgram));
    }

    public void HandleProgramChange() {
      int value = ProgramDropdown.value;
      AvailableProgram NewProgram = (AvailableProgram)value;
      PlayerBehaviour Behaviour = Selected.MyPlayer.GetComponent<PlayerBehaviour>();
      Behaviour.ChosenProgram = NewProgram;
      Behaviour.SetProgram();
    }
  }
}
