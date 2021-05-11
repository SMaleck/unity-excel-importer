using UnityEngine;

namespace ExcelImporter.Editor
{
    [CreateAssetMenu]
    public class ExcelImporterSettings : ScriptableObject
    {
        [Header("WorkBook Importers Settings")]
        [Tooltip("Needs access to " + nameof(SheetNamespace) + ", but does not need to go into builds.")]
        [SerializeField] private string _importerNamespace;
        [SerializeField] private string _importerCodePath;

        [Header("Sheet Import Settings")]
        [Tooltip("These will be used for the ScriptableObjects and need to be accessible by your game code")]
        [SerializeField] private string _sheetNamespace;
        [SerializeField] private string _sheetCodePath;

        [Header("Import Assets")]
        [SerializeField] private string _importAssetPath;

        [Header("Misc. Settings")]
        [SerializeField] private string _ignoredSheetPrefix;
        [SerializeField] private string _ignoredColumnPrefix;
        [SerializeField] private bool _prefixAssetNamesByDefault;

        public string SheetNamespace => _sheetNamespace;
        public string SheetCodePath => _sheetCodePath;
        public string ImporterNamespace => _importerNamespace;
        public string ImporterCodePath => _importerCodePath;
        public string ImportAssetPath => _importAssetPath;
        public string IgnoredSheetPrefix => _ignoredSheetPrefix;
        public string IgnoredColumnPrefix => _ignoredColumnPrefix;
        public bool PrefixAssetNamesByDefault => _prefixAssetNamesByDefault;

        public void ResetDefaults()
        {
            _importerNamespace = "Game.Editor.Data.Importers";
            _importerCodePath = "Assets/Data/Editor/Generated/Importers";

            _sheetNamespace = "Game.Data.Tables";
            _sheetCodePath = "Assets/Data/Runtime/Tables";

            _importAssetPath = "Assets/Data/Imports/";

            _ignoredSheetPrefix = "_";
            _ignoredColumnPrefix = "_";
            _prefixAssetNamesByDefault = false;
        }
    }
}