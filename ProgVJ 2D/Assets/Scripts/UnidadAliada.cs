using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UnidadAliada : MonoBehaviour
{
    [SerializeField] protected int vida;
    [SerializeField] protected int ataque;
    [SerializeField] protected float velocidadAtaque;
    [SerializeField] protected float velocidadMovimiento;

    //private SpriteRenderer spriteRenderer;

    private float tiempoUltimoAtaque;
    public bool conVida => vida > 0;
    protected UnidadEnemiga enemigoEnRango;

    protected SPUM_Prefabs spum;
    protected PlayerState _currentState;
    protected PlayerState _ultimoEstado;
    protected Dictionary<PlayerState, int> IndexPair = new();

    private void Awake()
    {
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spum == null)
        {
            spum = GetComponentInChildren<SPUM_Prefabs>();
        }

        if (!spum.allListsHaveItemsExist())
        {
            spum.PopulateAnimationLists();
        }

        spum.OverrideControllerInit();
        foreach (PlayerState state in Enum.GetValues(typeof(PlayerState)))
        {
            IndexPair[state] = 0;
        }


    }

    private void OnEnable() {

        _currentState = PlayerState.IDLE;
        PlayStateAnimation(_currentState);
    }

    public virtual void RecibirDanio(int danio) {
        vida -= danio;
        if (vida <= 0) {
            _currentState = PlayerState.DEATH;
            PlayStateAnimation(_currentState);
            Invoke(nameof(Morir), 1f);
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

            _currentState = PlayerState.ATTACK;
            PlayStateAnimation(_currentState);
        }
    }

    //Metodo para encontrar a quien atacar
    protected void enemigoObjetivo(UnidadEnemiga objetivo) {
        if (objetivo != null) {

            // Ir hacia el enemigo
            float distancia = Vector3.Distance(transform.position, objetivo.transform.position);

            if (distancia > 0.5f) {
                _currentState = PlayerState.MOVE;
                PlayStateAnimation(_currentState);

                transform.position = Vector3.MoveTowards(
                    transform.position, // pos actual
                    objetivo.transform.position, // pos objetivo
                    2f * Time.deltaTime // velocidad de movimiento
                );

                //Vector3 direccion = objetivo.transform.position - transform.position;
                //FlipSprite(direccion.x >= 0);
            } else // si está cerca, atacar
                {
                    Atacar(objetivo);
                }
        }
    }

    protected virtual void PlayStateAnimation(PlayerState state) { 
        spum.PlayAnimation(state, IndexPair[state]);
    }

    //private void FlipSprite(bool mirarDerecha) // Metodo para girar el sprite, ya que no encuentro el renderer para el flip del prefab de spum
    //{
    //    Vector3 scale = transform.localScale;
    //    scale.x = mirarDerecha ? 1f : -1f;
    //    transform.localScale = scale;
    //}

}
