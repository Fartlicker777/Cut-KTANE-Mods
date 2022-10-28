using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class TwoDimensionalMaze : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;

   public KMSelectable[] Buttons;
   public KMSelectable GoalKMS;
   public Material[] Colors;

   int[][] Grid = new int[][] {
      new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 1, 0},
      new int[] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0},
      new int[] { 1, 0, 0, 0, 1, 1, 1, 0, 0, 1},
      new int[] { 0, 0, 0, 1, 1, 0, 1, 0, 0, 1},
      new int[] { 0, 1, 1, 0, 0, 1, 0, 1, 0, 1},
      new int[] { 0, 0, 1, 1, 0, 1, 0, 1, 0, 1},
      new int[] { 1, 0, 0, 1, 1, 1, 1, 1, 0, 1},
      new int[] { 0, 1, 0, 1, 1, 0, 0, 1, 1, 0},
      new int[] { 0, 1, 1, 0, 0, 0, 0, 0, 1, 0},
      new int[] { 1, 0, 0, 1, 1, 1, 0, 1, 1, 0}
   };
   int[] CurrentSpot = { 0, 0 };
   int[] DirectionAmounts = { 0, 0, 0, 0};
   int[] Goal = { 0, 0};
   int[] ActualDirections = { 0, 1, 2, 3};
   bool[] RowChange = { false, false, false, false, false, false, false, false, false, false };
   bool[] ColChange = { false, false, false, false, false, false, false, false, false, false };

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

   void Awake () {
      ModuleId = ModuleIdCounter++;

      foreach (KMSelectable Button in Buttons) {
         Button.OnInteract += delegate () { ButtonPress(Button); return false; };
      }


      GoalKMS.OnInteract += delegate () { GoalPress(); return false; };

   }

   void ButtonPress (KMSelectable B) {
      for (int i = 0; i < 4; i++) {
         if (B == Buttons[i]) {
            UpdatePos(i);
         }
      }
      GoalKMS.GetComponent<MeshRenderer>().material = Colors[Grid[CurrentSpot[0]][CurrentSpot[1]]];
   }

   void UpdateQuirkIdentifier (string Input) {

   }

   void ScrambleDirections () { //Just so I remember what Actual Directions actually does
      ActualDirections.Shuffle();
   }

   void UpdatePos (int I) {
      switch (ActualDirections[I]) { //Up
         case 0:
            CurrentSpot[0] -= DirectionAmounts[I];
            CurrentSpot[0] = (CurrentSpot[0] + 10) % 10;
            break;
         case 1: // Down
            CurrentSpot[0] += DirectionAmounts[I];
            CurrentSpot[0] %= 10;
            break;
         case 2: //Right
            CurrentSpot[1] += DirectionAmounts[I];
            CurrentSpot[1] %= 10;
            break;
         case 3: //Left
            CurrentSpot[1] -= DirectionAmounts[I];
            CurrentSpot[1] = (CurrentSpot[1] + 10) % 10;
            break;
         default:
            break;
      }
      Debug.Log("Y=" + CurrentSpot[0].ToString() + ", " + "X=" + CurrentSpot[1].ToString());
   }

   void QuirkB () {
      //bool cont = false;
      int rand = 0;
      int rand2 = 0;
      int[] Row = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      int[] Col = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      Row.Shuffle();
      Col.Shuffle();
      for (int i = 0; i < 10; i++) {
         
         Debug.Log(Col[i].ToString() + " " + Row[i].ToString());
         RowChange[Row[i]] = true;
         ColChange[Col[i]] = true;
         Grid[Col[i]][Row[i]] = 1 - Grid[Col[i]][Row[i]];
      }
   }

   void QuirkC () {
      //bool cont = false;
      int rand = 0;
      int rand2 = 0;
      int[] Row = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      int[] Col = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      Row.Shuffle();
      Col.Shuffle();
      for (int i = 0; i < 10; i++) {

         Debug.Log(Col[i].ToString() + " " + Row[i].ToString());
         RowChange[Row[i]] = true;
         ColChange[Col[i]] = true;
         Grid[Col[i]][Row[i]] = 1 - Grid[Col[i]][Row[i]];
      }
   }

   void QuirkA () {

      //Debug.Log("-----------");
      //Debug.Log(ActualDirections[0]);
      //Debug.Log(ActualDirections[1]);
      //Debug.Log(ActualDirections[2]);
      //Debug.Log(ActualDirections[3]);
      //Debug.Log("-----------");

      DirectionAmounts[ActualDirections[0]] = Rnd.Range(1, 3);
      if (DirectionAmounts[ActualDirections[0]] == 2) {
         DirectionAmounts[ActualDirections[1]] = 1;
      }
      else {
         DirectionAmounts[ActualDirections[1]] = Rnd.Range(1, 3);
      }

      DirectionAmounts[ActualDirections[2]] = Rnd.Range(1, 3);
      if (DirectionAmounts[ActualDirections[2]] == 2) {
         DirectionAmounts[ActualDirections[3]] = 1;
      }
      else {
         DirectionAmounts[ActualDirections[3]] = Rnd.Range(1, 3);
      }


      Debug.Log("-----------");
      Debug.Log(ActualDirections[0]);
      Debug.Log(ActualDirections[1]);
      Debug.Log(ActualDirections[2]);
      Debug.Log(ActualDirections[3]);
      Debug.Log("-----------");

      Debug.Log("-----------");
      Debug.Log(DirectionAmounts[0]);
      Debug.Log(DirectionAmounts[1]);
      Debug.Log(DirectionAmounts[2]);
      Debug.Log(DirectionAmounts[3]);
      Debug.Log("-----------");

      /*for (int i = 0; i < 4; i++) {
         Debug.LogFormat("[2D Maze #{0}] True {1} goes {2} space(s).", ModuleId, new string[] { "u", "d", "r", "l" }[i], DirectionAmounts[i]);
      }*/
   }

   void GoalPress () {
      if (CurrentSpot[0] == Goal[0] && CurrentSpot[1] == Goal[1]) {
         GetComponent<KMBombModule>().HandlePass();
      }
      else {
         GetComponent<KMBombModule>().HandleStrike();
      }
   }

   void Start () {
      
      CurrentSpot[0] = Rnd.Range(0, 10);
      CurrentSpot[1] = Rnd.Range(0, 10);  //Generate random starting position

      ScrambleDirections();
      //Flip();
      QuirkA();
      
      //Debug.Log(DirectionAmounts[0]);
      //Debug.Log(DirectionAmounts[1]);
      //Debug.Log(DirectionAmounts[2]);
      //Debug.Log(DirectionAmounts[3]);
      //Debug.Log("-----------");

      for (int i = 0; i < 4; i++) {
         Debug.LogFormat("[2D Maze #{0}] Fake {1} actually goes {2} by {3}.", ModuleId, new string[] { "u", "d", "r", "l" }[i], new string[] { "u", "d", "r", "l" }[ActualDirections[i]], DirectionAmounts[ActualDirections[i]]);
      }
      Goal[1] = Bomb.GetSerialNumberNumbers().First();
      Goal[0] = Bomb.GetSerialNumberNumbers().Last();
      GoalKMS.GetComponent<MeshRenderer>().material = Colors[Grid[CurrentSpot[0]][CurrentSpot[1]]];
      Debug.Log("Y=" + CurrentSpot[0].ToString() + ", " + "X=" + CurrentSpot[1].ToString());
      
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
