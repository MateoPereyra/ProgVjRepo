using UnityEngine;

[CreateAssetMenu(fileName = "JuegoData", menuName = "Scriptable Objects/JuegoData")]
public class JuegoData : ScriptableObject {

    [SerializeField] [Range(1, 1000)] private int numeroHorda;
    public int NumeroHorda { get => numeroHorda; set => numeroHorda = value; }

    ///////Usados en ArmeriaManager
    [SerializeField][Range(10, 2000)] private int costoOre;
    public int CostoOre { get => costoOre; set => costoOre = value; }

    [SerializeField][Range(150, 2000)] private int costoRecluta;
    public int CostoRecluta { get => costoRecluta; set => costoRecluta = value; }

    ///////Para modificar las recompensas de rondas
    [SerializeField][Range(1, 2000)] private int escalaRecompensas;
    public int EscalaRecompensas { get => escalaRecompensas; set => escalaRecompensas = value; }

    private int enemigosDerrotados;
    public int EnemigosDerrotados { get => enemigosDerrotados; set => enemigosDerrotados = value; }

}
