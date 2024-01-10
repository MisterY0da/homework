using Microsoft.AspNetCore.Mvc;
using Producer.Services;

namespace Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IKafkaService _kafkaService;

        private readonly IRabbitMqService _mqService;

        public ProducerController(IKafkaService kafkaService, IRabbitMqService mqService) 
        {
            _kafkaService = kafkaService;
            _mqService = mqService;
        }

        [Route("kafka")]
        [HttpGet]
        public IActionResult SendToKafka(string message)
        {
            _kafkaService.SendMessageAsync(message);

            return Ok("Сообщение отправлено");
        }

        [Route("rabbit")]
        [HttpGet]
        public IActionResult SendToRabbit(string message)
        {
            _mqService.SendMessage(message);

            return Ok("Сообщение отправлено");
        }
    }
}
