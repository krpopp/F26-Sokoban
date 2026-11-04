using UnityEngine;

public class LevelProgressionManager : MonoBehaviour
{
   public GameObject[] completionImages;
   public GameObject[] levels;

   public ParticleSystem confetti;
   public float displayDuration = 4f;
   private int currentLevel = 1;
   private bool transitioning = false;
   private float hideTime;

   private void Start()
   {
      foreach (GameObject image in completionImages)
      {
         image.SetActive(false);
      }

      for (int i = 0; i < levels.Length; i++)
      {
         levels[i].SetActive(i == 0);
      }
   }

   public void CompleteLevel(int levelNumber)
   {
      if (transitioning) return;
      if (levelNumber != currentLevel) return;
      transitioning = true;
      completionImages[levelNumber - 1].SetActive(true);
      confetti.Play();
      hideTime = Time.time + displayDuration;
   }

   private void Update()
   {
      if (transitioning && Time.time >= hideTime)
      {
         completionImages[currentLevel - 1].SetActive(false);
         confetti.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

         if (currentLevel < levels.Length)
         {
            levels[currentLevel - 1].SetActive(false);
            currentLevel++;
            levels[currentLevel - 1].SetActive(true);

         }
         else
         {

         }

         transitioning = false;
      }
   }

}