namespace Linn.Template.Messaging.Handlers
{
    using System;
    using System.Text;

    using Linn.Common.Logging;
    using Linn.Common.Messaging.RabbitMQ.Handlers;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Messaging.Messages;
    using Linn.Template.Resources;

    using Newtonsoft.Json;

    public class ThingMessageHandler : Handler<ThingMessage>
    {
        public ThingMessageHandler(ILog logger)
            : base(logger)
        {
        }

        public override bool Handle(ThingMessage message)
        {
            this.Logger.Info("Message received: " + message.Event.RoutingKey);

            try
            {
                var body = message.Event.Body.ToArray();
                var enc = Encoding.UTF8.GetString(body);
                var resource = JsonConvert.DeserializeObject<Thing>(enc);
                this.Logger.Info("Data: " + resource.Name);
                return true;
            }
            catch (Exception e)
            {
                this.Logger.Error(e.Message);
                return false;
            }
        }
    }
}
