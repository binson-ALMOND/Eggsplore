using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   public static ScoreManager Instance;
   public TextMeshProUGUI scoreText;
   private int score = 0;

   void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   void Start()
   {
      UpdateUI();
   }

   public void AddPoint()
   {
      score++;
      UpdateUI();
   }

   void UpdateUI()
   {
      if (scoreText != null)
      {
         scoreText.text = "Eggs: " + score;
      }
   }
}
