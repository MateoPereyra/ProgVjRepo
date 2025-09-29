using UnityEngine;

[CreateAssetMenu(fileName = "JuegoData", menuName = "Scriptable Objects/JuegoData")]
public class JuegoData : ScriptableObject {

    [SerializeField] [Range(1, 1000)] private int numeroHorda;
    public int NumeroHorda { get => numeroHorda; set => numeroHorda = value; }

}
