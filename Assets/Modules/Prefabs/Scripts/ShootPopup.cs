using UnityEngine;

namespace Modules.Prefabs.Scripts
{
    public class ShootPopup : MonoBehaviour
    {
        private float destroyTime = 1.5f;

        private void Update()
        {
            float moveSpeed = 2f;
            transform.position += Vector3.up * (moveSpeed * Time.deltaTime);

            destroyTime -= Time.deltaTime;
            
            if (destroyTime <= 0f)
                Destroy(gameObject);
        }
    }
}
