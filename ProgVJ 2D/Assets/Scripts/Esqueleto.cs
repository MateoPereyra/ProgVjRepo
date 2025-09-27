using UnityEngine;

public class Esqueleto : UnidadEnemiga
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //vida = 75;
        //ataque = 25;
        //velocidadAtaque = 1.75f;
    }

    // Update is called once per frame
    void Update() {

        if (enemigoEnRango != null && enemigoEnRango.conVida) {
            Atacar(enemigoEnRango); //ataca hasta que el enemigo ya no se encuentre en rango
        }

        if (armeriaEnRango != null) {
            AtacarArmeria();
        }

    }

    //Usado para pelear
    private void OnTriggerEnter2D(Collider2D other) {

        UnidadAliada u = other.GetComponent<UnidadAliada>();
        if (u != null) {
            enemigoEnRango = u;
        }

        ArmeriaManager a = other.GetComponent<ArmeriaManager>();
        if (a != null) {
            armeriaEnRango = a;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        UnidadAliada u = other.GetComponent<UnidadAliada>();
        if (u != null && u == enemigoEnRango) {
            enemigoEnRango = null;
        }

        ArmeriaManager a = other.GetComponent<ArmeriaManager>();
        if (a != null && a == armeriaEnRango) {
            armeriaEnRango = null;
        }

    }
}
