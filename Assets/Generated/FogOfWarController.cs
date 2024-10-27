using UnityEngine;

public class FogOfWarController : MonoBehaviour
{
    [Tooltip("The fog of war GameObject to be disabled.")]
    [SerializeField] private GameObject fogOfWar;

    private void Update()
    {
        if (AreAllEnemiesGone())
        {
            DisableFogOfWar();
        }
    }

    private bool AreAllEnemiesGone()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    private void DisableFogOfWar()
    {
        if (fogOfWar != null)
        {
            fogOfWar.SetActive(false);
        }
    }
}