using UnityEngine;

namespace FlappyBird
{
    [RequireComponent(typeof(Rigidbody2D))]
    //Animation
    public class Player : MonoBehaviour
    {
        [SerializeField] Rigidbody2D birdPhysics;
        [SerializeField] float upForce = 6;
        [SerializeField] float maxHeight = 4;
        [SerializeField] PhysicsMaterial2D bounce;
        //Animation
        void Start()
        {
            birdPhysics = GetComponent<Rigidbody2D>();
            //set gravity to 0 when you have  the game loop
            birdPhysics.gravityScale = 0;
        }
        void Flap()
        {
            if (GameManager.instance.currentGameState != GameState.PostGame && this.transform.position.y < maxHeight)
            {
                if (GameManager.instance.currentGameState == GameState.PreGame)
                {
                    birdPhysics.gravityScale = 1;
                    GameManager.instance.currentGameState = GameState.Game;
                }
                //stop gravities velocity
                birdPhysics.linearVelocity = Vector2.zero;
                //apply up force
                birdPhysics.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
                //play flap animation
            }

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                //control game state
                Flap();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Damage")
            {
                if (GameManager.instance.currentGameState == GameState.Game)
                {
                    GameManager.instance.currentGameState = GameState.PostGame;
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Score")
            {
                GameManager.instance.score++;
                GameManager.instance.UpdatingUI();
            }
        }
    }
}