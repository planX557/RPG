using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Item effect/Portal scroll", fileName = "item effect data - Portal scroll")]

public class ItemEffect_PortalScroll : ItemEffect_DataSO
{
    public override void ExecuteEffect()
    {
        if (SceneManager.GetActiveScene().name == "Level_0")
        {
            Debug.Log("Cannot open portal in town!");
            return;
        }

        Player player = Player.instance;
        Vector3 portalPosition = player.transform.position + new Vector3(player.facingDir * 1.5f, 0);

        Object_Portal.instance.ActivatePortal(portalPosition, player.facingDir);
    }
}
