using UnityEngine;

public class Esqueleto : UnidadEnemiga
{

    // Update is called once per frame
    void Update() {

        if (enemigoEnRango != null && enemigoEnRango.conVida)
            Atacar(enemigoEnRango);
        else if (armeriaEnRango != null)
            AtacarArmeria();
        else
            _currentState = PlayerState.MOVE; // si no hay objetivo, volver a moverse

        // Solo reproduce la animación si cambió el estado
        if (_currentState != _ultimoEstado) {
            PlayStateAnimation(_currentState);
            _ultimoEstado = _currentState;
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
