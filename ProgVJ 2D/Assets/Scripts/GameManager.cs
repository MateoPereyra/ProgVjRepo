using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar escenas en el futuro
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia { get; private set; }

    [SerializeField] private JuegoData juegoData;
    public JuegoData JuegoData {  get => juegoData; }

    [SerializeField] private ArmeriaManager armeriamanager;
    int recompensas;

    //Manejo de enemigos
    private List<UnidadEnemiga> enemigosVivos; //Para el sistema de progresion en hordas///
    private float posX, posY; //Posiciones aleatorias para spawnear enemigos

    [SerializeField] private int poolSize = 50; //tamaño del pool de enemigos///
    private int cantidad; //Cantidad aleatoria de enemigos a generar en cada horda

    //Manejo de hordas
    [SerializeField][Range(0, 1000)] private int numeroHorda;
    [SerializeField][Range(1, 1000)] private int cdHordas;
    private Coroutine hordaCoroutine;


    ////////////////////////// Enemigos //////////////////////////
    [SerializeField] private GameObject esqueletoPrefab; //enemigo principiante (unico por ahora)


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

        enemigosVivos = new List<UnidadEnemiga>();
        for (int i = 0; i < poolSize; i++) {
            InstanciarEnemigo();
        }
        NuevaHorda();

    }


    /////////// Sistema de Progresion ///////////////

    private void NuevaHorda() {
        
        if (hordaCoroutine == null)
            hordaCoroutine = StartCoroutine(ControlarHorda());
    }

    private IEnumerator ControlarHorda() {

        numeroHorda++;
        Victoria();

        // Calcular cantidad aleatoria de enemigos
        cantidad = Random.Range(numeroHorda, numeroHorda + numeroHorda + 1);

        // Esperar X segundos antes de spawnear
        yield return new WaitForSeconds(cdHordas);

        // Spawnear enemigos
        for (int i = 0; i < cantidad; i++) {
            PrepararEnemigo();
            yield return null; // Pausa por frame
        }



        // Esperar hasta que no queden enemigos
        yield return new WaitUntil(() => TodosLosEnemigosInactivos());///

        // Reiniciar corrutina
        hordaCoroutine = null;
        NuevaHorda();
    }

    private void PrepararEnemigo() {
        UnidadEnemiga pooledEnemy = GetPooledEnemy();
        if (pooledEnemy != null) {
            posX = Random.Range(0f, 5f);
            posY = Random.Range(0f, 5f);
            pooledEnemy.transform.position = new Vector3(posX, posY, 0);
            pooledEnemy.transform.rotation = Quaternion.identity;
            pooledEnemy.gameObject.SetActive(true);
        }

    }

    private void InstanciarEnemigo()
    {

        GameObject clon = Instantiate(esqueletoPrefab);
        UnidadEnemiga enemigo = clon.GetComponent<UnidadEnemiga>();
        enemigo.gameObject.SetActive(false);
        AgregarEnemigo(enemigo);

    }


    public void AgregarEnemigo(UnidadEnemiga enemigo)
    {
        enemigosVivos.Add(enemigo);
    }

    public void EliminarEnemigo(UnidadEnemiga enemigo) {
        enemigo.gameObject.SetActive(false);
        

        if (TodosLosEnemigosInactivos())
        {
            recompensas = CalcularRecompensa();
            armeriamanager.Recompensas(recompensas);
            NuevaHorda();
        }
    }


    private UnidadEnemiga GetPooledEnemy()
    {
        foreach (UnidadEnemiga enemigo in enemigosVivos)
        {
            if (!enemigo.gameObject.activeInHierarchy)
            {
                return enemigo;
            }
        }

        return null;
    }

    private bool TodosLosEnemigosInactivos() {
    foreach (UnidadEnemiga enemigo in enemigosVivos)
        if (enemigo.gameObject.activeInHierarchy)
            return false;
    return true;
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

    private int CalcularRecompensa() {
        return 100 + (numeroHorda * 25);
    }
}
