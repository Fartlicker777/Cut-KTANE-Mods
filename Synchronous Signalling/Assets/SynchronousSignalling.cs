using System; //Copyright (c) 2323, Jort Mama, All Rights Reserved.
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class SynchronousSignalling : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

    public KMSelectable[] Arrows;
    public GameObject Signal;
    public Material[] Colors;

    int[][] NumbersForColumns = {
      new int[] {2, 5, 4, 1, 3, 6},
      new int[] {6, 3, 4, 1, 2, 5},
      new int[] {1, 4, 2, 5, 6, 3}
    };
    int[] Selectors = {0, 0, 0, 0};
    List<int> PossibleStartingPositions = new List<int> {};
    int MazeSelector;
    int Ending;
    int Index;
    int Iteration;

    string[][][] WordsForPositions = new string[3][][] {
      new string[6][] { new string[4] {"Town", "Helix", "Quiver", "Xanthic"}, new string[4] {"Abhorrent", "Jury", "Zodiac", "Trance"}, new string[4] {"Sailor", "Digest", "Yacht", "Virus"}, new string[4] {"Overt", "Unanimous", "Night", "Gaze"}, new string[4] {"Focalize", "Kerosene", "Length", "Morse"}, new string[4] {"Probable", "Laser", "Waxy", "Ursa"}},
      new string[6][] { new string[4] {"Town", "Abhorrent", "Ursa", "Night"}, new string[4] {"Focalize", "Waxy", "Virus", "Gaze"}, new string[4] {"Morse", "Unanimous", "Yacht", "Jury"}, new string[4] {"Zodiac", "Probable", "Laser", "Digest"}, new string[4] {"Overt", "Sailor", "Length", "Xanthic"}, new string[4] {"Helix", "Quiver", "Trance", "Kerosene"}},
      new string[6][] { new string[4] {"Helix", "Abhorrent", "Virus", "Night"}, new string[4] {"Kerosene", "Probable", "Quiver", "Trance"}, new string[4] {"Ursa", "Length", "Overt", "Yacht"}, new string[4] {"Focalize", "Morse", "Waxy", "Gaze"}, new string[4] {"Laser", "Unanimous", "Sailor", "Jury"}, new string[4] {"Town", "Xanthic", "Zodiac", "Digest"}}
    };
    static readonly string[] Mazes = {
      "-------------" +
      "|X|X|XOXOX|X|" +
      "|O-O---O-O-O|" +
      "|XOX|XOX|X|X|" +
      "|--O-O---O-O|" +
      "|X|X|X|XOX|X|" +
      "|O-O-O-O---O|" +
      "|X|X|X|XOXOX|" +
      "|O-O-O-----O|" +
      "|XOXOXOXOX|X|" +
      "|----O---O--|" +
      "|XOXOX|XOXOX|" +
      "-------------",

      "-------------" +
      "|XOX|XOXOXOX|" +
      "|O-O-O-----O|" +
      "|X|XOXOX|XOX|" +
      "|O-----O----|" +
      "|X|XOX|X|XOX|" +
      "|O-O-O-O-O--|" +
      "|X|X|XOXOX|X|" +
      "|--O---O---O|" +
      "|X|XOX|X|XOX|" +
      "|O---O-O-O-O|" +
      "|XOXOX|XOX|X|" +
      "-------------",

      "-------------" +
      "|X|XOX|XOX|X|" +
      "|O-O-O-O-O-O|" +
      "|XOX|X|X|X|X|" +
      "|O---O-O-O-O|" +
      "|X|XOX|X|X|X|" +
      "|O-O---O-O-O|" +
      "|X|XOX|X|X|X|" +
      "|O---O-O-O-O|" +
      "|X|XOX|X|X|X|" +
      "|O-O---O-O-O|" +
      "|X|XOXOX|XOX|" +
      "-------------",

      "-------------" +
      "|XOXOX|XOX|X|" +
      "|O-O---O-O-O|" +
      "|X|XOXOX|XOX|" +
      "|O---O-O-O--|" +
      "|X|X|X|X|X|X|" +
      "|O-O-O-----O|" +
      "|XOX|XOXOXOX|" +
      "|--O-----O--|" +
      "|X|X|XOXOX|X|" +
      "|O-O-O---O-O|" +
      "|XOX|XOX|XOX|" +
      "-------------"
    };
    string[] MorseLetters = {".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.."};
    //Dit I ,|| Dot O .|| Dah A =|| Dash S -
    string[] MoreLetters = {".-.,.", "=", "=,,", "=,-", ".,.-=", "-.-", ".", ",..", "=--=,", "=.,", ",", "=-,=", ",,.", "--.=", "-----", "-", "==.", ",--=", "-,==", "=.,,", "=====", "=,", ",=", "=.", "-,", ",=-"};
    string[] Keywords = new string[3];
    string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string Columns = "ABCDEF";
    string RedChannel = string.Empty;
    string GreenChannel = string.Empty;
    string BlueChannel = string.Empty;

    char[] ColorChannels = {'R', 'G', 'B'};
    char[] PositionIndicators = {'C', 'R', 'E'};

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable Arrow in Arrows)
            Arrow.OnInteract += delegate () { ArrowPress(Arrow); return false; };
    }

    void Start () {
      ColorChannels.Shuffle();
      PositionIndicators.Shuffle();
      MazeSelector = (ColorChannels[0] == 'R' ? 0 : 1) + (Bomb.GetSerialNumber().Any(x => "AEIOU".Contains(x)) ? 0 : 2);
      var XXX = 0;
      for (int i = 0; i < 3; i++) {
        Selectors[i] = UnityEngine.Random.Range(0, WordsForPositions[i].Length);
        switch (PositionIndicators[i]) {
          case 'C': Keywords[i] = WordsForPositions[i][Selectors[i]][UnityEngine.Random.Range(0, WordsForPositions[i][i].Length)]; Index += Selectors[i]; break;
          case 'R': Keywords[i] = NumbersForColumns[i][Selectors[i]].ToString(); Index += Selectors[i] * 6; break;
          case 'E': XXX = UnityEngine.Random.Range(0, 6); Ending = Selectors[i] + NumbersForColumns[Selectors[i]][XXX] * 6; Keywords[i] = WordsForPositions[i][Selectors[i]][UnityEngine.Random.Range(0, WordsForPositions[i][i].Length)] + "_" + NumbersForColumns[Selectors[i]][XXX].ToString(); break;
        }
      }
      for (int i = 0; i < Mazes[MazeSelector].Length; i++) {
        if (Mazes[MazeSelector][i] == 'X')
          PossibleStartingPositions.Add(i);
      }
      Index = PossibleStartingPositions[Index];
      switch (ColorChannels[0]) {
        case 'R':
        for (int i = 0; i < Keywords[0].Length; i++)
          RedChannel += MorseLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
        case 'G':
        for (int i = 0; i < Keywords[0].Length; i++)
          GreenChannel += MorseLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
        case 'B':
        for (int i = 0; i < Keywords[0].Length; i++)
          BlueChannel += MorseLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
      }

      switch (ColorChannels[1]) {
        case 'R':
        for (int i = 0; i < Keywords[0].Length; i++)
          RedChannel += TapLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
        case 'G':
        for (int i = 0; i < Keywords[0].Length; i++)
          GreenChannel += TapLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
        case 'B':
        for (int i = 0; i < Keywords[0].Length; i++)
          BlueChannel += TapLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
      }

      switch (ColorChannels[2]) {
        case 'R':
        for (int i = 0; i < Keywords[0].Length; i++)
          RedChannel += MoreLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
        case 'G':
        for (int i = 0; i < Keywords[0].Length; i++)
          GreenChannel += MoreLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
        case 'B':
        for (int i = 0; i < Keywords[0].Length; i++)
          BlueChannel += MoreLetters[Keywords.IndexOf(Keywords[0][i])] + "_";
        break;
      }
      StartCoroutine(ColorChanger());
    }

    IEnumerator ColorChanger () {

    }

    void ArrowPress (KMSelectable Arrow) {

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
