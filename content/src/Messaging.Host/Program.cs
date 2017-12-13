namespace Linn.Template.Messaging.Host
{
    using Autofac;

    using Linn.Common.Logging;

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = Configuration.BuildContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var log = scope.Resolve<ILog>();
                var listener = new Listener(scope, log);

                while (true)
                {
                    listener.Listen();
                }
            }
        }
    }
}