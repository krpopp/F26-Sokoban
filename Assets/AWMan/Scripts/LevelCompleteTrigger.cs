using UnityEngine;

public class LevelProgressionManager : MonoBehaviour
{
   public GameObject[] completionImages;
   public GameObject[] levels;

   public ParticleSystem confetti;
   public float displayDuration = 4f;
   private int currentLevel = 1;
   //prevents another level from being completing for transitioning
   private bool transitioning = false;
   private float hideTime;

   public AudioSource celebrationAudio;
   public AudioClip celebrationSound;
   private void Start()
   {
      //hides all level completion images when game starts
      foreach (GameObject image in completionImages)
      {
         image.SetActive(false);
      }

      //actives first level, disables others
      for (int i = 0; i < levels.Length; i++)
      {
         levels[i].SetActive(i == 0);
      }
   }

   public void CompleteLevel(int levelNumber)
   {
      //stops function if level transition happened already
      if (transitioning) return;
      //makes sure completed level matches current level
      if (levelNumber != currentLevel) return;
      transitioning = true;
      completionImages[levelNumber - 1].SetActive(true);
      //plays confetti particle system
      confetti.Play();
      hideTime = Time.time + displayDuration;

      if (celebrationAudio != null && celebrationSound != null)
      {
         celebrationAudio.PlayOneShot(celebrationSound);
      }
   }

   private void Update()
   {
      //checks if transitioned happened and display time ended
      if (transitioning && Time.time >= hideTime)
      {
         //hides completion level for current level
         completionImages[currentLevel - 1].SetActive(false);
         //removes any reamaining particles
         confetti.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

         //checks if any more levels to play
         if (currentLevel < levels.Length)
         {
            //deactives level jst completed, moves onto next and then actives level 
            levels[currentLevel - 1].SetActive(false);
            currentLevel++;
            levels[currentLevel - 1].SetActive(true);

         }
         else
         {

         }
         //transition finished - allows next level to be completed
         transitioning = false;
      }
   }

}