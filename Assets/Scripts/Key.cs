using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable/Key")]
public class Key : Item
{
    public string keyId;
    public override void Use(GameObject user)
    {
        Debug.Log($"Utilisation de la clé {itemName}");
        // verifier que regarde l'utilisateur, si c'est un coffre, un teleporteur, etc
        if (user != null)
        {
            // trouve ce qu'il regarde
            RaycastHit hit;
            Transform camera = user.GetComponent<PlayerMovement>().camera;
            if (Physics.Raycast(camera.position, camera.forward, out hit, 3f))
            {
                Object lockable = hit.transform.GetComponent<Object>();
                if (lockable != null)
                {
                    var unlockMethod = lockable.GetType().GetMethod("Unlock");
                    if (unlockMethod != null) unlockMethod.Invoke(lockable, new object[] { user });
                }
            }
        }
    }
}