﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace utility.ResourceMessage {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Message {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Message() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("utility.ResourceMessage.Message", typeof(Message).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to در عملیات حذف خطایی رخ داده است.
        /// </summary>
        public static string DeleteError {
            get {
                return ResourceManager.GetString("DeleteError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حجم تصویر انتخابی نمی تواند بیشتر از 700 کیلوبایت باشد.
        /// </summary>
        public static string InvalidFileSize {
            get {
                return ResourceManager.GetString("InvalidFileSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فرمت فایل انتخابی صحیح نمی باشد.
        /// </summary>
        public static string InvalidFileType {
            get {
                return ResourceManager.GetString("InvalidFileType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to خبر مورد نظر یافت نشد.
        /// </summary>
        public static string PostNotFound {
            get {
                return ResourceManager.GetString("PostNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to در عملیات ثبت خطایی رخ داده است.
        /// </summary>
        public static string SaveError {
            get {
                return ResourceManager.GetString("SaveError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تعداد آگهی های ثبت شده در یک روز اخیر، نمی تواند بیشتر از سه عدد باشد.
        /// </summary>
        public static string SavePostLimitation {
            get {
                return ResourceManager.GetString("SavePostLimitation", resourceCulture);
            }
        }
    }
}
