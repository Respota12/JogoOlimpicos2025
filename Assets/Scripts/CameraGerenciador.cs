using UnityEngine;
using Cinemachine;

public class CameraGerenciador : MonoBehaviour
{
    public CinemachineVirtualCamera cameraCorrida;
    public CinemachineVirtualCamera cameraSalto;
    public CinemachineVirtualCamera cameraSaltoBarra;

    void Start()
    {
        if (cameraCorrida == null || cameraSalto == null || cameraSaltoBarra == null)
        {
            Debug.LogError("Erro: Algumas câmeras Cinemachine não foram atribuídas corretamente!");
        }
    }

    public void AlternarParaAtletaSalto()
    {
        if (cameraCorrida != null && cameraSalto != null)
        {
            Debug.Log("Alternando câmera: Corrida para Salto");

            cameraCorrida.Priority = 10;
            cameraSalto.Priority = 40;
            cameraSaltoBarra.Priority = 0;
        }
        else
        {
            Debug.LogError("Erro ao alternar para a câmera do salto!");
        }
    }

    public void AlternarParaAtletaSaltoBarra()
    {
        if (cameraSaltoBarra != null)
        {
            Debug.Log("Alternando câmera para o terceiro atleta...");

            cameraCorrida.Priority = 0;
            cameraSalto.Priority = 10;
            cameraSaltoBarra.Priority = 50;

            Debug.Log($"Nova prioridade da câmera de salto em barra: {cameraSaltoBarra.Priority}");
        }
        else
        {
            Debug.LogError("Erro ao alternar para a câmera do terceiro atleta! Verifique no Inspector.");
        }
    }

}
