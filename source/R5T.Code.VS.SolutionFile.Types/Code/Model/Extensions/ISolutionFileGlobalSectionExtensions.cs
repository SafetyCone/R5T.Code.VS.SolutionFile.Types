using System;
using System.Collections.Generic;
using System.Linq;


namespace R5T.Code.VisualStudio.Model
{
    public static class ISolutionFileGlobalSectionExtensions
    {
        public static bool HasGlobalSectionByName<T>(this IEnumerable<ISolutionFileGlobalSection> globalSections, string globalSectionName, out T globalSection)
            where T : class, ISolutionFileGlobalSection
        {
            // Intermediate reference required.
            globalSection = globalSections.Where(x => x.Name == globalSectionName).ToList().SingleOrDefault() as T;

            var output = globalSection != default;
            return output;
        }

        public static bool HasGlobalSectionByName<T>(this IEnumerable<ISolutionFileGlobalSection> globalSections, string globalSectionName)
            where T : class, ISolutionFileGlobalSection
        {
            var output = globalSections.HasGlobalSectionByName<T>(globalSectionName, out var _);
            return output;
        }

        public static T GetGlobalSectionByName<T>(this IEnumerable<ISolutionFileGlobalSection> globalSections, string globalSectionName)
            where T : class, ISolutionFileGlobalSection
        {
            var hasGlobalSection = globalSections.HasGlobalSectionByName<T>(globalSectionName, out var globalSection);
            if (!hasGlobalSection)
            {
                throw new InvalidOperationException($"No '{globalSectionName}' global section found.");
            }

            return globalSection;
        }

        public static T AddGlobalSection<T>(this List<ISolutionFileGlobalSection> globalSections, Func<T> constructor)
            where T : ISolutionFileGlobalSection
        {
            var globalSection = constructor();

            globalSections.Add(globalSection);

            return globalSection;
        }

        public static T AcquireGlobalSectionByName<T>(this List<ISolutionFileGlobalSection> globalSections, string globalSectionName, Func<T> constructor)
            where T : class, ISolutionFileGlobalSection
        {
            var hasGlobalSection = globalSections.HasGlobalSectionByName<T>(globalSectionName, out var globalSection);
            if (!hasGlobalSection)
            {
                globalSection = globalSections.AddGlobalSection(constructor);
            }

            return globalSection;
        }

        public static bool HasGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections, string globalSectionName, out GenericSolutionFileGlobalSection globalSection)
        {
            var output = globalSections.HasGlobalSectionByName(globalSectionName, out globalSection);
            return output;
        }

        public static bool HasGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections, string globalSectionName)
        {
            var output = globalSections.HasGlobalSectionByName<GenericSolutionFileGlobalSection>(globalSectionName);
            return output;
        }

        public static GenericSolutionFileGlobalSection GetGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections, string globalSectionName)
        {
            var globalSection = globalSections.GetGlobalSectionByName<GenericSolutionFileGlobalSection>(globalSectionName);
            return globalSection;
        }

        public static GenericSolutionFileGlobalSection AddGlobalSection(this List<ISolutionFileGlobalSection> globalSections, string globalSectionName, PreOrPostSolution preOrPostSolution)
        {
            var globalSection = globalSections.AddGlobalSection(() => GenericSolutionFileGlobalSection.New(globalSectionName, preOrPostSolution));
            return globalSection;
        }

        public static GenericSolutionFileGlobalSection AcquireGlobalSection(this List<ISolutionFileGlobalSection> globalSections, string globalSectionName, PreOrPostSolution preOrPostSolution)
        {
            var globalSection = globalSections.AcquireGlobalSectionByName(globalSectionName, () => GenericSolutionFileGlobalSection.New(globalSectionName, preOrPostSolution));
            return globalSection;
        }

        public static bool HasNestedProjectsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections, out NestedProjectsSolutionFileGlobalSection nestedProjectsSolutionFileGlobalSection)
        {
            var output = globalSections.HasGlobalSectionByName(NestedProjectsSolutionFileGlobalSection.SolutionFileGlobalSectionName, out nestedProjectsSolutionFileGlobalSection);
            return output;
        }

        public static bool HasNestedProjectsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections)
        {
            var output = globalSections.HasGlobalSectionByName<NestedProjectsSolutionFileGlobalSection>(NestedProjectsSolutionFileGlobalSection.SolutionFileGlobalSectionName);
            return output;
        }

        public static NestedProjectsSolutionFileGlobalSection GetNestedProjectsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections)
        {
            var nestedProjectsGlobalSection = globalSections.GetGlobalSectionByName<NestedProjectsSolutionFileGlobalSection>(NestedProjectsSolutionFileGlobalSection.SolutionFileGlobalSectionName);
            return nestedProjectsGlobalSection;
        }

        public static NestedProjectsSolutionFileGlobalSection AddNestedProjectsGlobalSection(this List<ISolutionFileGlobalSection> globalSections)
        {
            var nestedProjectsGlobalSection = globalSections.AddGlobalSection(NestedProjectsSolutionFileGlobalSection.New);
            return nestedProjectsGlobalSection;
        }

        public static NestedProjectsSolutionFileGlobalSection AcquireNestedProjectsGlobalSection(this List<ISolutionFileGlobalSection> globalSections)
        {
            var nestedProjectsGlobalSection = globalSections.AcquireGlobalSectionByName(NestedProjectsSolutionFileGlobalSection.SolutionFileGlobalSectionName, NestedProjectsSolutionFileGlobalSection.New);
            return nestedProjectsGlobalSection;
        }

        public static bool HasProjectConfigurationPlatformsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections, out ProjectConfigurationPlatformsGlobalSection projectConfigurationPlatformsGlobalSection)
        {
            var output = globalSections.HasGlobalSectionByName(ProjectConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName, out projectConfigurationPlatformsGlobalSection);
            return output;
        }

        public static bool HasProjectConfigurationPlatformsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections)
        {
            var output = globalSections.HasGlobalSectionByName<ProjectConfigurationPlatformsGlobalSection>(ProjectConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName);
            return output;
        }

        public static ProjectConfigurationPlatformsGlobalSection GetProjectConfigurationPlatformsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections)
        {
            var projectConfigurationPlatformsGlobalSection = globalSections.GetGlobalSectionByName<ProjectConfigurationPlatformsGlobalSection>(ProjectConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName);
            return projectConfigurationPlatformsGlobalSection;
        }

        public static ProjectConfigurationPlatformsGlobalSection AddProjectConfigurationPlatformsGlobalSection(this List<ISolutionFileGlobalSection> globalSections)
        {
            var projectConfigurationPlatformsGlobalSection = globalSections.AddGlobalSection(ProjectConfigurationPlatformsGlobalSection.New);
            return projectConfigurationPlatformsGlobalSection;
        }

        public static ProjectConfigurationPlatformsGlobalSection AcquireProjectConfigurationPlatformsGlobalSection(this List<ISolutionFileGlobalSection> globalSections)
        {
            var projectConfigurationPlatformsGlobalSection = globalSections.AcquireGlobalSectionByName(ProjectConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName, ProjectConfigurationPlatformsGlobalSection.New);
            return projectConfigurationPlatformsGlobalSection;
        }

        public static bool HasSolutionConfigurationPlatformsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections, out SolutionConfigurationPlatformsGlobalSection solutionConfigurationPlatformsGlobalSection)
        {
            var output = globalSections.HasGlobalSectionByName(SolutionConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName, out solutionConfigurationPlatformsGlobalSection);
            return output;
        }

        public static bool HasSolutionConfigurationPlatformsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections)
        {
            var output = globalSections.HasGlobalSectionByName<SolutionConfigurationPlatformsGlobalSection>(SolutionConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName);
            return output;
        }

        public static SolutionConfigurationPlatformsGlobalSection GetSolutionConfigurationPlatformsGlobalSection(this IEnumerable<ISolutionFileGlobalSection> globalSections)
        {
            var solutionConfigurationPlatformsGlobalSection = globalSections.GetGlobalSectionByName<SolutionConfigurationPlatformsGlobalSection>(SolutionConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName);
            return solutionConfigurationPlatformsGlobalSection;
        }

        public static SolutionConfigurationPlatformsGlobalSection AddSolutionConfigurationPlatformsGlobalSection(this List<ISolutionFileGlobalSection> globalSections)
        {
            var solutionConfigurationPlatformsGlobalSection = globalSections.AddGlobalSection(SolutionConfigurationPlatformsGlobalSection.New);
            return solutionConfigurationPlatformsGlobalSection;
        }

        public static SolutionConfigurationPlatformsGlobalSection AcquireSolutionConfigurationPlatformsGlobalSection(this List<ISolutionFileGlobalSection> globalSections)
        {
            var solutionConfigurationPlatformsGlobalSection = globalSections.AcquireGlobalSectionByName(SolutionConfigurationPlatformsGlobalSection.SolutionFileGlobalSectionName, SolutionConfigurationPlatformsGlobalSection.New);
            return solutionConfigurationPlatformsGlobalSection;
        }
    }
}
