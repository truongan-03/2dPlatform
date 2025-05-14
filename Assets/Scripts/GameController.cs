using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Player Settings")]
    public Rigidbody2D playerRb;
    public Transform playerTransform;

    [Header("Checkpoint Settings")]
    private Vector2 checkpointPos;
    private bool hasCheckpoint = false;

    [Header("Effects")]
    public ParticleController particleController;

    private AudioManage audioManage;
    private GameOverManager gameOverManager;
    private GameWinManager gameWinManager;



    private void Awake()
    {
        if (playerRb == null)
        {
            playerRb = FindObjectOfType<Rigidbody2D>();
        }

        if (playerTransform == null)
        {
            playerTransform = playerRb.transform;
        }

        if (playerRb == null || playerTransform == null)
        {
            Debug.LogError("Player Rigidbody2D hoặc Transform chưa được gán!");
        }

        audioManage = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManage>();
        gameOverManager = FindObjectOfType<GameOverManager>();// Tìm script GameOverMenu
        gameWinManager = FindObjectOfType<GameWinManager>();

    }

    private void Start()
    {
        checkpointPos = playerTransform.position; // Lưu vị trí ban đầu
        hasCheckpoint = false; // Ban đầu chưa có checkpoint
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
        else if (collision.CompareTag("Goal"))
        {
            Win();
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
        hasCheckpoint = true;
    }

    private void Die()
    {
        if (particleController != null)
        {
            particleController.PlayParticle(ParticleController.Particles.die, playerTransform.position);
        }

        if (audioManage != null)
        {
            audioManage.PlaySFX(audioManage.death);
        }

        if (hasCheckpoint)
        {
            StartCoroutine(Respawn(0.5f));
        }
        else
        {
            playerRb.simulated = false;
            playerTransform.localScale = Vector3.zero;

            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }

    }

    private IEnumerator Respawn(float delay)
    {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        playerTransform.localScale = Vector3.zero;

        yield return new WaitForSeconds(delay);

        playerTransform.position = checkpointPos;
        playerTransform.rotation = Quaternion.identity;
        playerTransform.localScale = new Vector3(1.3665f, 1.4032f, 1f);

        playerRb.simulated = true;
    }

    private void Win()
    {
       
        if (gameWinManager != null)
        {
            gameWinManager.ShowGameWin();
        }

        playerRb.simulated = false;
        playerTransform.localScale = Vector3.zero;
    }
}
