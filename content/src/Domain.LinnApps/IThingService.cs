namespace Linn.Template.Domain.LinnApps
{
    public interface IThingService
    {
        void SendThingMessage(string message);

        Thing CreateThing(Thing thing);
    }
}
