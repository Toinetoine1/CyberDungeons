using Photon.Pun;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        protected bool Friendly;

        protected Vector2 BulletDir;

        protected float Speed;
        protected int Damage;

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(Time.deltaTime * Speed * BulletDir);
            RaycastHit2D detect = Detection();
            if (detect.collider != null)
            {
                if (Friendly)
                {
                    if (detect.collider.CompareTag("Enemy"))
                    {
                        GameObject enemy = detect.collider.gameObject;
                        enemy.GetComponent<Health>().takeDamageRPC(Damage);
                        PhotonNetwork.Destroy(gameObject);
                    }
                }
                else
                {
                    if (detect.collider.CompareTag("Player"))
                    {
                        GameObject player = detect.collider.gameObject;
                        if (player.GetComponent<PlayerMovement>().mouvementState == PlayerMovement.State.Walking)
                        {
                            player.GetComponent<Health>().takeDamageRPC(Damage);
                            if (gameObject.GetComponent<PhotonView>().IsMine)
                            {
                                Debug.Log("hit player");
                                PhotonNetwork.Destroy(gameObject);
                            }
                        }
                    }
                }

                if (detect.collider.CompareTag("WallCollider"))
                {
                    if (gameObject.GetComponent<PhotonView>().IsMine)
                        PhotonNetwork.Destroy(gameObject);
                }
            }
        }

        public void Setup(float speed, int dmg, Vector2 dir)
        {
            Speed = speed;
            Damage = dmg;
            BulletDir = dir;
            Friendly = true;
        }

        public void EnemiSetup(Transform targetTransform)
        {
            Speed = 15;
            Damage = 10;
            BulletDir = (targetTransform.position - transform.position).normalized;
            Friendly = false;
        }
    
        public void EnemiSniperSetup(Transform targetTransform)
        {
            Speed = 40;
            Damage = 10;
            BulletDir = (targetTransform.position - transform.position).normalized;
            Friendly = false;
        }

        public void RandomatorSetup(Transform targetTransform, int damage, float speed)
        {
            Speed = speed;
            Damage = damage;
            BulletDir = (targetTransform.position - transform.position).normalized;
            Friendly = false;
        }

        private RaycastHit2D Detection()
        {
            return Physics2D.Raycast(transform.position, BulletDir, 0.5f);
        }
    }
}