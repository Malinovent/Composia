using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Composia.LevelCreator
{
    public class PaletteWindow : EditorWindow
    {
        public static PaletteWindow instance;

        private List<PaletteItem.Category> _categories;
        private List<string> _categoryLabels;
        private PaletteItem.Category _categorySelected;

        private string _path = "Assets/_Composia/Gameplays/Creation/Level Pieces";
        private List<PaletteItem> _items;
        private Dictionary<PaletteItem.Category, List<PaletteItem>> _categorizedItems;
        private Dictionary<PaletteItem, Texture2D> _previews;
        private Vector2 _scrollPosition;
        private const float ButtonWidth = 80;
        private const float ButtonHeight = 90;

        public delegate void itemSelectedDelegate(PaletteItem item, Texture2D preview);
        public static event itemSelectedDelegate ItemSelectedEvent;

        private GUIStyle _tabStyle;

        public static void ShowPalette()
        {
            instance = (PaletteWindow)EditorWindow.GetWindow(typeof(PaletteWindow));
            instance.titleContent = new GUIContent("Palette");
        }

        private void OnEnable()
        {
            InitStyles();
            if (_categories == null)
            {
                InitCategories();
            }

            if(_categorizedItems == null)
            {
                InitContent();
            }
        }

        private void OnDisable()
        {
            
        }

        private void OnDestroy()
        {
           
        }

        private void OnGUI()
        {
            DrawTabs();
            DrawScroll();
        }

        private void Update()
        {
            if(_previews.Count != _items.Count)
            {
                GeneratePreviews();
            }
        }

        private void InitStyles()
        {
            _tabStyle = new GUIStyle();
            _tabStyle.alignment = TextAnchor.MiddleCenter;
            _tabStyle.fontSize = 16;

            Texture2D tabNormal = (Texture2D)Resources.Load("Tab_Normal");
            Texture2D tabSelected = (Texture2D)Resources.Load("Tab_Selected");
            Font tabFont = (Font)Resources.Load("Oswald-Regular");

            _tabStyle.font = tabFont;
            _tabStyle.fixedHeight = 40;

            _tabStyle.normal.background = tabNormal;
            _tabStyle.normal.textColor = Color.grey;

            _tabStyle.onNormal.background = tabSelected;
            _tabStyle.onNormal.textColor = Color.black;

            _tabStyle.focused.background = tabSelected;
            _tabStyle.focused.textColor = Color.black;

            _tabStyle.border = new RectOffset(18,18,20,4);
        }

        private void InitCategories()
        {
            Debug.Log("InitCategories Called...");
            _categories = EditorUtils.GetListFromEnum<PaletteItem.Category>();
            _categoryLabels = new List<string>();

            foreach (PaletteItem.Category category in _categories)
            {
                _categoryLabels.Add(category.ToString());
            }
        }

        private void InitContent()
        {
            //Set the ScrollList
            _items = EditorUtils.GetAssetsWithScript<PaletteItem>(_path);
            _categorizedItems = new Dictionary<PaletteItem.Category, List<PaletteItem>>();
            _previews = new Dictionary<PaletteItem, Texture2D>();

            foreach (PaletteItem.Category category in _categories)
            {
                _categorizedItems.Add(category, new List<PaletteItem>());
            }

            foreach (PaletteItem item in _items)
            {
                _categorizedItems[item.category].Add(item);
            }
        }

        private void DrawTabs()
        {
            int index = (int)_categorySelected;
            EditorGUILayout.Space();
            index = GUILayout.Toolbar(index, _categoryLabels.ToArray(), _tabStyle);
            _categorySelected = _categories[index];
        }

        private void DrawScroll()
        {
            if (_categorizedItems[_categorySelected].Count == 0)
            {
                EditorGUILayout.HelpBox("This category is empty!", MessageType.Info);
                return;
            }
            int rowCapacity = Mathf.FloorToInt(position.width / (ButtonWidth));

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            int selectionGridIndex = -1;
            selectionGridIndex = GUILayout.SelectionGrid(
                selectionGridIndex,
                GetGUIContentsFromItems(),
                rowCapacity,
                GetGUIStyle()
                );

            GetSelectedItem(selectionGridIndex);
            GUILayout.EndScrollView();
        }


        private void GeneratePreviews()
        {
            //int count = 0;
            foreach (PaletteItem item in _items)
            { 
                if(!_previews.ContainsKey(item))
                {
                    Texture2D preview = AssetPreview.GetAssetPreview(item.gameObject);
                    if (preview != null)
                    {
                        //count++;
                        _previews.Add(item, preview);
                        //textures[count] = preview;
                    }
                }
            }
        }
        /*
        private Texture2D[] textures;

        private void SavePreviews()
        {
            foreach (Texture2D t in textures)
            { 
                
            }
        }*/

        private GUIContent[] GetGUIContentsFromItems()
        {
            List<GUIContent> guiContents = new List<GUIContent>();

            if (_previews.Count == _items.Count)
            {
                int totalItems = _categorizedItems[_categorySelected].Count;

                for (int i = 0; i < totalItems; i++)
                {
                    GUIContent guiContent = new GUIContent();
                    guiContent.text = _categorizedItems[_categorySelected][i].itemName;
                    guiContent.image = _previews[_categorizedItems[_categorySelected][i]];
                    guiContents.Add(guiContent);
                }
            }

            return guiContents.ToArray();
        }

        private GUIStyle GetGUIStyle()
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
            guiStyle.alignment = TextAnchor.LowerCenter;
            guiStyle.imagePosition = ImagePosition.ImageAbove;
            guiStyle.fixedWidth = ButtonWidth;
            guiStyle.fixedHeight = ButtonHeight;
            return guiStyle;
        }

        private void GetSelectedItem(int index)
        {
            if (index != -1)
            {
                PaletteItem selectedItem = _categorizedItems[_categorySelected][index];
                Debug.Log("Selected Item is " + selectedItem.itemName);


                ItemSelectedEvent?.Invoke(selectedItem, _previews[selectedItem]);
            }

        }

    }
}
