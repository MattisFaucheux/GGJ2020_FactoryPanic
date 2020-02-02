using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public GameObject soundMenu;

   private void Start()
   {
      soundMenu.SetActive(false);
   }

   public void LoadGame()
   {
      SceneManager.LoadScene("GameScene");
   }

   public void OpenSoundSettings()
   {
      soundMenu.SetActive(true);
   }
   
   public void CloseSoundSettings()
   {
      soundMenu.SetActive(false);
   }
   public void ExitGame()
   {
      Application.Quit();
   }
}
