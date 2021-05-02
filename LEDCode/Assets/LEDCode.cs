using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class LEDCode : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] LEDs;
    public KMSelectable[] Arrows;
    public KMSelectable Submit;
    public TextMesh Numbah;
    public GameObject[] LEDColors;
    public Material[] Colors;

    int[][] ColorsArray = new int[][] {
      new int[] {5, 0, 4}, //11/22
      new int[] {0, 1, 2}, //2/13
      new int[] {1, 0, 5}, //...
      new int[] {3, 1, 4},
      new int[] {2, 3, 1},
      new int[] {4, 2, 3},
      new int[] {4, 5, 1},
      new int[] {0, 2, 3},
      new int[] {3, 4, 2},
      new int[] {5, 0, 4}
    };
    int[][] DisplayingColors = new int[23][] {
      new int[7], new int[7], new int[7], new int[7], new int[7], new int[7],
      new int[7], new int[7], new int[7], new int[7], new int[7],
      new int[7], new int[7], new int[7],
      new int[7], new int[7], new int[7],
      new int[7], new int[7], new int[7],
      new int[7], new int[7], new int[7]
    };
    int[] OnOffState = new int[23];

    bool[][] LEDStates = new bool[10][] {
      new bool[] {true, true, true, false, false, true, false},
      new bool[] {true, true, false, false, true, true, false},
      new bool[] {true, true, false, true, true, false, false},
      new bool[] {true, false, false, false, false, true, false},
      new bool[] {true, false, true, true, true, false, false},
      new bool[] {true, false, false, true, true, true, false},
      new bool[] {true, false, true, false, false, false, false},
      new bool[] {true, false, false, false, true, false, false},
      new bool[] {true, false, false, true, false, false, false},
      new bool[] {true, true, true, false, true, false, false}
    };

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable LED in LEDs) {
            LED.OnInteract += delegate () { LEDPress(LED); return false; };
        }

        foreach (KMSelectable Arrow in Arrows) {
            Arrow.OnInteract += delegate () { ArrowPress(Arrow); return false; };
        }

        Submit.OnInteract += delegate () { SubmitPress(); return false; };

    }

    void Start () {
      for (int i = 0; i < 23; i++) {
        int ChosenSequence = UnityEngine.Random.Range(0,10)
        OnOffState[i] = ChosenSequence;
        for (int j = 0; j < 7; j++) {
          DisplayingColors[i][j] = UnityEngine.Random.Range(0,6);
          if (!LEDStates[ChosenSequence][j]) {
            while (DisplayingColors[i][j].Any()) {
              DisplayingColors[i][j] = UnityEngine.Random.Range(0,6);
            }
          }
        }
      }

    }

    void LEDPress (KMSelectable LED) {

    }

    void ArrowPress (KMSelectable Arrow) {

    }

    void SubmitPress () {

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
