using UnityEngine;

public class MouseToWorldPosition : MonoBehaviour
{
    [Header("Sprite")]
        [Tooltip("Sprite to visualize mouse world position")]
        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [Tooltip("How much to offset the world position from the mouse")]
        [SerializeField] private Vector3 m_Offset;
        
        [Header("Raycasting parameters")]
        [Tooltip("Camera to use for raycast")]
        [SerializeField] Camera m_MainCamera;
        [Tooltip("Limit ray casting to this layer")]
        [SerializeField] private LayerMask m_LayerMask;
        [Tooltip("Rectangular screen area to ignore as a dead zone (percentage of screen); (x,y) = (bottom, left)")]
        [SerializeField] ScreenDeadZone m_ScreenDeadZone;


        public ScreenDeadZone ScreenDeadZone => m_ScreenDeadZone;
        public Vector3 Offset => m_Offset;
        
        private Vector3 m_Position;
        public Vector3 Position => m_Position;

        private void Awake()
        {
            if (m_MainCamera == null)
                m_MainCamera = Camera.main;
        }

        // Use Raycast to convert mouse position to 3D world coordinate
        private Vector3 GetMouseWorldSpacePosition()
        {
            if (m_MainCamera == null)
            {
                Debug.Log("[MouseToWorldPosition]: Missing a main camera");
                return Vector3.zero;
            }

            RaycastHit hit;
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);

            // Debugging information
            // Debug.Log("Ray origin: " + ray.origin);
            // Debug.Log("Ray direction: " + ray.direction);
            // Debug.Log("Mouse position: " + Input.mousePosition);

            // Visualize the ray in the editor
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
            {
                Debug.Log("Run here: Ray hit an object at position: " + hit.point);
                return new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            else
            {
                // Debug.Log("Ray did not hit any object.");
            }

            return Vector3.zero;
        }
        
        
        private void Update()
        {
            if (m_ScreenDeadZone.IsMouseInDeadZone())
            {
                m_SpriteRenderer.enabled = false;
                return;
            }
            
            m_Position = GetMouseWorldSpacePosition();
            
            if (m_SpriteRenderer != null)
            {
                m_SpriteRenderer.enabled = true;
                m_SpriteRenderer.transform.position = Position + m_Offset;
            }
        }
}
