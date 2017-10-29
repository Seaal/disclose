namespace Disclose
{
    /// <summary>
    /// Installers can be used to install multiple different handlers linking to a single feature.
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// This is called by the disclose client when you install this installer.
        /// </summary>
        /// <param name="discoseClient"></param>
        void Install(DiscloseClient discoseClient);
    }
}
