using UnityEngine;
using TMPro;

public class VencedorChegada : MonoBehaviour
{
    public TextMeshProUGUI vencedorText;
    private static bool vencedorDefinido = false;
    public static string vencedor = "";
    private static float menorTempo = float.MaxValue;
    public static int pistaVencedor = 0;

    public GameObject atletaCorrida;
    public GameObject atletaSalto;
    public GameObject terceiroAtleta; 
    public CameraGerenciador cameraGerenciador;

    public static void VerificarVencedor()
    {
        if (vencedorDefinido) return;

        string[] competidores = { "Sergio", "Galego", "Mozart", "Jeyniglis", "Ruan", "Trump" };
        float[] tempos = { AtletaChegada.tempoAtleta, CompetidorChegada.tempoCompetidor, AI2Chegada.tempoAI2, AI3Chegada.tempoAI3, AI4Chegada.tempoAI4, AI5Chegada.tempoAI5 };

        for (int i = 0; i < competidores.Length; i++)
        {
            if (tempos[i] < menorTempo)
            {
                menorTempo = tempos[i];
                vencedor = competidores[i];
                pistaVencedor = i + 1;
            }
        }

        Debug.Log($"Vencedor: {vencedor} na pista {pistaVencedor} com {menorTempo:F2} segundos!");

        VencedorChegada instancia = FindObjectOfType<VencedorChegada>();
        if (instancia != null)
        {
            instancia.AtivarAtletaSalto();
            instancia.TrocarCameraParaSalto();
        }

        vencedorDefinido = true;

        if (instancia != null && instancia.vencedorText != null)
        {
            instancia.vencedorText.gameObject.SetActive(true);
            instancia.vencedorText.SetText($"Vencedor: {vencedor} na pista {pistaVencedor}\nTempo: {menorTempo:F2} segundos");
            instancia.vencedorText.ForceMeshUpdate();
        }
    }

    public void AtivarAtletaSalto()
    {
        if (atletaCorrida != null)
        {
            atletaCorrida.SetActive(false);
        }

        if (atletaSalto != null)
        {
            atletaSalto.SetActive(true);
        }

        Debug.Log("Atleta do salto ativado! Controle agora é dele.");
    }

    public void TrocarCameraParaSalto()
    {
        if (cameraGerenciador != null)
        {
            cameraGerenciador.AlternarParaAtletaSalto();
        }
        else
        {
            Debug.LogError("O script CameraGerenciador não está atribuído!");
        }
    }

    public void AtivarAtletaSaltoBarra()
    {
        Debug.Log("Chamando AtivarAtletaSaltoBarra!");

        if (atletaSalto != null)
        {
            atletaSalto.SetActive(false);
            Debug.Log("Segundo atleta removido!");
        }

        if (terceiroAtleta != null)
        {
            terceiroAtleta.SetActive(true);
            Debug.Log("Terceiro atleta ativado! Controle agora é dele.");
            TrocarCameraParaSaltoBarra();
        }
        else
        {
            Debug.LogError("Erro: terceiro atleta não está atribuído corretamente!");
        }
    }


    public void TrocarCameraParaSaltoBarra()
    {
        if (cameraGerenciador != null)
        {
            Debug.Log("Alternando para a câmera do terceiro atleta...");
            cameraGerenciador.AlternarParaAtletaSaltoBarra(); 
        }
        else
        {
            Debug.LogError("Erro: `cameraGerenciador` não está atribuído corretamente!");
        }
    }
}
