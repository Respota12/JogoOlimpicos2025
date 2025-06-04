using UnityEngine;
using UnityEngine.SceneManagement; 

public class AtletaSaltoBarra : MonoBehaviour
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

    public GameObject barra; 

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
        Debug.Log("Terceiro atleta começou a se movimentar.");
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = forcaPulo;
            animator.SetBool("Jump", true);
            Debug.Log("Pulo ativado!");
        }

        velocity.y += gravidade * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (velocity.y < 0)
        {
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter chamado! Colisão detectada com: {other.gameObject.name}");

        if (other.gameObject == barra) 
        {
            Debug.Log("Terceiro atleta tocou na barra! Reiniciando a cena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}
