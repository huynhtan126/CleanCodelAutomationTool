namespace CleanCode {
    using System;
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource1 {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource1() {
        }
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CleanCode.Resource1", typeof(Resource1).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        /// <summary>
        ///   Looks up a localized string similar to ActiveTab.
        /// </summary>
        internal static string ActiveTab {
            get {
                return ResourceManager.GetString("ActiveTab", resourceCulture);
            }
        }
        /// <summary>
        ///   Looks up a localized string similar to Options.
        /// </summary>
        internal static string Options {
            get {
                return ResourceManager.GetString("Options", resourceCulture);
            }
        }
        /// <summary>
        ///   Looks up a localized string similar to OPTIONS_DIALOG.
        /// </summary>
        internal static string OPTIONSDIALOG {
            get {
                return ResourceManager.GetString("OPTIONSDIALOG", resourceCulture);
            }
        }
        /// <summary>
        ///   Looks up a localized string similar to {productKey}\Profiles\{acPrefComObj.Profiles.ActiveProfile}\Dialogs\OptionsDialog.
        /// </summary>
        internal static string productKeyProfilesacPrefComObjProfilesActiveProfileDialogsOptionsDialog {
            get {
                return ResourceManager.GetString("productKeyProfilesacPrefComObjProfilesActiveProfileDialogsOptionsDialog", resourceCulture);
            }
        }
        /// <summary>
        ///   Looks up a localized string similar to Abc.
        /// </summary>
        internal static string test {
            get {
                return ResourceManager.GetString("test", resourceCulture);
            }
        }
    }
}
