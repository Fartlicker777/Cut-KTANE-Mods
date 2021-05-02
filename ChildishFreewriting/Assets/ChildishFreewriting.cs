using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class ChildishFreewriting : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] Wireslol;
    public TextMesh[] Ruleslol;
    public TextMesh Timerll;
    public TextMesh WhatsTheDayTodayJuneFifteenthWhenIsYourConcertJuneEleventhCanIComeToYourViolinConcertItAlreadyHappenedWhatsTheDayTodayJuneFifteenthWhenIsYourViolinConcertJuneEleventh;
    public KMSelectable Startlol;
    public Material[] Colorlol;
    public GameObject[] Doorslol;
    public GameObject[] WiresButColorForThemObkectGamelol;
    public GameObject[] WiresButColorForThemObkectGameButCutlol;


    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    string[] Ruleslolol = {"", "", ""};
    int[][] ColorCuttingCombinations = new int[6][] {
      new int[4] {1234, 1243, 1423, 1432},
      new int[4] {1324, 1342, 2134, 2143},
      new int[4] {2314, 2341, 2431, 2413},
      new int[4] {3124, 3142, 3214, 3241},
      new int[4] {3421, 3412, 4132, 4123},
      new int[4] {4321, 4312, 4231, 4213}
    };
    string Date = "";
    string[] Descriptions = {"Weird", "Wicked", "Ugly", "Cool"};
    //string[] Colors = {"Red", "Blue", "Green", "Yellow"};
    string[] ColorsWhenMoved = {"", "", "", ""};
    int[] Randomized = {0, 1, 2, 3};
    bool[] CutTHingValidityCheck = {false, false, false, false};
    int Order = 0;
    int[] WireColors = {0, 0, 0, 0, 0};
    bool[] Validity = {false, false, false, false, false};

    void Awake () {
        moduleId = moduleIdCounter++;
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */

        Startlol.OnInteract += delegate () { StartPress(); return false; };

    }

    // Use this for initialization
    void Start () {

    }

    void StartPress () {
      DateAndTimeAndColorGeneration();
    }

    void DateAndTimeAndColorGeneration () {
      int Fuck = UnityEngine.Random.Range(0, 6);
      int Weed = UnityEngine.Random.Range(0, 4);
      Date = "";
      switch (Weed) {
        case 0:
        switch (UnityEngine.Random.Range(0,3)) {
          case 0:
          Date += "Jan " + UnityEngine.Random.Range(1,32) + ", ";
          break;
          case 1:
          Date += "Feb " + UnityEngine.Random.Range(1,29) + ", ";
          break;
          case 2:
          Date += "Mar " + UnityEngine.Random.Range(1,32) + ", ";
          break;
        }
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 1:
        switch (UnityEngine.Random.Range(0,3)) {
          case 0:
          Date += "Apr " + UnityEngine.Random.Range(1,31) + ", ";
          break;
          case 1:
          Date += "May " + UnityEngine.Random.Range(1,32) + ", ";
          break;
          case 2:
          Date += "Jun " + UnityEngine.Random.Range(1,31) + ", ";
          break;
        }
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 2:
        switch (UnityEngine.Random.Range(0,3)) {
          case 0:
          Date += "Jul " + UnityEngine.Random.Range(1,32) + ", ";
          break;
          case 1:
          Date += "Aug " + UnityEngine.Random.Range(1,32) + ", ";
          break;
          case 2:
          Date += "Sep " + UnityEngine.Random.Range(1,31) + ", ";
          break;
        }
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 3:
        switch (UnityEngine.Random.Range(0,3)) {
          case 0:
          Date += "Oct " + UnityEngine.Random.Range(1,32) + ", ";
          break;
          case 1:
          Date += "Nov " + UnityEngine.Random.Range(1,31) + ", ";
          break;
          case 2:
          Date += "Dec " + UnityEngine.Random.Range(1,32) + ", ";
          break;
        }
        break;
      }
      switch (Fuck) {
        case 0:
        Date += UnityEngine.Random.Range(1876, 1901).ToString();
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 1:
        Date += UnityEngine.Random.Range(1901, 1926).ToString();
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 2:
        Date += UnityEngine.Random.Range(1926, 1951).ToString();
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 3:
        Date += UnityEngine.Random.Range(1951, 1976).ToString();
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 4:
        Date += UnityEngine.Random.Range(1976, 2001).ToString();
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
        case 5:
        Date += UnityEngine.Random.Range(2001, 2026).ToString();
        Order = ColorCuttingCombinations[Fuck][Weed];
        break;
      }
      WhatsTheDayTodayJuneFifteenthWhenIsYourConcertJuneEleventhCanIComeToYourViolinConcertItAlreadyHappenedWhatsTheDayTodayJuneFifteenthWhenIsYourViolinConcertJuneEleventh.text = Date;
      /*var DictionaryNumberToColor = new Dictionary<int, string>() {
        [0] = "Red",
        [1] = "Blue",
        [2] = "Green",
        [3] = "Yellow"
      };*/
      Debug.Log(Order);
      for (int i = 0; i < 4; i++) {
        ColorsWhenMoved[i] = Order.ToString()[i].ToString();
      }
      for (int i = 0; i < 5; i++) {
        int FuckYou = UnityEngine.Random.Range(0, 4);
        WireColors[i] = FuckYou;
        WiresButColorForThemObkectGamelol[i].GetComponent<MeshRenderer>().material = Colorlol[FuckYou];
        WiresButColorForThemObkectGameButCutlol[i].GetComponent<MeshRenderer>().material = Colorlol[FuckYou];
      }
      Randomized.Shuffle();
      string CutWires = Descriptions[Randomized[0]];
      string wirenamelolidoitlater = Descriptions[Randomized[1]];
      string WeedChungus = Descriptions[Randomized[2]];
      Ruleslolol[0] = String.Format("I don't like\n{0} wires", CutWires);
      Ruleslolol[1] = String.Format("All {0} wires\nmake adjacent\nwires {1}", wirenamelolidoitlater, CutWires);
      Ruleslolol[2] = String.Format("{0} wires\naren't {1}", WeedChungus, CutWires);
      for (int i = 0; i < 3; i++) {
        Ruleslol[i].text = Ruleslolol[i];
      }
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
    }
}
