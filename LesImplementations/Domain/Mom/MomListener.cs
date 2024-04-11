using System;
using System.Text.Json;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;
using Domain.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Domain.Mom
{
    public class MomListener : IServiceDeamon
    {
        private readonly ILogger _logger;

		//public InfoFromListener dlgListener;

        public event InfoFromListener InfoDlg;

        public MomListener(ILogger<MomListener> logger)
        {
            _logger = logger;
            _logger.LogInformation("Ctr du MomListener");

        }

        public void Initialize()
        {
            if(localListener == null)
            {
                localListener = new InnerMomListener(this);
                localListener.init();
            }

        }

        public void Pause()
        {
 
        }

        public void Resume()
        {
  
        }

        public void Run()
        {
            if (localListener != null && !localListener.IsRunning)
            {
                localListener.start();
            }
        }

        public void Stop()
        {

        }

        public void UnInitialize()
        {

        }

        //public void EmissionMessage(string auteur, string message)
        public void EmissionMessage(MessageComm message)
        {
			if (localListener != null && localListener.IsRunning)
			{
				localListener.SendMessage(message);
			}
		}

		/// <summary>
		///  Reference interne du listener   
		/// </summary>
		private InnerMomListener localListener = null;

        private class InnerMomListener
        {
            public bool IsRunning { get; private set; } 
            
            private Uri connecturi;

            private IConnectionFactory factory;

            private IConnection connection;

            private ISession session;

            private IMessageConsumer consumer;

			private IMessageProducer producer;

			private IDestination destination;

			private IDestination destinationOut;

			//private readonly ILogger _inner_logger;
			//private InfoFromListener _dlg;

            private MomListener pthis;

			public InnerMomListener(MomListener parent)
            {
                // _inner_logger = hostLogger;
                // _dlg = dlg;
                pthis = parent;
				pthis._logger.LogInformation("Ctr du listener");
                IsRunning = false;
            }

            public void init()
            {
                connecturi = new Uri("activemq:tcp://localhost:61616");
                factory = new Apache.NMS.ActiveMQ.ConnectionFactory(connecturi);
            }

            public void start()
            {
				pthis._logger.LogInformation("Start du listener");
                connection = factory.CreateConnection("user","user");
                connection.Start();

                session = connection.CreateSession(AcknowledgementMode.ClientAcknowledge);
                destination = SessionUtil.GetDestination(session, "queue://topic.Notification.Message");
                //destinationOut = SessionUtil.GetDestination(session, "topic://topic.Notification.Message.Output");
                destinationOut = SessionUtil.GetDestination(session, "queue://topic.Notification.Message");
                consumer = session.CreateConsumer(destination);
				producer = session.CreateProducer(destinationOut);
                producer.DeliveryMode = MsgDeliveryMode.Persistent;

				consumer.Listener += new MessageListener(OnMessage);
                IsRunning = true;
            }

            public void OnMessage(IMessage receivedMsg)
            {
                ITextMessage message = receivedMsg as ITextMessage;
                MessageComm? messageComm = JsonSerializer.Deserialize<MessageComm>(message.Text);
                pthis._logger.LogInformation($"MessageComm : {messageComm.Description}");
				pthis.InfoDlg(this, messageComm);
				receivedMsg.Acknowledge();

			}

            public void SendMessage(MessageComm message)
            {
                //ITextMessage requestMessage = session.CreateTextMessage($"{message.Description}");
                ITextMessage requestMessage = session.CreateTextMessage(JsonSerializer.Serialize(message));
                pthis._logger.LogInformation($"{message.Description}");
				producer.Send(requestMessage);
            }

        }
    }
}
