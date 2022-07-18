using System;
using System.Collections.Generic;
using System.Reflection;

namespace BreakDown.ManagedPdf.Core.Pdf
{
    /// <summary>
    /// Contains meta information about all keys of a PDF dictionary.
    /// </summary>
    internal class DictionaryMeta
    {
        public DictionaryMeta(Type type)
        {
            //#if (NETFX_CORE && DEBUG) || CORE
            //            if (type == typeof(PdfPages.Keys))
            //            {
            //                var x = typeof(PdfPages).GetRuntimeFields();
            //                var y = typeof(PdfPages).GetTypeInfo().DeclaredFields;
            //                x.GetType();
            //                y.GetType();
            //                Debug-Break.Break();
            //                Test.It();
            //            }
            //#endif
#if !NETFX_CORE && !UWP
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(typeof(KeyInfoAttribute), false);
                if (attributes.Length == 1)
                {
                    var attribute = (KeyInfoAttribute)attributes[0];
                    var descriptor = new KeyDescriptor(attribute);
                    descriptor.KeyValue = (string)field.GetValue(null);
                    _keyDescriptors[descriptor.KeyValue] = descriptor;
                }
            }
#else
            // Rewritten for WinRT.
            CollectKeyDescriptors(type);
            //var fields = type.GetRuntimeFields();  // does not work
            //fields2.GetType();
            //foreach (FieldInfo field in fields)
            //{
            //    var attributes = field.GetCustomAttributes(typeof(KeyInfoAttribute), false);
            //    foreach (var attribute in attributes)
            //    {
            //        KeyDescriptor descriptor = new KeyDescriptor((KeyInfoAttribute)attribute);
            //        descriptor.KeyValue = (string)field.GetValue(null);
            //        _keyDescriptors[descriptor.KeyValue] = descriptor;
            //    }
            //}
#endif
        }

#if NETFX_CORE || UWP || DNC10
        // Background: The function GetRuntimeFields gets constant fields only for the specified type,
        // not for its base types. So we have to walk recursively through base classes.
        // The docmentation says full trust for the immediate caller is required for property BaseClass.
        // TODO: Rewrite this stuff for medium trust.
        void CollectKeyDescriptors(Type type)
        {
            // Get fields of the specified type only.
            var fields = type.GetTypeInfo().DeclaredFields;
            foreach (FieldInfo field in fields)
            {
                var attributes = field.GetCustomAttributes(typeof(KeyInfoAttribute), false);
                foreach (var attribute in attributes)
                {
                    KeyDescriptor descriptor = new KeyDescriptor((KeyInfoAttribute)attribute);
                    descriptor.KeyValue = (string)field.GetValue(null);
                    _keyDescriptors[descriptor.KeyValue] = descriptor;
                }
            }
            type = type.GetTypeInfo().BaseType;
            if (type != typeof(object) && type != typeof(PdfObject))
                CollectKeyDescriptors(type);
        }
#endif

#if (NETFX_CORE || CORE) && true_
        public class A
        {
            public string _a;
            public const string _ca = "x";
        }
        public class B : A
        {
            public string _b;
            public const string _cb = "x";

            void Foo()
            {
                var str = A._ca;
            }
        }
        class Test
        {
            public static void It()
            {
                string s = "Runtime fields of B:";
                foreach (var fieldInfo in typeof(B).GetRuntimeFields()) { s += " " + fieldInfo.Name; }
                Debug.WriteLine(s);

                s = "Declared fields of B:";
                foreach (var fieldInfo in typeof(B).GetTypeInfo().DeclaredFields) { s += " " + fieldInfo.Name; }
                Debug.WriteLine(s);

                s = "Runtime fields of PdfPages.Keys:";
                foreach (var fieldInfo in typeof(PdfPages.Keys).GetRuntimeFields()) { s += " " + fieldInfo.Name; }
                Debug.WriteLine(s);
            }
        }
#endif
        /// <summary>
        /// Gets the KeyDescriptor of the specified key, or null if no such descriptor exits.
        /// </summary>
        public KeyDescriptor this[string key]
        {
            get
            {
                _keyDescriptors.TryGetValue(key, out var keyDescriptor);
                return keyDescriptor;
            }
        }

        readonly Dictionary<string, KeyDescriptor> _keyDescriptors = new Dictionary<string, KeyDescriptor>();
    }
}
