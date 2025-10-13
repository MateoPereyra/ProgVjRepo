using UnityEngine;

public class Espadachin : UnidadAliada
{

    void Start() {
        //vida = 150;
        //ataque = 35;
        //velocidadAtaque = 1.75f;
        //velocidadMovimiento = 2;
    }

    void Update() {

        //if (enemigoEnRango != null && enemigoEnRango.conVida) {
        //    Atacar(enemigoEnRango); //ataca hasta que el enemigo ya no se encuentre en rango
        //}

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
