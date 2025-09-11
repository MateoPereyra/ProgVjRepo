using UnityEngine;

public class ReclutaController : MonoBehaviour
{

    [SerializeField] private ArmeriaManager armeriamanager;
    [SerializeField] private Transform destino;

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
}
