using Domain;
using Domain.Mom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Processes.Pages
{
    public class FeedbackModel : PageModel
    {
        [BindProperty]
        public string Auteur { get; set; }

		[BindProperty]
		public string Description { get; set; }

		private readonly ILogger _logger;

		private readonly IServiceDeamon _momListener;

		private readonly IServiceDeamon _notificationService;

		//public FeedbackModel(ILogger<FeedbackModel> logger, MomListener momListener)
		public FeedbackModel(
			ILogger<FeedbackModel> logger, 
			[FromKeyedServices("MomListener")] IServiceDeamon momListener,
			[FromKeyedServices("NotificationService")] IServiceDeamon notificationService)
		{
			_logger = logger;
			_momListener = momListener;
			_notificationService = notificationService;
			_logger.LogInformation("Ctr du FeedbackModel");
		}


		public void OnGet()
        {
			
		}

        public void OnPost() 
        {
			_logger.LogInformation($"Auteur : {Auteur} / Description : {Description}");
			MessageComm message = new MessageComm
			{
				CreationDate = DateTime.Now,
				Description = Description,
				Source = Auteur,
				Target = "le topic"
			};
			_momListener.EmissionMessage(message);
		}    
    }
}
