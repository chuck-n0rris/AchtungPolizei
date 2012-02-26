using System;

namespace Altalerta.Core.Essential
{
    public class BuildInfo
    {
        /// <summary>
        /// Name of the project.
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Number of the build.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Time of the build.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Is build successful?
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Authors of changes since previous build.
        /// </summary>
        public string[] Authors { get; set; }

        public override string ToString()
        {
            return string.Format(
                "BuildInfo. Project: {0}, Number: {1}, Time: {2}, " +
                "Is Successful: {3}, Authors: {4}.",
                Project,
                Number,
                Time,
                IsSuccessful,
                string.Join(", ", Authors));
        }

        public BuildState GetState(bool wasPreviouslyBroken)
        {
            if (!IsSuccessful && wasPreviouslyBroken)
            {
                return BuildState.StillBroken;
            }

            if (!IsSuccessful && !wasPreviouslyBroken)
            {
                return BuildState.Broken;
            }

            if (IsSuccessful && wasPreviouslyBroken)
            {
                return BuildState.Fixed;
            }

            return BuildState.Ok;
        }
    }
}