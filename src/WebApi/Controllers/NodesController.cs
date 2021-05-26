using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Dtos;
using Application.Models;
using Application.Services;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/vnd.api+json")]
    public class NodesController : ControllerBase
    {
        private readonly ILogger<NodesController> logger;
        private readonly IMapper mapper;
        private readonly INodeService nodeService;

        public NodesController(
            ILogger<NodesController> logger,
            IMapper mapper,
            INodeService nodeService
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.nodeService = nodeService;
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
                var nodes = await this.nodeService.GetNodes();
                var nodeDtos = this.mapper.Map<IEnumerable<Node>, List<NodeDto>>(nodes);
                var response = new JsonOkResponse<IEnumerable<NodeDto>>(nodeDtos);
                return Ok(response);
            }
            catch (Exception e)
            {
                var message = $"An error accured when fetching AllNodes.";
                var errors = LogError(e, message);
                var response = new JsonErrorResponse(errors);
                return StatusCode(500, response);
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
        public async Task<ActionResult<NodeDto>> GetNodeById(
            [FromRoute] string clientId)
        {
            try
            {
                var node = await this.nodeService.GetNodeById(clientId);
                var data = this.mapper.Map<NodeDto>(node);
                var response = new JsonOkResponse<NodeDto>(data);
                return Ok(response);
            }
            catch (Exception e)
            {
                var message = $"An error accured when fetching AllNodes.";
                var errors = LogError(e, message);
                var response = new JsonErrorResponse(errors);
                return StatusCode(500, response);
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
        public async Task<ActionResult<IEnumerable<ReadingDto>>> GetNodeReadings(
            [FromRoute] string clientId)
        {
            try
            {
                var readings = await this.nodeService.GetReadingsById(clientId);
                var data = this.mapper
                    .Map<IEnumerable<Reading>, List<ReadingDto>>(readings);
                var response = new JsonOkResponse<IEnumerable<ReadingDto>>(data);
                return Ok(response);
            }
            catch (Exception e)
            {
                var message = $"An error accured when fetching readings for client: {clientId}.";
                var errors = LogError(e, message);
                var response = new JsonErrorResponse(errors);
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Fetches all nodes.
        /// </summary>
        /// <remarks>
        /// <param name="node"></param>
        /// <returns>A list of nodes</returns>
        /// <response code="200">Returns the newly created item</response>     
        [HttpGet("{clientId}/definitions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetDefinitions(
            [FromRoute] string clientId)
        {
            try
            {
                var definitions = await this.nodeService.GetDefinitionsByClientId(
                    clientId);
                return Ok();
            }
            catch (Exception e)
            {
                var message = $"An error accured when fetching definitions for client: {clientId}.";
                var errors = LogError(e, message);
                var response = new JsonErrorResponse(errors);
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Fetches all nodes.
        /// </summary>
        /// <remarks>
        /// <param name="node"></param>
        /// <returns>A list of nodes</returns>
        /// <response code="201">Returns the newly created item</response>     
        [HttpPut("{clientId}/definitions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<string>>> UpdateDefinitions(
            [FromRoute] string clientId, [FromBody] ReadingDefinition definition)
        {
            try
            {
                var definitions = await this.nodeService.UpdateDefinitions(
                    clientId, definition);
                return Ok();
            }
            catch (Exception e)
            {
                var message = $"An error accured when updating definitions for client: {clientId}.";
                var errors = LogError(e, message);
                var response = new JsonErrorResponse(errors);
                return StatusCode(500, response);
            }
        }

        private Dictionary<string, string> LogError(Exception e, string message)
        {
            var errors = new Dictionary<string, string>();
            errors.Add("readError", message);
            this.logger.LogError(message);
            if (e != null)
            {
                this.logger.LogError(e.StackTrace);
                this.logger.LogError(e.Message);
            }
            return errors;
        }
    }
}