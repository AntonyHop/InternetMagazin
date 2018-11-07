using InternetMagazine.PL.Infrastructure;
using Ninject;
using Ninject.Modules;
using System.Windows;
using WpfClient.Modules;

namespace WpfClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IKernel container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NinjectModule md = new MainNinjectModule();
            NinjectModule serviceModule = new ServiceModule(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InternetMagazineApteka;Integrated Security=True");
            container = new StandardKernel(md,serviceModule);

            Current.MainWindow = this.container.Get<MainWindow>();
            Current.MainWindow.Title = "Auth";
            Current.MainWindow.Show();

        }

    }
}
