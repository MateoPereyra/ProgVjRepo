using UnityEngine;

public class ReclutaController : MonoBehaviour
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
        puntoOrigen = (transform.position - destino.position).normalized;


    }

    // Update is called once per frame
    void Update() {
        
        if (armeriamanager.getArmas()) {
            transform.Translate(puntoDestino * Time.deltaTime);
        }

    }

    //Para cuando llega a la armería
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Armeria")) {
            ConvertirAEspadachin();
        }

    }

    //Convierte al recluta en una nueva unidad
    private void ConvertirAEspadachin() {
        
        Vector3 pos = transform.position;
        Destroy(gameObject); // destruye al recluta para convertirlo en otra unidad (Espadachin)
        Instantiate(espadachinPrefab, pos);

    }
}
