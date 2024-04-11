using Apache.NMS.ActiveMQ.Commands;
using Domain;
using Domain.Mom;
using ElectronNET.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Domain.Services
{
	public class NotificationService : IServiceDeamon
	{
		private readonly MomListener _listener;

		private readonly ILogger _logger;

		//public NotificationService(MomListener listener, ILogger<MomListener> logger)
		public NotificationService([FromKeyedServices("MomListener")]IServiceDeamon srvListener, ILogger<MomListener> logger)
		{
			_logger = logger;
			_listener = srvListener as MomListener;
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
			if ( _listener.dlgListener == null ) {
				_logger.LogInformation($"new Delegate {CallBackFct}");
				_listener.dlgListener = CallBackFct;
			}
			else
			{
				_logger.LogInformation($"Add Delegate {CallBackFct}");
				_listener.dlgListener += CallBackFct;
			}		
		}

		public void Stop()
		{

		}

		public void UnInitialize()
		{

		}



		private void CallBackFct(MessageComm message)
		{
			_logger.LogInformation($"Message de notification --> {message.Description} <--");
			var mainWindow = Electron.WindowManager.BrowserWindows.First();
            Electron.IpcMain.Send(mainWindow, "asynchronous-reply", message.Description);
        }
	}
}
