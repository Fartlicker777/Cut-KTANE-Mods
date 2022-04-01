using System.Collections;
using UnityEngine;

public class TheClown : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;

   public GameObject Back;

   public KMNeedyModule Needy;

   public KMSelectable Nose;

   public Material[] Clowns;

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

   int weed;

   void Awake () {
      ModuleId = ModuleIdCounter++;
      Needy.OnNeedyActivation += OnNeedyActivation;

      Nose.OnInteract += delegate () { NoseP(); return false; };

   }

   void Start () {
      
   }

   protected void OnNeedyActivation () {
      weed++;
   }

   void NoseP () {
      Back.GetComponent<MeshRenderer>().material = Clowns[1];
      StartCoroutine(Strike());
      StartCoroutine(Loud());
   }

   IEnumerator Strike () {
      while (true) {
         GetComponent<KMNeedyModule>().HandleStrike();
         Audio.PlaySoundAtTransform("funnt", transform);
         yield return new WaitForSeconds(.3f);
      }
   }

   IEnumerator Loud () {
      while (true) {
         Audio.PlaySoundAtTransform("funnt", transform);
         yield return new WaitForSeconds(1f);
      }
   }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"Use !{0} honk.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      if (Command == "honk") {
         Nose.OnInteract();
         yield return null;
      }
      else {
         yield return "sendtochaterror It's one command dumbass.";
      }
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return ProcessTwitchCommand("honk");
   }
}
