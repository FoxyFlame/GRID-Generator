using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {

	public TextMeshProUGUI highestFPS;
	public TextMeshProUGUI averageFPS;
	public TextMeshProUGUI lowestFPS;


	FPSCounter fpsCounter;

	void Awake () {
		fpsCounter = GetComponent<FPSCounter>();
	}

	void Update () {
		Display();
	}

	void Display()
    {
		GetFPS(highestFPS, fpsCounter.HighestFPS);
		GetFPS(averageFPS, fpsCounter.AverageFPS);
		GetFPS(lowestFPS, fpsCounter.LowestFPS);
	}

	void GetFPS(TextMeshProUGUI label, int fps) {
		label.text = fps.ToString();
	}
}