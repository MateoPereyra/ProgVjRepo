using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar escenas en el futuro

public class GameManager : MonoBehaviour
{
    public static GameManager instancia; 
    private void Awake() {
        if (instancia == null) {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void GameOver() {
        Debug.Log("Los monstruos destruyeron la armeria... ¡Game Over!");
        // Por ahora: detener el tiempo
        Time.timeScale = 0f;

        // Más adelante: cargar una escena de Game Over
        // SceneManager.LoadScene("GameOver");
    }

    public void Victoria() {

        Debug.Log("Acabaste con todos los monstruos. ¡Has ganado!... por ahora");
    
    }
}