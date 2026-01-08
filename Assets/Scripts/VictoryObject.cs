using UnityEngine;

public class VictoryObject : MonoBehaviour
{
    public WinMenu winMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winMenu.ShowVictory();
        }
    }
}
