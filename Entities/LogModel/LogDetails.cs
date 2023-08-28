using System.Text.Json;

namespace Entities.LogModel
{
    public class LogDetails
    {
        public Object? ModelName { get; set; }
        public Object? Controller { get; set; }
        public Object? Action { get; set; }
        public Object? CreatedAt { get; set; }
        public Object? Id { get; set; }

        public LogDetails()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
