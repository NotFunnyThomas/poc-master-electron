using Apache.NMS.ActiveMQ.Commands;
using Domain;
using Domain.Mom;
using Domain.Tools;
using ElectronNET.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Domain.Services
{
	public class NotificationService : IServiceDeamon
	{
        //private readonly MomListener _listener;

        private readonly IServiceDeamon _listener;

        public event InfoFromListener InfoDlg;

        private readonly ILogger _logger;

		//public NotificationService(MomListener listener, ILogger<MomListener> logger)
		public NotificationService([FromKeyedServices("MomListener")]IServiceDeamon srvListener, ILogger<NotificationService> logger)
		{
			_logger = logger;
			_listener = srvListener;
		}

        

        public void EmissionMessage(MessageComm msg)
        {

        }

        public void Initialize()
		{

		}

		public void Pause()
		{

		}

		public void Resume()
		{
		}

		public void Run()
		{
			/*if ( _listener.InfoDlg == null ) {
				_logger.LogInformation($"new Delegate {CallBackFct}");
				_listener.InfoDlg = CallBackFct;
			}
			else
			{*/
				_logger.LogInformation($"Add Delegate {CallBackFct}");
				_listener.InfoDlg += CallBackFct;
			//}		
		}

		public void Stop()
		{

		}

		public void UnInitialize()
		{

		}



		private void CallBackFct(Object source, MessageComm message)
		{
			_logger.LogInformation($"Message de notification --> {message.Description} <--");
			var mainWindow = Electron.WindowManager.BrowserWindows.First();
            Electron.IpcMain.Send(mainWindow, "asynchronous-reply", message.Description);
        }
	}
}
