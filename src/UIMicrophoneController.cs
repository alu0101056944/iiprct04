/// Marcos Barrios
/// Interfaces Inteligentes
/// Universidad de La Laguna
///
/// Record from microphone until stop button is pressed, then reproduce on
/// demand. Implemented using UI Buttons.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMicrophoneController : MonoBehaviour {

  private Button recordButton;
  private Button reproduceButton;
  private Button stopButton;

  private AudioSource audioSource;
  private AudioClip clipFromMicrophone;

  void Start() {
    audioSource = GetComponent<AudioSource>();
    initializeVisualElements();
    registerEventCallbacks();
    reproduceButton.SetEnabled(false);
    stopButton.SetEnabled(false);
  }

  private void initializeVisualElements() {
    GameObject uiObject = GameObject.FindWithTag("UIDocument");
    UIDocument uiDocument = uiObject.GetComponent<UIDocument>();
    VisualElement rootVisualElement = uiDocument.rootVisualElement;
    recordButton = rootVisualElement.Q<Button>("record-button");
    reproduceButton = rootVisualElement.Q<Button>("reproduce-button");
    stopButton = rootVisualElement.Q<Button>("stop-button");
  }

  private void registerEventCallbacks() {
    recordButton.RegisterCallback<ClickEvent>(ev => recordFromMicrophone());
    stopButton.RegisterCallback<ClickEvent>(ev => stopRecordingMicrophone());
    reproduceButton.RegisterCallback<ClickEvent>(ev => reproduceFromMicrophone());
  }

  private void recordFromMicrophone() {
    Microphone m = new Microphone();
    string nameOfActiveMicrophone = 
        UIDeviceSelector.NameOfActiveMicrophone;
    clipFromMicrophone = 
        Microphone.Start(nameOfActiveMicrophone, false, 60, 44100);
    stopButton.SetEnabled(true);
    reproduceButton.SetEnabled(false);
  }

  private void stopRecordingMicrophone() {
    Microphone.End(Microphone.devices[0]);
    stopButton.SetEnabled(false);
    if (clipFromMicrophone != null) {
      reproduceButton.SetEnabled(true);
    }
  }

  private void reproduceFromMicrophone() {
    if (clipFromMicrophone != null) {
      audioSource.clip = clipFromMicrophone;;
      audioSource.Play();
    } else {
      Debug.LogError("Couldn't reproduce microphone clip. Please record beforehand.");
    }
  }

}
