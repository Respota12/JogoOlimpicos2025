using UnityEngine;
using TMPro;

public class CompetidorChegada : MonoBehaviour
{
    public TextMeshProUGUI tempoCompetidorText;
    public static float tempoCompetidor = 0f;
    private bool competidorFinalizou = false;
    private bool corridaIniciada = false;

    void Start()
    {
        Invoke(nameof(IniciarCorrida), 3f);
    }

    void Update()
    {
        if (corridaIniciada && !competidorFinalizou)
        {
            tempoCompetidor += Time.deltaTime;
            tempoCompetidorText.SetText($" Tempo de Galego: {tempoCompetidor:F2} s");
        }
    }

    void IniciarCorrida()
    {
        corridaIniciada = true;
        Debug.Log(" Corrida iniciada para o Galego!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Competidor>())
        {
            competidorFinalizou = true;
            FinalizarCorrida();
        }
    }

    void FinalizarCorrida()
    {
        Debug.Log($" Galego finalizou em {tempoCompetidor:F2} segundos!");
        VencedorChegada.VerificarVencedor();
    }
}
