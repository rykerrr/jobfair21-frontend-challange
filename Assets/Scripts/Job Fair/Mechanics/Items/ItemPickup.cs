using UnityEngine;

namespace Platformer.JobFair.Mechanics.Items
{
    /// <summary>
    /// Component class for a "physical" depiction of an item, it's animations and other stuff
    /// regarding visuals
    /// </summary>
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private Item item = default;

        public Item Item => item;
    }
}
