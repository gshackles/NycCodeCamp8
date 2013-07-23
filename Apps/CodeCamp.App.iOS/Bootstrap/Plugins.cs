using Cirrious.CrossCore.Plugins;

namespace CodeCamp.App.iOS.Bootstrap
{
    public class EmailPluginBootstrap
        : MvxLoaderPluginBootstrapAction<Cirrious.MvvmCross.Plugins.Email.PluginLoader, Cirrious.MvvmCross.Plugins.Email.Touch.Plugin>
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
        : MvxLoaderPluginBootstrapAction<Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader, Cirrious.MvvmCross.Plugins.WebBrowser.Touch.Plugin>
    {
    }
}