using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar escenas en el futuro
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia { get; private set; }
    [SerializeField] JuegoData juegoData;

    private List<UnidadEnemiga> enemigosVivos = new List<UnidadEnemiga>(); //Para el sistema de progresion en hordas
    private float posX, posY; //Posiciones aleatorias para spawnear enemigos
    private int cantidad; //Cantidad aleatoria de enemigos a generar en cada horda

    [SerializeField] private GameObject esqueletoPrefab;

    private void Awake() {
        if (instancia == null) {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {

        InstanciarEnemigo(new Vector3(10, 1, 0));
        InstanciarEnemigo(new Vector3(11, 2, 0));
        InstanciarEnemigo(new Vector3(12, 3, 0));
        Debug.Log("Horda: " + juegoData.NumeroHorda);
        GetNumEnemigos();

    }
    /////////// Sistema de Progresion ///////////////

    private void NuevaHorda()
    {

        juegoData.NumeroHorda++;
        Victoria();
        Debug.Log("Horda: " + juegoData.NumeroHorda + ". Más monstruos están por llegar...");

        cantidad = Random.Range(juegoData.NumeroHorda, juegoData.NumeroHorda + juegoData.NumeroHorda + 1);
        for (int i = 0; i < cantidad; i++) {
            Invoke("PrepararEnemigo", 10f);
        }

        GetNumEnemigos();

    }

    public void AgregarEnemigo(UnidadEnemiga enemigo) {
        enemigosVivos.Add(enemigo);
    }

    public void EliminarEnemigo(UnidadEnemiga enemigo) {
        enemigosVivos.Remove(enemigo);
        GetNumEnemigos();

        if (enemigosVivos.Count == 0) {
            NuevaHorda();
        }

    }

    //////// Metodos para Enemigos ////////
    
    private void PrepararEnemigo()
    {

        posX = Random.Range(0f, 5f);
        posY = Random.Range(0f, 5f);
        InstanciarEnemigo(new Vector3(posX, posY, 0));

    }

    private void InstanciarEnemigo(Vector3 posicion)
    {

        GameObject clon = Instantiate(esqueletoPrefab, posicion, Quaternion.identity);
        UnidadEnemiga enemigo = clon.GetComponent<UnidadEnemiga>();
        AgregarEnemigo(enemigo);

    }

    private void GetNumEnemigos() {
        Debug.Log("Enemigos restantes: " + enemigosVivos.Count);
    }

    
    ////////// Victoria/Derrota ////////////
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
