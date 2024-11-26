using UnityEngine;

public class FogOfWarController : MonoBehaviour
{
    [Tooltip("The fog of war GameObject to be disabled.")]
    [SerializeField] private GameObject fogOfWar;
    private V2SpriteManager v2SpriteManager;
    private void Start()
    {
        v2SpriteManager = GameObject.Find("SpriteManager").GetComponent<V2SpriteManager>();
        DisableFogOfWar();
    }
    private void Update()
    {
        if (AreAllEnemiesGone())
        {
            DisableFogOfWar();
        }
        else{
            EnableFogOfWar();
        }
    }

    private bool AreAllEnemiesGone()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }
    private void DisableFogOfWar()
    {
        foreach (GameObject o in v2SpriteManager.agents)
        {
            Player agent = o.GetComponent<Player>();
            agent.InterpolateFogScale(100.0f, 0.1f);
        }
    }
    private void EnableFogOfWar()
    {
        foreach (GameObject o in v2SpriteManager.agents)
        {
            Player agent = o.GetComponent<Player>();
            agent.InterpolateFogScale(2.5f, 0.1f);
        }
    }
}