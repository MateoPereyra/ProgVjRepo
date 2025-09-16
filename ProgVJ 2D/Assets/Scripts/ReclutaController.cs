using UnityEngine;

public class ReclutaController : UnidadAliada
{

    [SerializeField] private ArmeriaManager armeriamanager;
    [SerializeField] private Transform destino;
    [SerializeField] private GameObject espadachinPrefab;

    private Vector3 puntoDestino;
    private Vector3 puntoOrigen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        armeriamanager = FindFirstObjectByType<ArmeriaManager>();
        destino = GameObject.Find("Armeria").transform;

        puntoDestino = (destino.position - transform.position).normalized;
        puntoOrigen = (transform.position - destino.position).normalized; //por ahora, no se usa

        vida = 100;
        ataque = 15;
        velocidadAtaque = 2.0f;
        velocidadMovimiento = 1;
    }

    // Update is called once per frame
    void Update() {
        
        if (armeriamanager.getArmas()) {
            transform.Translate(puntoDestino * Time.deltaTime);
        }

        if (enemigoEnRango != null && enemigoEnRango.conVida) {
            Atacar(enemigoEnRango); //ataca hasta que el enemigo ya no se encuentre en rango
        }

    }

    //Para cuando llega a la armería y para pelear
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.GetComponent<ArmeriaManager>() != null) {
            ConvertirAEspadachin();
        }

        UnidadEnemiga u = other.GetComponent<UnidadEnemiga>();
        if (u != null) {
            enemigoEnRango = u;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {

        UnidadEnemiga u = other.GetComponent<UnidadEnemiga>();
        if (u != null && u == enemigoEnRango){
            enemigoEnRango = null;
        }
    }

    //Convierte al recluta en una nueva unidad
    private void ConvertirAEspadachin() {
        
        Vector3 pos = transform.position;
        Destroy(gameObject); // destruye al recluta para convertirlo en otra unidad (Espadachin)
        GameObject Espadachin = Instantiate(espadachinPrefab);
        espadachinPrefab.transform.position = pos;

    }
}
