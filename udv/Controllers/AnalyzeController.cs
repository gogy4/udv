using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.Abstraction;

namespace udv.Controllers
{
    /// <summary>
    /// Контроллер для анализа информации о пользователях.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AnalyzeController(
        IUserPostInfoService service,
        ILogger<AnalyzeController> logger,
        IUserIdService userIdService)
        : ControllerBase
    {
        /// <summary>
        /// Анализирует посты пользователя по его идентификатору.
        /// </summary>
        /// <remarks>
        /// Данный метод принимает только идентификатор пользователя.
        /// screen_name пользователя, который у некоторых указан как ник в поисковой строке,
        /// не учитывается в данном методе.
        /// Если нужно получить id пользователя, используйте метод ниже
        /// </remarks>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Информация о постах пользователя.</returns>
        /// <response code="200">Возвращает информацию о постах пользователя.</response>
        /// <response code="400">Если произошла ошибка при получении данных.</response>
        [HttpPost("analyze-user-posts/{userId}")]
        public async Task<IActionResult> AnalyzeUserPosts(string userId)
        {
            logger.LogInformation("Analyze request received");
            try
            {
                var userPostInfo = await service.GetUserPostInfoAsync(userId);
                logger.LogInformation("User info received");
                return Ok(userPostInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получает идентификатор пользователя по его нику.
        /// </summary>
        /// <param name="screenName">Ник пользователя.</param>
        /// <returns>Идентификатор пользователя.</returns>
        /// <response code="200">Возвращает идентификатор пользователя.</response>
        /// <response code="400">Если произошла ошибка при получении идентификатора.</response>
        [HttpGet("get-user-id/{screenName}")]
        public async Task<IActionResult> GetId(string screenName)
        {
            try
            {
                var userId = await userIdService.GetUserIdByScreenNameAsync(screenName);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
