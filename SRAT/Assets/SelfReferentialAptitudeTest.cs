using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class SelfReferentialAptitudeTest : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

    public KMSelectable[] Buttons;
    public TextMesh Question;
    public TextMesh[] AnswerText;

    int[] Totals = new int[5];
    List<int>[] PossibleQuestions = new List<int>[] {
      new List<int>{0},
      new List<int>{0},
      new List<int>{0},
      new List<int>{0},
      new List<int>{0}
    };

    static string[] PossibleQuestionsStatic = {"The answer to this question ", "The only question with the same answer as this is one ", "The answer to question ", "The first question whose answer is ", "The number of questions whose answer is a "};
    string PossibleAnswer = "ABCDE";

    char[] Answers = new char[5];

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable Button in Buttons) {
            Button.OnInteract += delegate () { ButtonsPress(Button); return false; };
        }
    }

    void Start () {
      for (int i = 0; i < 5; i++) {
        var x = UnityEngine.Random.Range(0, 5);
        Answers[i] = PossibleAnswer[x];
        Totals[i] = x;
      }
      for (int i = 0; i < 5; i++)
        if (Totals[i] == 2)
          PossibleQuestions[i].Add(1);
      
    }

    void ButtonsPress (KMSelectable Button) {

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
