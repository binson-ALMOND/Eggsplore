using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   public static ScoreManager Instance;
   public TextMeshProUGUI scoreText;
   public GameObject winPanel;
   public GameObject winBlurVolume;
   public int targetEggs = 1;
   private int currentEggs = 0;
   
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
      Time.timeScale = 1f;
      if(winPanel != null) winPanel.SetActive(false);
      if(winBlurVolume != null) winBlurVolume.SetActive(false);
      UpdateUI();
   }

   public void AddPoint()
   {
      currentEggs++;
      UpdateUI();
      if (currentEggs >= targetEggs)
      {
         WinGame();
      }
   }

   void UpdateUI()
   {
      if (scoreText != null)
      {
         scoreText.text = "Eggs: " + currentEggs;
      }
   }

   void WinGame()
   {
      if(winPanel != null) winPanel.SetActive(true);
      if(winBlurVolume != null) winBlurVolume.SetActive(true);
      
      Time.timeScale = 0f;
   }
   public void RestartGame()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene("GameScene");
   }
   public void GoToMainMenu()
   {
      SceneManager.LoadScene("MainMenu");
   }
}
