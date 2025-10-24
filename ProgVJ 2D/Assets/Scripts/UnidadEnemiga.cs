using UnityEngine;

public class UnidadEnemiga : MonoBehaviour
{
    [SerializeField] protected int vidaMax;
    protected int vida;
    [SerializeField] protected int ataque;
    [SerializeField] protected float velocidadAtaque;

    private float tiempoUltimoAtaque;
    public bool conVida => vida > 0;
    protected UnidadAliada enemigoEnRango;
    protected ArmeriaManager armeriaEnRango;

    private SPUM_Prefabs spum;

    private void Awake()
    {
        spum = GetComponent<SPUM_Prefabs>();
        //spum.OverrideControllerInit(); //para usar las animaciones de defecto del prefab
    }

    private void OnEnable() {
        // Cada vez que el enemigo se activa restablece su vida
        vida = vidaMax;
    }

    public virtual void RecibirDanio(int danio) {
        vida -= danio;
        if (vida <= 0) {
            Morir();
        }

    }

    protected virtual void Morir() {
        GameManager.instancia.EliminarEnemigo(this);
    }

    protected void Atacar(UnidadAliada objetivo) {
        if (!conVida || objetivo == null || !objetivo.conVida) return;

        if (Time.time - tiempoUltimoAtaque >= velocidadAtaque) {
            objetivo.RecibirDanio(ataque);
            tiempoUltimoAtaque = Time.time;
        }

        if (spum._anim != null) {
            // Esto dispara la animación de ataque original
            spum._anim.SetTrigger("2_Attack");
            Debug.Log("Disparando animación de ataque original");
        }
        else {
            Debug.LogWarning("Animator no asignado en SPUM_Prefabs");
        }
    }
    

    protected virtual void AtacarArmeria() {

        if (Time.time - tiempoUltimoAtaque >= velocidadAtaque) {
            armeriaEnRango.RecibirDanio(ataque);
            tiempoUltimoAtaque = Time.time;
        }

        spum._anim.SetTrigger("0_Attack_Normal");
    }

    protected virtual void SetVida() {

        vida = vidaMax;    
    
    }
}
