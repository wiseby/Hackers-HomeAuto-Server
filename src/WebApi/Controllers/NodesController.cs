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
using WebApi.Models;

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

        /// <summary>
        /// Fetches all nodes.
        /// </summary>
        /// <remarks>
        /// <param name="node"></param>
        /// <returns>A list of nodes</returns>
        /// <response code="200">Returns the newly created item</response>     
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NodeDto>>> GetNodes()
        {
            try
            {
                var result = await this.nodeRepository.ReadAll();
                var response = new JsonResponse<IEnumerable<NodeDto>>();

                foreach (var node in result)
                {
                    node.LatestReading = await this.readingRepository.GetLatestByClientId(node.ClientId);
                    node.ReadingsAvailable = await this.readingRepository.GetReadingCount(node.ClientId);
                }
                var nodeDtos = this.mapper.Map<IEnumerable<Node>, List<NodeDto>>(result);
                response.Data = nodeDtos;

                return Ok(response);
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

        /// <summary>
        /// Retrives a Node by a clientId string with more indepth data.
        /// </summary>
        /// <param name="Node"></param>
        /// <returns>A Single Node</returns>
        /// <response code="200">Returns node matching clientId, otherwise null.</response>           
        [HttpGet("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NodeDto>> GetNodeById([FromRoute] string clientId)
        {
            try
            {
                var node = await this.nodeRepository.ReadById(clientId);
                var response = new JsonResponse<NodeDto>();

                if (node != null)
                {
                    var reading = await this.readingRepository.GetLatestByClientId(clientId);
                    var totalReadingCount = await this.readingRepository.GetReadingCount(clientId);
                    var nodeDto = this.mapper.Map<NodeDto>(node);
                    nodeDto.LatestReading = this.mapper.Map<ReadingDto>(reading);
                    nodeDto.ReadingsAvailable = totalReadingCount;
                    response.Data = nodeDto;
                    return Ok(response);
                }
                return Ok(response);
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

        /// <summary>
        /// Retrives all readings from Node by a clientId string.
        /// </summary>
        /// <param name="Node"></param>
        /// <returns>A Single Node</returns>
        /// <response code="200">Returns the newly created item</response>  
        [HttpGet("{clientId}/readings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReadingDto>>> GetNodeReadings([FromRoute] string clientId)
        {
            try
            {
                var node = await this.nodeRepository.ReadById(clientId);
                var response = new JsonResponse<IEnumerable<ReadingDto>>();

                if (node != null)
                {
                    var readings = await this.readingRepository.GetAllByClientId(clientId);
                    response.Data = this.mapper.Map<IEnumerable<Reading>, List<ReadingDto>>(readings);
                    return Ok(response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var message = $"An error accured when fetching readings for client: {clientId}.";
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