using UnityEngine;

public class PickupSpawnPointGizmo : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }

}
