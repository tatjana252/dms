using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Messaging
{
    public interface IBus
    {

        void SetUpBus();

        void AddConsumer(DocumentInfo input);

        DocumentsResponse? Request(DocumentInfo input);

        void Publish(object document);

        void PublishDocumentCreated(Document doc);

        void AddRequestConsumer(DocumentInfo input);

        void StartBus();

        void StopBus();
        void AddUpdateConsumer(DocumentInfo i);

        void AddBinderConsumer(DocumentInfo i);
    }
}
