using UnityEngine;

public class ReclutaController : MonoBehaviour
{

    [SerializeField] private ArmeriaManager armeriamanager;
    [SerializeField] private Transform destino;
    [SerializeField] private GameObject espadachinPrefab;

    private Vector3 puntoDestino;
    private Vector3 puntoOrigen;
    private int vida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        armeriamanager = FindFirstObjectByType<ArmeriaManager>();
        destino = GameObject.Find("Armeria").transform;

        puntoDestino = (destino.position - transform.position).normalized;
        puntoOrigen = (transform.position - destino.position).normalized; //por ahora, no se usa

        vida = 100;

    }

    // Update is called once per frame
    void Update() {
        
        if (armeriamanager.getArmas()) {
            transform.Translate(puntoDestino * Time.deltaTime);
        }

    }

    //Para cuando llega a la armería
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.GetComponent<ArmeriaManager>() != null) {
            ConvertirAEspadachin();
        }

    }

    //Convierte al recluta en una nueva unidad
    private void ConvertirAEspadachin() {
        
        Vector3 pos = transform.position;
        Destroy(gameObject); // destruye al recluta para convertirlo en otra unidad (Espadachin)
        GameObject Espadachin = Instantiate(espadachinPrefab);
        espadachinPrefab.transform.position = pos;

    }

    public void recibirDanio(int daño) {

        vida -= daño;
        if (vida <=0) {
            Destroy(gameObject);
        }

    }
}
