using System;
using System.Collections.Generic;
using System.Linq;
using Entitas.Utils;

namespace Entitas.CodeGeneration.CodeGenerator {

    public static class CodeGeneratorUtil {

        public static CodeGenerator CodeGeneratorFromPreferences() {
            var preferences = Preferences.sharedInstance;

            var config = new CodeGeneratorConfig();
            config.Configure(preferences);

            var types = LoadTypesFromPlugins(preferences);

            var dataProviders = GetEnabledInstances<ICodeGeneratorDataProvider>(types, config.dataProviders);
            var codeGenerators = GetEnabledInstances<ICodeGenerator>(types, config.codeGenerators);
            var postProcessors = GetEnabledInstances<ICodeGenFilePostProcessor>(types, config.postProcessors);

            configure(dataProviders, preferences);
            configure(codeGenerators, preferences);
            configure(postProcessors, preferences);

            return new CodeGenerator(dataProviders, codeGenerators, postProcessors);
        }

        static void configure(ICodeGeneratorInterface[] plugins, Preferences preferences) {
            foreach (var plugin in plugins.OfType<IConfigurable>()) {
                plugin.Configure(preferences);
            }
        }

        public static Type[] LoadTypesFromPlugins(Preferences preferences) {
            var config = new CodeGeneratorConfig();
            config.Configure(preferences);
            var resolver = new AssemblyResolver(AppDomain.CurrentDomain, config.searchPaths);
            foreach (var path in config.plugins) {
                resolver.Load(path);
            }

            return resolver.GetTypes();
        }

        public static string[] GetOrderedNames(string[] types) {
            return types
                    .OrderBy(type => type)
                    .ToArray();
        }

        public static T[] GetOrderedInstances<T>(Type[] types) where T : ICodeGeneratorInterface {
            return types
                    .Where(type => type.ImplementsInterface<T>())
                    .Where(type => !type.IsAbstract)
                    .Select(type => (T)Activator.CreateInstance(type))
                    .OrderBy(instance => instance.priority)
                    .ThenBy(instance => instance.GetType().ToCompilableString())
                    .ToArray();
        }

        public static string[] GetOrderedTypeNames<T>(Type[] types) where T : ICodeGeneratorInterface {
            return GetOrderedInstances<T>(types)
                    .Select(instance => instance.GetType().ToCompilableString())
                    .ToArray();
        }

        public static T[] GetEnabledInstances<T>(Type[] types, string[] enabledTypeNames) where T : ICodeGeneratorInterface {
            return GetOrderedInstances<T>(types)
                    .Where(instance => enabledTypeNames.Contains(instance.GetType().ToCompilableString()))
                    .ToArray();
        }

        public static string[] GetAvailable<T>(Type[] types, string[] enabledTypeNames) where T : ICodeGeneratorInterface {
            return GetOrderedTypeNames<T>(types)
                    .Where(typeName => !enabledTypeNames.Contains(typeName))
                    .ToArray();
        }

        public static string[] GetUnavailable<T>(Type[] types, string[] enabledTypeNames) where T : ICodeGeneratorInterface {
            var typeNames = GetOrderedTypeNames<T>(types);
            return enabledTypeNames
                    .Where(typeName => !typeNames.Contains(typeName))
                    .ToArray();
        }

        public static T[] GetUsed<T>(Type[] types, string[] enabledTypeNames) where T : ICodeGeneratorInterface {
            return GetOrderedInstances<T>(types)
                .Where(instance => enabledTypeNames.Contains(instance.GetType().ToCompilableString()))
                .ToArray();
        }

        public static Dictionary<string, string> GetConfigurables(ICodeGeneratorDataProvider[] dataProviders, ICodeGenerator[] codeGenerators, ICodeGenFilePostProcessor[] postProcessors) {
            return new Dictionary<string, string>()
                .Merge(dataProviders.OfType<IConfigurable>()
                       .Concat(codeGenerators.OfType<IConfigurable>())
                       .Concat(postProcessors.OfType<IConfigurable>())
                       .Select(instance => instance.defaultProperties)
                       .ToArray());
        }
    }
}
