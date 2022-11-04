using UnityEngine;

namespace Composia.LevelCreator
{
    public class PaletteItem : MonoBehaviour
    {
        #if UNITY_EDITOR
        public enum Category
        {
            Misc,
            Collectibles,
            NoteBlocks,
            DecorBlocks
        }

        public Category category = Category.Misc;
        public string itemName = "";
        public Object inspectedScript;

        #endif
    }
}
