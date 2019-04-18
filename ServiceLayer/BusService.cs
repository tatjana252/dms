using ActivityMessaging;
using MassTransit.Util;
using Models;
using System.Collections.Generic;

namespace ServiceLayer
{
    //service layer 
    public class DMBus
    {
        //ovo treba da postane interfejs
        private static RabbitMqBus _bus;

        public DMBus(RabbitMqBus rabbit)
        {
            _bus = rabbit;
        }
        public void Start(Activity activity)
        {
            ConfigureInput(activity.InputDocuments);
            ConfigureOutput(activity.OutputDocuments);
            _bus.StartBus();
        }
        public DocumentsResponse SendRequest(Document input)
        {
            return _bus.Request(input);
        }
        public void SendDocument(Document document)
        {
            _bus.Publish(document);
        }

        private void ConfigureInput(IEnumerable<Document> inputs)
        {
            if(inputs != null)
            foreach (var input in inputs)
            {
                switch (input.InputOperation)
                {
                    case InputOperations.Read:
                        _bus.AddConsumer(input);
                        break;
                    case InputOperations.Request:
                        break;
                }
            }
        }
        private void ConfigureOutput(IEnumerable<Document> outputs)
        {
            if (outputs != null)
                foreach (var output in outputs)
            {
                switch (output.OutputOperation)
                {
                    case OutputOperations.Send:
                        break;
                    case OutputOperations.Create:
                        _bus.AddRequestConsumer(output);
                        break;
                }
            }
        }

    }
}
