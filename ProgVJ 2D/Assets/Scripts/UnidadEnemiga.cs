using UnityEngine;

public class UnidadEnemiga : MonoBehaviour
{
    [SerializeField] protected int vida;
    [SerializeField] protected int ataque;
    [SerializeField] protected float velocidadAtaque;

    private float tiempoUltimoAtaque;
    public bool conVida => vida > 0;
    protected UnidadAliada enemigoEnRango;
    protected ArmeriaManager armeriaEnRango;

    public virtual void RecibirDanio(int danio) {
        vida -= danio;
        if (vida <= 0) {
            Morir();
        }

    }

    protected virtual void Morir() {
        Destroy(gameObject);
    }

    protected void Atacar(UnidadAliada objetivo) {
        if (!conVida || objetivo == null || !objetivo.conVida) return;

        if (Time.time - tiempoUltimoAtaque >= velocidadAtaque) {
            objetivo.RecibirDanio(ataque);
            tiempoUltimoAtaque = Time.time;
        }
    }

    protected virtual void AtacarArmeria() {

        if (Time.time - tiempoUltimoAtaque >= velocidadAtaque) {
            armeriaEnRango.RecibirDanio(ataque);
            tiempoUltimoAtaque = Time.time;
        }
    }
}
