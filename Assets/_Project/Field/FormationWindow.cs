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
    public PlayDescription CurrentAction;
    public GameObject Marker1;
    public GameObject Marker2;
    private GameObject[] Caps;
    public void Start() {
      UpdateField();
      UpdateCapRefs();
      RepositionCaps();
      Apply();
      gameObject.SetActive(false);
    }
    private void UpdateField() {
      UpdateMarker(Marker1, CurrentAction.MarkerCurrent);
      UpdateMarker(Marker2, CurrentAction.MarkerToGo);
    }
    private void UpdateCapRefs() {
      Caps = GameObject.FindGameObjectsWithTag("Cap");
    }
    private void RepositionCaps() {
      for (int i = 0; i < 11; i++) {
        Cap Cap = Caps[i].GetComponent<Cap>();
        Vector3 normal = Cap.transform.localPosition;
        float normalYards = Converter.CapXPositionToYards(normal.x);
        float yardsDiff = CurrentAction.MarkerCurrent - 50.0f;
        float newYards = normalYards + yardsDiff;
        float newX = Converter.YardsToCapXPosition(newYards);
        Cap.transform.localPosition = new Vector3(newX, normal.y, normal.z);
      }

    }
    private void UpdateMarker(GameObject Marker, float yards) {
      Vector3 old = Marker.transform.localPosition;
      float newX = Converter.YardsToCapXPosition(yards);
      Marker.transform.localPosition = new Vector3(newX, old.y, old.z);
    }

    public void Apply() {
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
