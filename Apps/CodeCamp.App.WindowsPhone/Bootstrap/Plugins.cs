using Cirrious.CrossCore.Plugins;

namespace CodeCamp.App.WindowsPhone.Bootstrap
{
    public class EmailPluginBootstrap
        : MvxLoaderPluginBootstrapAction<Cirrious.MvvmCross.Plugins.Email.PluginLoader, Cirrious.MvvmCross.Plugins.Email.WindowsPhone.Plugin>
    {
    }

    public class JsonPluginBootstrap
        : MvxPluginBootstrapAction<Cirrious.MvvmCross.Plugins.Json.PluginLoader>
    {
    }

    public class MessengerPluginBootstrap
        : MvxPluginBootstrapAction<Cirrious.MvvmCross.Plugins.Messenger.PluginLoader>
    {
    }

    public class WebBrowserPluginBootstrap
        : MvxLoaderPluginBootstrapAction<Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader, Cirrious.MvvmCross.Plugins.WebBrowser.WindowsPhone.Plugin>
    {
    }

    public class VisibilityPluginBootstrap
        : MvxPluginBootstrapAction<Cirrious.MvvmCross.Plugins.Visibility.PluginLoader>
    {
    }
}
