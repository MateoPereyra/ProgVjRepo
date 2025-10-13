using System.Collections.Generic;
using UnityEngine;

public class ZonaAldea : MonoBehaviour
{

    public static ZonaAldea instancia { get; private set; }
    private List<UnidadEnemiga> enemigosEnZona = new List<UnidadEnemiga>();

    private void Awake() {
        instancia = this;
    }


    // Para detectar cuando un enemigo entra a la zona de la aldea y llevar registro
    private void OnTriggerEnter2D(Collider2D other)
    {
        UnidadEnemiga enemigo = other.GetComponent<UnidadEnemiga>();
        if (enemigo != null && !enemigosEnZona.Contains(enemigo))
        {
            enemigosEnZona.Add(enemigo);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UnidadEnemiga enemigo = other.GetComponent<UnidadEnemiga>();
        if (enemigo != null && enemigosEnZona.Contains(enemigo))
        {
            enemigosEnZona.Remove(enemigo);
        }
    }


    //Consigue el enemigo mas cercano para ser atacado
    public UnidadEnemiga GetEnemigoCercano(Vector3 posicionAliado)  {
        UnidadEnemiga masCercano = null;
        float distanciaMin = float.MaxValue; //distancia al enemigo mas cercano, iniciada en valor maximo para considerar cada enemigo

        foreach (UnidadEnemiga enemigo in enemigosEnZona) {
            if (enemigo == null) continue; //en caso de que alguno se haya destruido y no eliminado de la lista
            float distancia = Vector3.Distance(posicionAliado, enemigo.transform.position);
            if (distancia < distanciaMin) {
                distanciaMin = distancia;
                masCercano = enemigo;
            }
        }

        return masCercano;
    }
}
