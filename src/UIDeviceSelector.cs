/// Marcos Barrios
/// Interfaces Inteligentes
/// Universidad de La Laguna
///
/// Allows switching the microphone using the GUI.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDeviceSelector : MonoBehaviour {

  public static string NameOfActiveMicrophone = "";

  private Button changeMicrophone;
  private Label currentMicrophone;
  private ScrollView listOfMicrophones;

  void Start() {
    initializeVisualElements();
    listOfMicrophones.style.display = DisplayStyle.None; // to hide it
    changeMicrophone.RegisterCallback<ClickEvent>(ev => showListOfMicrophones());
    if (Microphone.devices.Length > 0) {
      NameOfActiveMicrophone = Microphone.devices[0];
      currentMicrophone.text = "Active microphone:" + NameOfActiveMicrophone;
    }
  }

  private void initializeVisualElements() {
    GameObject uiObject = GameObject.FindWithTag("UIDocument");
    UIDocument uiDocument = uiObject.GetComponent<UIDocument>();
    VisualElement rootVisualElement = uiDocument.rootVisualElement;
    currentMicrophone = rootVisualElement.Q<Label>("active-microphone-label");
    listOfMicrophones = rootVisualElement.Q<ScrollView>("available-microphones");
    changeMicrophone = rootVisualElement.Q<Button>("change-microphone-button");
  }

  /// A list of microphones should show up to be selected
  /// It is cleared everytime it is called instead of reinstancing the visual
  /// element as a temporary fix.
  private void showListOfMicrophones() {
    listOfMicrophones.Clear();
    listOfMicrophones.style.display = DisplayStyle.Flex; // from hiden to shown
    for (int i = 0; i < Microphone.devices.Length; i++) {
      string nameOfFoundMicrophone = Microphone.devices[i];

      Label foundMicrophone = new Label(nameOfFoundMicrophone);
      foundMicrophone.RegisterCallback<ClickEvent>(ev => {
        setActiveMicrophone(nameOfFoundMicrophone);
      });
      listOfMicrophones.Add(foundMicrophone);
    }
  }

  /// Only one active microphone, so it needs to change
  private void setActiveMicrophone(string nameOfMicrophone) {
    Microphone.End(NameOfActiveMicrophone);
    NameOfActiveMicrophone = nameOfMicrophone;
    currentMicrophone.text = "Active microphone:" + nameOfMicrophone;
    listOfMicrophones.style.display = DisplayStyle.None; // hide list
  }
  
}
