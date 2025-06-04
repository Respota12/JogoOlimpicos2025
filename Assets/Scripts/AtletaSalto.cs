using UnityEngine;
using TMPro;

public class AtletaSaltoMovimentacao : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    public float velocidadeCorrida = 6f;
    public float velocidadeCaminhada = 3f;
    public float gravidade = -9.81f;
    private float velocidadeAtual = 0f;
    private float suavizacaoVelocidade = 5f;

    public float tempoDeEspera = 3f;
    private bool podeMover = false;

    public float forcaPulo = 7f;
    private bool podePular = false;

    public GameObject plataformaPulo;
    public GameObject zonaAreia;

    public TextMeshProUGUI distanciaPuloText; 
    private Vector3 posicaoInicialPulo; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity.y = -2f;

        animator.SetBool("Mover", false);
        animator.SetBool("Jump", false);

        Invoke("LiberarMovimentacao", tempoDeEspera);
    }

    void LiberarMovimentacao()
    {
        podeMover = true;
        animator.SetBool("Mover", true);
        Debug.Log("Segundo atleta começou a se movimentar.");
    }

    void Update()
    {
        if (!podeMover) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? velocidadeCorrida : velocidadeCaminhada;
        velocidadeAtual = Mathf.Lerp(velocidadeAtual, targetSpeed, Time.deltaTime * suavizacaoVelocidade);

        if (moveDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 10f);
        }

        controller.Move(moveDirection * Time.deltaTime * velocidadeAtual);
        animator.SetBool("Mover", moveDirection.magnitude > 0.1f);

        
        if (podePular && Input.GetKeyDown(KeyCode.Space))
        {
            posicaoInicialPulo = transform.position; 
            velocity.y = forcaPulo;
            animator.SetBool("Jump", true);
            Debug.Log("Pulo ativado!");
        }

        
        velocity.y += gravidade * Time.deltaTime;
        controller.Move(new Vector3(0, velocity.y * Time.deltaTime, 0));

        if (controller.isGrounded)
        {
            velocity.y = -2f;
            animator.SetBool("Jump", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Colisão detectada: {other.gameObject.name}");

        if (zonaAreia != null && other.gameObject == zonaAreia)
        {
            Debug.Log("Segundo atleta tocou a areia! Calculando a distância do pulo.");

            if (posicaoInicialPulo != Vector3.zero)
            {
                float distanciaPulo = Vector3.Distance(posicaoInicialPulo, transform.position);
                Debug.Log($"Distância do pulo: {distanciaPulo:F2} metros");

                //  Exibe a distância na UI
                if (distanciaPuloText != null)
                {
                    distanciaPuloText.text = $"Distância do Pulo: {distanciaPulo:F2}m";
                }
            }

            //  Chamando VencedorChegada para ativar o terceiro atleta
            VencedorChegada instancia = FindObjectOfType<VencedorChegada>();
            if (instancia != null)
            {
                Debug.Log("Chamando AtivarAtletaSaltoBarra() dentro de VencedorChegada.");
                instancia.AtivarAtletaSaltoBarra();
            }
            else
            {
                Debug.LogError("Erro: `VencedorChegada` não encontrado na cena!");
            }
        }

        
        if (other.gameObject == plataformaPulo)
        {
            podePular = true;
            Debug.Log("Atleta pode pular agora!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == plataformaPulo)
        {
            podePular = false;
            Debug.Log("Atleta não pode mais pular!");
        }
    }
}
