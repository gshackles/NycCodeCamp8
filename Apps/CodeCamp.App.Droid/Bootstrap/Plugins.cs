using Cirrious.CrossCore.Plugins;

namespace CodeCamp.App.Droid.Bootstrap
{
    public class EmailPluginBootstrap
        : MvxLoaderPluginBootstrapAction<Cirrious.MvvmCross.Plugins.Email.PluginLoader, Cirrious.MvvmCross.Plugins.Email.Droid.Plugin>
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
        : MvxLoaderPluginBootstrapAction<Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader, Cirrious.MvvmCross.Plugins.WebBrowser.Droid.Plugin>
    {
    }
}