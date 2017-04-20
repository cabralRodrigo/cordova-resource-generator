using SimpleInjector;
using System.Windows;

namespace CordovaResourceGenerator.UI
{
    public partial class App : Application
    {
        private readonly Container container;

        /// <summary>
        /// Creates a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.container = new Container();
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Configure the IoC container.
            Bootstrap.ConfigInjectionDependency(this.container);

            //Creates and show wizard window.
            this.MainWindow = this.container.GetInstance<WizardWindow>();
            this.MainWindow.Show();
        }
    }
}