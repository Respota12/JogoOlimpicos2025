using UnityEngine;
using TMPro;

public class AI4Chegada : MonoBehaviour
{
    public TextMeshProUGUI tempoAI4Text;
    public float tempoDeEspera = 3f; 
    public static float tempoAI4 = 0f;
    private bool AI4Finalizou = false;
    private bool corridaIniciada = false;

    void Start()
    {
        Invoke(nameof(IniciarCorrida), tempoDeEspera); 
    }

    void Update()
    {
        if (corridaIniciada && !AI4Finalizou)
        {
            tempoAI4 += Time.deltaTime;
            tempoAI4Text.SetText($" Tempo de Ruan {tempoAI4:F2} s");
        }
    }

    void IniciarCorrida()
    {
        corridaIniciada = true;
        Debug.Log(" Corrida iniciada para Ruan!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Competidor>()) 
        {
            AI4Finalizou = true;
            Debug.Log($" Ruan finalizou em {tempoAI4:F2} segundos!");
        }
    }
}
