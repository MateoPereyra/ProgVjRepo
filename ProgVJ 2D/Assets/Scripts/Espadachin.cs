using UnityEngine;

public class Espadachin : UnidadAliada
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //vida = 150;
        //ataque = 35;
        //velocidadAtaque = 1.75f;
        //velocidadMovimiento = 2;
    }

    // Update is called once per frame
    void Update() {

        if (enemigoEnRango != null && enemigoEnRango.conVida) {
            Atacar(enemigoEnRango); //ataca hasta que el enemigo ya no se encuentre en rango
        }

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
