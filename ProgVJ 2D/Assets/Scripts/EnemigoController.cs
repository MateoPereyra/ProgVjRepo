using UnityEngine;

public class EnemigoController : MonoBehaviour
{

    [SerializeField] private ArmeriaManager armeriamanager;
    [SerializeField] private Transform destino;

    private Vector3 puntoDestino;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        armeriamanager = FindFirstObjectByType<ArmeriaManager>();
        destino = GameObject.Find("Armeria").transform;

        puntoDestino = (destino.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update() {

        transform.Translate(puntoDestino * Time.deltaTime);

    }
}
