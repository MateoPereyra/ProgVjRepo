using UnityEngine;

public class EnemigoController : MonoBehaviour
{

    [SerializeField] private ArmeriaManager armeriamanager;
    [SerializeField] private Transform destino;


    private Vector3 puntoDestino;
    private bool enAldea;
    private bool enPelea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        armeriamanager = FindFirstObjectByType<ArmeriaManager>();
        destino = GameObject.Find("Armeria").transform;
        puntoDestino = (destino.position - transform.position).normalized;

        enAldea = false;
        enPelea = false;
    }

    // Update is called once per frame
    void Update() {

        if (!enPelea) {
            transform.Translate(puntoDestino * Time.deltaTime);
        }
    }


    public bool GetEnemigoPos() { //aún sin uso

        return enAldea;

    }

    //Detecta cuando el enemigo entra a la zona de la aldea para ser atacado, y cuando esta peleando
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Aldea") {
            enAldea = true;
        } //todavia no tiene uso

        if (other.GetComponent<ReclutaController>() != null || other.GetComponent<ArmeriaManager>() != null || other.GetComponent<Espadachin>() != null)
        {

            enPelea = true;
            Vector3 pos = transform.position;
            transform.position = pos;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {

        if (other.GetComponent<ReclutaController>() != null || other.GetComponent<Espadachin>() != null) {
            enPelea = false;
        }
    }

}
