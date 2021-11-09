/// Marcos Barrios
/// Interfaces Inteligentes
/// Universidad de La Laguna
///
/// Outputs webcam texture to render texture

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCamera : MonoBehaviour {

  private Renderer renderer_;

  void Start() {
    WebCamTexture webcamTexture = new WebCamTexture();
    renderer_ = GetComponent<Renderer>();
    renderer_.material.mainTexture = webcamTexture;
    webcamTexture.Play();
  }

}
