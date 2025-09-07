using UnityEngine;
using TMPro;

public class ArmeriaManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TextMeshProUGUI textoOre;
    [SerializeField] private TextMeshProUGUI textMonedas;
    [SerializeField] private TextMeshProUGUI textArmas;
    [SerializeField] private GameObject panelArmeria;

    [SerializeField] [Range(10, 2000)] private int monedas;

    private int cantidadOre;
    private int cantidadArmas;

    void Start() {

        cantidadOre = 0;
        cantidadArmas = 0;
        textMonedas.text = monedas.ToString();

        InvokeRepeating("crearArma", 0, 6);

    }

    //Metodo para comprar recursos
    public void comprarOre() {

        cantidadOre++;
        textoOre.text = "x " + cantidadOre;

    }

    //Metodo para esconder/activar el panel
    public void OnMouseDown() {

        if (panelArmeria.activeSelf) {
            panelArmeria.SetActive(false);
        } else { 
        panelArmeria.SetActive(true);
        }

    }

    //Metodo para generar armas cada X segundos si hay recursos suficientes
    public void crearArma() {

        if (cantidadOre > 0) {
            cantidadOre--;
            cantidadArmas++;
            textArmas.text = "x " + cantidadArmas;
            textoOre.text = "x " + cantidadOre;
        }

    }

}
