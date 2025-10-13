using UnityEngine;

public class UnidadAliada : MonoBehaviour
{
    [SerializeField] protected int vida;
    [SerializeField] protected int ataque;
    [SerializeField] protected float velocidadAtaque;
    [SerializeField] protected float velocidadMovimiento;

    private float tiempoUltimoAtaque;
    public bool conVida => vida > 0;
    protected UnidadEnemiga enemigoEnRango;

    public virtual void RecibirDanio(int danio) {
        vida -= danio;
        if (vida <= 0) {
            Morir();
        }
           
    }

    protected virtual void Morir() {
        Destroy(gameObject);
    }

    protected void Atacar(UnidadEnemiga objetivo) {
        if (!conVida || objetivo == null || !objetivo.conVida) return;

        if (Time.time - tiempoUltimoAtaque >= velocidadAtaque) {
            objetivo.RecibirDanio(ataque);
            tiempoUltimoAtaque = Time.time;
        }
    }

    //Metodo para encontrar a quien atacar
    protected void enemigoObjetivo(UnidadEnemiga objetivo) {
        if (objetivo != null) {

            // Ir hacia el enemigo
            float distancia = Vector3.Distance(transform.position, objetivo.transform.position);

            if (distancia > 0.5f) {
                transform.position = Vector3.MoveTowards(
                    transform.position, // pos actual
                    objetivo.transform.position, // pos objetivo
                    2f * Time.deltaTime // velocidad de movimiento
                );
            } else // si está cerca, atacar
                {
                    Atacar(objetivo);
                }
        }
    }
}
