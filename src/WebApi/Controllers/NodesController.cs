using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Models;
using Application.Repositries;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/vnd.api+json")]
    public class NodesController : ControllerBase
    {
        private readonly ILogger<NodesController> logger;
        private readonly AppOptions configuration;
        private readonly INodeRepository nodeRepository;
        private readonly IReadingRepository readingRepository;
        private readonly IMapper mapper;

        public NodesController(
            ILogger<NodesController> logger,
            AppOptions configuration,
            INodeRepository nodeRepository,
            IReadingRepository readingRepository,
            IMapper mapper)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.nodeRepository = nodeRepository;
            this.readingRepository = readingRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetNodes()
        {
            try
            {
                var result = await this.nodeRepository.ReadAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                var message = $"An error accured when fetching AllNodes.";
                var errors = new Dictionary<string, string>();
                errors.Add("readError", message);
                this.logger.LogError(message);
                this.logger.LogError(e.Message);
                return StatusCode(500, errors);
            }
        }

        [HttpGet("{clientId}/readings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetNodeById([FromRoute] string clientId)
        {
            try
            {
                var node = await this.nodeRepository.ReadById(clientId);

                if (node == null)
                {
                    return NotFound(new { Error = $"Node with clientId {clientId} could not be found" });
                }

                var readings = await this.readingRepository.GetAllByClientId(clientId);
                var nodeDto = this.mapper.Map<NodeDto>(node);
                nodeDto.Readings = this.mapper.Map<IEnumerable<Reading>, List<ReadingDto>>(readings);
                return Ok(new { Data = nodeDto });

            }
            catch (Exception e)
            {
                var message = $"An error accured when fetching AllNodes.";
                var errors = new Dictionary<string, string>();
                errors.Add("readError", message);
                this.logger.LogError(message);
                this.logger.LogError(e.StackTrace);
                this.logger.LogError(e.InnerException.Message);
                return StatusCode(500, errors);
            }
        }
    }
}