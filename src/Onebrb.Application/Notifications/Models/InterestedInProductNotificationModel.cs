namespace Onebrb.Application.Notifications.Models
{
    public class InterestedInProductNotificationModel
    {
        public string ProductId { get; set; }

        public string InterestedUserId { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
