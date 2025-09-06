using UnityEngine;
using TMPro;

public class ArmeriaManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TextMeshProUGUI textoOre;
    [SerializeField] private TextMeshProUGUI textMonedas;
    [SerializeField] private GameObject panelArmeria;

    [SerializeField] [Range(10, 2000)] private int monedas;

    private int cantidadOre;

    void Start() {

        cantidadOre = 0;
        textMonedas.text = monedas.ToString();

    }

    public void comprarOre() {

        cantidadOre++;
        textoOre.text = "x " + cantidadOre;

    }

    public void OnMouseDown() {

        if (panelArmeria.activeSelf) {
            panelArmeria.SetActive(false);
        } else { 
        panelArmeria.SetActive(true);
        }

    }
}
