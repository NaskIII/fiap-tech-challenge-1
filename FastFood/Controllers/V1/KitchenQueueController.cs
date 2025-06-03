using Application.Dtos.KitchenDtos.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/kitchen-queue")]
    public class KitchenQueueController : ControllerBase
    {

        private readonly IUpdateStatusKitchenQueueUseCase _updateStatusKitchenQueueUseCase;
        private readonly IKitchenEnqueueOrderUseCase _kitchenEnqueueOrderUseCase;
        private readonly IKitchenDequeueOrderUseCase _kitchenDequeueOrderUseCase;
        private readonly IKitchenQueueListQueue _kitchenQueueListQueue;

        public KitchenQueueController(
            IUpdateStatusKitchenQueueUseCase updateStatusKitchenQueueUseCase,
            IKitchenEnqueueOrderUseCase kitchenEnqueueOrderUseCase,
            IKitchenDequeueOrderUseCase kitchenDequeueOrderUseCase,
            IKitchenQueueListQueue kitchenQueueListQueue)
        {
            _updateStatusKitchenQueueUseCase = updateStatusKitchenQueueUseCase;
            _kitchenEnqueueOrderUseCase = kitchenEnqueueOrderUseCase;
            _kitchenDequeueOrderUseCase = kitchenDequeueOrderUseCase;
            _kitchenQueueListQueue = kitchenQueueListQueue;
        }

        [HttpPost("enqueue")]
        public async Task<IActionResult> EnqueueOrder([FromBody] ManageKitchenRequest request)
        {
            try
            {
                await _kitchenEnqueueOrderUseCase.ExecuteAsync(request);
                return Ok("Order enqueued successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("dequeue")]
        public async Task<IActionResult> DequeueOrder([FromBody] ManageKitchenRequest request)
        {
            try
            {
                await _kitchenDequeueOrderUseCase.ExecuteAsync(request);
                return Ok("Order dequeued successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] ManageKitchenRequest request)
        {
            try
            {
                await _updateStatusKitchenQueueUseCase.ExecuteAsync(request);
                return Ok("Order status updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-queue")]
        public async Task<IActionResult> ListQueue()
        {
            try
            {
                var result = await _kitchenQueueListQueue.ExecuteAsync(null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
