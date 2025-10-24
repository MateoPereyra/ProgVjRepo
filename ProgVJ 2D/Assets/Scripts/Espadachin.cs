using UnityEngine;

public class Espadachin : UnidadAliada
{

    void Start() {
    }

    void Update() {


        // Si hay enemigos en la aldea, buscar al más cercano
        UnidadEnemiga objetivo = ZonaAldea.instancia.GetEnemigoCercano(transform.position);

        enemigoObjetivo(objetivo);

    }

    //Usado para pelear
    private void OnTriggerEnter2D(Collider2D other) {

        UnidadEnemiga u = other.GetComponent<UnidadEnemiga>();
        if (u != null) {
            enemigoEnRango = u;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {

        UnidadEnemiga u = other.GetComponent<UnidadEnemiga>();
        if (u != null && u == enemigoEnRango) {
            enemigoEnRango = null;
        }

    }
}
