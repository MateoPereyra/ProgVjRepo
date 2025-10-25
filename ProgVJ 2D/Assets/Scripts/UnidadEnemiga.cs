using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    protected SPUM_Prefabs spum;
    protected PlayerState _currentState;
    protected PlayerState _ultimoEstado;
    protected Dictionary<PlayerState, int> IndexPair = new ();

    [SerializeField] private AudioClip muerte;
    [SerializeField] private AudioClip sonidoAtaque;
    private AudioSource audioSource;

    private void Awake()
    {

        if (spum == null)
        {
            spum = GetComponentInChildren<SPUM_Prefabs>();
        }

        if (!spum.allListsHaveItemsExist()) {
              spum.PopulateAnimationLists();
          }

        spum.OverrideControllerInit();
        foreach (PlayerState state in Enum.GetValues(typeof(PlayerState))) {
            IndexPair[state] = 0;
        }


    }

    private void OnEnable() {
        // Cada vez que el enemigo se activa restablece su vida
        vida = vidaMax;

        audioSource = GetComponent<AudioSource>();

        _currentState = PlayerState.MOVE;
        PlayStateAnimation(_currentState);
    }

    public virtual void RecibirDanio(int danio) {
        vida -= danio;
        if (vida <= 0) {
            _currentState = PlayerState.DEATH;
            PlayStateAnimation(_currentState);
            audioSource.PlayOneShot(muerte);

            Invoke(nameof(Morir), 1f);
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

            _currentState = PlayerState.ATTACK;
            PlayStateAnimation(_currentState);
            audioSource.PlayOneShot(sonidoAtaque);
        }

    }
    

    protected virtual void AtacarArmeria() {

        if (Time.time - tiempoUltimoAtaque >= velocidadAtaque) {
            armeriaEnRango.RecibirDanio(ataque);
            tiempoUltimoAtaque = Time.time;

            _currentState = PlayerState.ATTACK;
            PlayStateAnimation(_currentState);
            audioSource.PlayOneShot(sonidoAtaque);
        }

    }

    protected virtual void SetVida() {

        vida = vidaMax;    
    
    }

    protected virtual void PlayStateAnimation(PlayerState state) { 
        spum.PlayAnimation(state, IndexPair[state]);
    }
}
