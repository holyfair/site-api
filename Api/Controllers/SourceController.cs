using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Shared;
using Models.Sources;
using Services.Interfaceses;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/source")]
    public class SourceController : ControllerBase
    {
        private ISourceService sourceService;

        public SourceController(ISourceService sourceService)
        {
            this.sourceService = sourceService;
        }

        [HttpPost]
        [Route("electronic")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateElectronicSource(ElectronicSource source, string email)
        {
            var content = await sourceService.CreateSourceAsync(source, email);
            return Ok(content);
        }
       
        [HttpPost]
        [Route("book")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBookSource(BookSource source, string email)
        {
            var content = await sourceService.CreateSourceAsync(source, email);
            return Ok(content);
        }

        [HttpPost]
        [Route("periodical")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePeriodicalSource(PeriodicalSource source, string email)
        {
            var content = await sourceService.CreateSourceAsync(source, email);
            return Ok(content);
        }

        [HttpPost]
        [Route("dissertation")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDissertationSource(DissertationSource source, string email)
        {
            var content = await sourceService.CreateSourceAsync(source, email);
            return Ok(content);
        }

        [HttpPost]
        [Route("abstractDissertation")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAbstractDissertationSource(AbstractDissertationSource source, string email)
        {
            var content = await sourceService.CreateSourceAsync(source, email);
            return Ok(content);
        }

        [HttpGet]
        public async Task<IActionResult> GetSources([FromQuery] GetListQuery query, [Required] string email)
        {
            var result = await sourceService.GetSourceAsync(query, email);
            return Ok(result);
        }
    }
}
