using UnityEngine.SceneManagement;

public class ScenesLoader 
{
   public void ReloadGame()
   {
     var nameActiveScene = SceneManager.GetActiveScene().name;
     SceneManager.LoadSceneAsync(nameActiveScene);
   }
}
