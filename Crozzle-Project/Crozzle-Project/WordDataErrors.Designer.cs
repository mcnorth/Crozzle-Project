﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crozzle_Project {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class WordDataErrors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WordDataErrors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Crozzle_Project.WordDataErrors", typeof(WordDataErrors).Assembly);
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
        ///   Looks up a localized string similar to code 9004: word ({0}) is not alphabetic.
        /// </summary>
        internal static string AlphabeticError {
            get {
                return ResourceManager.GetString("AlphabeticError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to code 9002: word data ({0}) is missing data in field {1}.
        /// </summary>
        internal static string BlankFieldError {
            get {
                return ResourceManager.GetString("BlankFieldError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to code 9001: {0} fields in word data ({1}), instead of {2}.
        /// </summary>
        internal static string FieldCountError {
            get {
                return ResourceManager.GetString("FieldCountError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to code 9003: word ({0}) is not in the word list.
        /// </summary>
        internal static string MissingWordError {
            get {
                return ResourceManager.GetString("MissingWordError", resourceCulture);
            }
        }
    }
}