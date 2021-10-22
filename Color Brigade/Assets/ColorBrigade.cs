using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class ColorBrigade : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public GameObject FiveSegmented;
    public GameObject SevenSegmented;
    public GameObject ElevenSegmented;
    public GameObject[] FiveSegments, SevenSegments, ElevenSegments;
    public Material[] Colors;
    public KMSelectable[] FiveButtons;
    public KMSelectable[] SevenButtons;
    public KMSelectable[] ElevenButtons;

    int[] Colors5 = new int[5];
    int[] Colors7 = new int[7];
    int[] Colors11 = new int[11];

    float[] Speeds = {1f, 2f, 3f};

    bool[] Directions = new bool[3];

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable Button in FiveButtons) {
            Button.OnInteract += delegate () { FivePress(Button); return false; };
        }
        foreach (KMSelectable Button in SevenButtons) {
            Button.OnInteract += delegate () { SevenPress(Button); return false; };
        }
        foreach (KMSelectable Button in ElevenButtons) {
            Button.OnInteract += delegate () { ElevenPress(Button); return false; };
        }
    }

    void FivePress (KMSelectable Button) {
      for (int i = 0; i < 5; i++) {
        if (Button == FiveButtons[i]) {
          Debug.Log(i);
        }
      }
    }

    void SevenPress (KMSelectable Button) {
      for (int i = 0; i < 5; i++) {
        if (Button == SevenButtons[i]) {
          Debug.Log(i);
        }
      }
    }

    void ElevenPress (KMSelectable Button) {
      for (int i = 0; i < 5; i++) {
        if (Button == ElevenButtons[i]) {
          Debug.Log(i);
        }
      }
    }

    void Start () {
      Speeds.Shuffle();
      for (int i = 0; i < 5; i++) {
        Colors5[i] = UnityEngine.Random.Range(0, 11);
        FiveSegments[i].GetComponent<MeshRenderer>().material = Colors[Colors5[i]];
      }
      for (int i = 0; i < 7; i++) {
        Colors7[i] = UnityEngine.Random.Range(0, 11);
        SevenSegments[i].GetComponent<MeshRenderer>().material = Colors[Colors7[i]];
      }
      for (int i = 0; i < 11; i++) {
        Colors11[i] = UnityEngine.Random.Range(0, 11);
        ElevenSegments[i].GetComponent<MeshRenderer>().material = Colors[Colors11[i]];
      }
      for (int i = 0; i < 3; i++) {
        Directions[i] = UnityEngine.Random.Range(0, 2) == 1 ? true : false;
      }

      StartCoroutine(FiveSpin());
      StartCoroutine(SevenSpin());
      StartCoroutine(ElevenSpin());
    }

    IEnumerator FiveSpin () {
      while (true) {
        FiveSegmented.transform.Rotate(0, Directions[0] ? Speeds[0] : 0 - Speeds[0], 0);
        yield return new WaitForSecondsRealtime(.01f);
      }
    }

    IEnumerator SevenSpin () {
      while (true) {
        SevenSegmented.transform.Rotate(0, Directions[1] ? Speeds[1] : 0 - Speeds[1], 0);
        yield return new WaitForSecondsRealtime(.01f);
      }
    }

    IEnumerator ElevenSpin () {
      while (true) {
        ElevenSegmented.transform.Rotate(0, Directions[2] ? Speeds[2] : 0 - Speeds[2], 0);
        yield return new WaitForSecondsRealtime(.01f);
      }
    }

    void Update () {

    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
    }

    IEnumerator TwitchHandleForcedSolve () {
      yield return null;
    }
}
