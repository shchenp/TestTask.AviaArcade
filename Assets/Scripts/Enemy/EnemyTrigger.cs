using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.EnemyTag))
        {
            ScreenManager.Instance.ShowFailedScreen();
        }
    }
}